using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Ihaius
{
    public class ObjectPool<T> : AbstractObjectPool
    {
       


        #region Constructor and Self-Destruction

        /** 正在被使用的对象列表 */
        internal List<T> _spawned = new List<T>();
        public List<T> spawned { get { return new List<T>(this._spawned); } }

        /** 没有被使用的闲置对象列表 */
        internal List<T> _despawned = new List<T>();
        public List<T> despawned { get { return new List<T>(this._despawned); } }

        /** 对象的总数 = 使用的数量 + 闲置的数量 */
        public int totalCount
        {
            get
            {
                // Add all the items in the pool to get the total count
                int count = 0;
                count += this._spawned.Count;
                count += this._despawned.Count;
                return count;
            }
        }

        public ObjectPool()
        {
            name = "[ObjectPool]" + typeof(T);
        }

        /** 构造方法 */
        virtual internal void inspectorInstanceConstructor()
        {
            _spawned = new List<T>();
            _despawned = new List<T>();
        }

        /** 析构方法
        * 当SpawnPool.OnDestroy时调用
        */
        override internal void SelfDestruct()
        {
            for (int i = _despawned.Count - 1; i >= 0; i--)
            {
                ItemDestruct(_despawned[i]);
            }


            for (int i = _spawned.Count - 1; i >= 0; i--)
            {
                ItemDestruct(_spawned[i]);
            }

            _spawned.Clear();
            _despawned.Clear();
        }

        #endregion Constructor and Self-Destruction




        #region Pool Functionality

        /// <summary>
        /// 设置对象为闲置
        /// 将对象xfrom从 _spawned转到_despawned
        /// sendEventMessage=true时会对xfrom对象广播OnDespawned消息
        /// 设置xfrom.gameObejct.SetActive(false)
        /// 检测是否需要启动自动清理
        /// </summary>
        internal bool DespawnInstance(T xform)
        {
            return DespawnInstance(xform, true);
        }

        internal bool DespawnInstance(T xform, bool sendEventMessage)
        {
            if (this._despawned.Contains(xform))
            {
                if (this.logMessages)
                    Debug.LogWarningFormat(string.Format("[对象池] {0} : 设置闲置对象时,对象已经在闲置列表 {1}",
                        this.name,
                        xform));
                
                return false;
            }

            if (this.logMessages)
                Debug.Log(string.Format("[对象池] {0} : 设置闲置对象 {1}",
                    this.name,
                    xform));

            // Switch to the despawned list
            this._spawned.Remove(xform);
            this._despawned.Add(xform);

            // Notify instance of event OnDespawned for custom code additions.
            //   This is done before handling the deactivate and enqueue incase 
            //   there the user introduces an unforseen issue.
            if (sendEventMessage)
            {
                ItemOnDespawned(xform);
            }

            // Deactivate the instance and all children
            ItemSetActive(xform, false);

            // Trigger culling if the feature is ON and the size  of the 
            //   overall pool is over the Cull Above threashold.
            //   This is triggered here because Despawn has to occur before
            //   it is worth culling anyway, and it is run fairly often.
            if (!this.cullingActive &&   // Cheap & Singleton. Only trigger once!
                this.cullDespawned &&    // Is the feature even on? Cheap too.
                this.totalCount > this.cullAbove)   // Criteria met?
            {
                this.cullingActive = true;
                this.StartCoroutine(CullDespawned());
            }
            return true;
        }



        /** 清理闲置对象协程是否启动 */
        private bool cullingActive = false;
        /** 清理闲置对象协程 */
        internal IEnumerator CullDespawned()
        {
            if (this.logMessages)
                Debug.Log(string.Format("[对象池] {0} :  " +
                    "触发清理闲置对象,等待{1}秒开始检测despawns...",
                    this.name,
                    this.cullDelay));

            yield return new WaitForSeconds(this.cullDelay);

            while (this.totalCount > this.cullAbove)
            {
                for (int i = 0; i < this.cullMaxPerPass; i++)
                {
                    if (this.totalCount <= this.cullAbove)
                        break; 

                    if (this._despawned.Count > 0)
                    {
                        T inst = this._despawned[0];
                        this._despawned.RemoveAt(0);
                        ItemDestruct(inst);

                        if (this.logMessages)
                            Debug.Log(string.Format("[对象池] {0} : " +
                                "清理数量至{1}个，目前实例对象数量{2}个",
                                this.name,
                                this.cullAbove,
                                this.totalCount));
                    }
                    else if (this.logMessages)
                    {
                        Debug.Log(string.Format("[对象池] {0} : " +
                            "等待闲置对象，目前闲置对象数量为0。 " +
                            "等待{1}秒再次次检测",
                            this.name,
                            this.cullDelay));

                        break;
                    }
                }

                // Check again later
                yield return new WaitForSeconds(this.cullDelay);
            }

            if (this.logMessages)
                Debug.Log(string.Format("[对象池] {0} : 清理闲置对象完成!停止该协程!",
                    this.name));

            // Reset the singleton so the feature can be used again if needed.
            this.cullingActive = false;
            yield return null;
        }






        /// <summary>
        /// 获取一个实例对象
        /// 如果限制了实例对象数量，且正在使用的对象数量大于限制的数量，且开启了limitFIFO，那么就把_spawned[0]设置为闲置状态
        /// 如果闲置列表里有对象，就从限制列表里取出第一个对象；并把这个对象放到_spawned列表
        /// 如果限制列表没有对象，就新建一个对象
        /// </summary>
        /// <returns>The instance.</returns>
        internal T SpawnInstance(params object[] args)
        {
            // Handle FIFO limiting if the limit was used and reached.
            //   If first-in-first-out, despawn item zero and continue on to respawn it
            if (this.limitInstances && this.limitFIFO &&
                this._spawned.Count >= this.limitAmount)
            {
                T firstIn = this._spawned[0];

                if (this.logMessages)
                {
                    Debug.Log(string.Format
                        (
                            "[对象池] {0} : " +
                            "限制数量达到上限! FIFO=True. 将第一个被使用对象设置为闲置状态 {1}...",
                            this.name,
                            firstIn
                        ));
                }

                this.DespawnInstance(firstIn);

               
            }

            T inst;

            // If nothing is available, create a new instance
            if (this._despawned.Count == 0)
            {
                // This will also handle limiting the number of NEW instances
                inst = this.SpawnNew(args);
                ItemSetActive(inst, true);
            }
            else
            {
                // Switch the instance we are using to the spawned list
                // Use the first item in the list for ease
                inst = this._despawned[0];
                this._despawned.RemoveAt(0);
                this._spawned.Add(inst);

                // This came up for a user so this was added to throw a user-friendly error
                if (inst == null)
                {
                    var msg = "确定你没有自己销毁实例对象!";
                    throw new MissingReferenceException(msg);
                }

                if (this.logMessages)
                    Debug.Log(string.Format("[对象池] {0} : 从闲置列表取出了一个对象 '{1}'.",
                        this.name,
                        inst));

                ItemSetArg(inst, args);
                ItemSetActive(inst, true);

            }

            //
            // NOTE: OnSpawned message broadcast was moved to main Spawn() to ensure it runs last
            //

            return inst;
        }



        /// <summary>
        /// 创建一个实例对象
        /// 如果限制了实例对象数量，且对象总数大于限制的数量。就返回一个空对象 return null
        /// 否则 实例化一个对象，并检查SpawnPool的设置，对该对象进行设置。最后返回该创建的对象
        /// </summary>

        public T SpawnNew(params object[] arg)
        {
            // Handle limiting if the limit was used and reached.
            if (this.limitInstances && this.totalCount >= this.limitAmount)
            {
                if (this.logMessages)
                {
                    Debug.Log(string.Format
                        (
                            "[对象池] {0} : " +
                            "实例对象数量达到上限，不能创建新实例。(Returning null)",
                            this.name
                        ));
                }

                return default(T);
            }


            T inst = (T)Instantiate(arg);
            this.nameInstance(inst);  // Adds the number to the end

         

            // Start tracking the new instance
            this._spawned.Add(inst);

            if (this.logMessages)
                Debug.Log(string.Format( "[对象池] {0} : 创建了一个新对象{1}",
                    this.name,
                    inst));

            return inst;
        }


        /// <summary>
        /// 将以个势力对象添加到缓存池。 despawn=true时就加到_despwan闲置列表，并设置gameObject.active=false；否则就加到_spawn正在被使用列表
        /// </summary>
        /// <param name="inst">The instance to add</param>
        /// <param name="despawn">True to despawn on add</param>
        internal void AddUnpooled(T inst, bool despawn)
        {
            this.nameInstance(inst);   // Adds the number to the end

            if (despawn)
            {
                // Deactivate the instance and all children
                ItemSetActive(inst, false);

                // Start Tracking as despawned
                this._despawned.Add(inst);
            }
            else
                this._spawned.Add(inst);
        }



        /// <summary>
        /// 如果开启异步预实例对象，就开启延迟异步预实例化对象协程。如果每帧预实例化数量大于预实例化数量preloadFrames > preloadAmount，
        /// 那么就设置每帧预实例化数量等于预实例化数量preloadFrames preloadAmount
        /// </summary>
        /// <returns></returns>
        internal void PreloadInstances()
        {
            // If this has already been run for this PrefabPool, there is something
            //   wrong!
            if (this.preloaded)
            {
                Debug.Log(string.Format("[对象池] {0} : 已经预实例化过，你不能预实例化两次.",
                    this.name));

                return;
            }


            // Protect against preloading more than the limit amount setting
            //   This prevents an infinite loop on load if FIFO is used.
            if (this.limitInstances && this.preloadAmount > this.limitAmount)
            {
                Debug.LogWarning
                (
                    string.Format
                    (
                        "[对象池] {0} : 你配置的预实例对象数量超过限制数量。强制预实例数量等于限制数量" ,
                        this.name
                    )
                );

                this.preloadAmount = this.limitAmount;
            }

            // Notify the user if they made a mistake using Culling
            //   (First check is cheap)
            if (this.cullDespawned && this.preloadAmount > this.cullAbove)
            {
                Debug.LogWarning(string.Format("[对象池] {0} : " +
                    "你配置的预实例对象数量超过自动清理保留数量，这样很浪费! preloadAmount={1}, cullAbove={2}",
                    this.name, preloadAmount, cullAbove
                ));
            }

            if (this.preloadTime)
            {
                if (this.preloadFrames > this.preloadAmount)
                {
                    Debug.LogWarning(string.Format("[对象池] {0} : " +
                        "每帧预实例数量大于总预实例数量，不合理！ preloadFrames={1}, preloadAmount={2}",
                        this.name, preloadFrames, preloadAmount
                    ));

                    this.preloadFrames = this.preloadAmount;
                }

                StartCoroutine(this.PreloadOverTime());
            }
            else
            {
                // Reduce debug spam: Turn off this.logMessages then set it back when done.
                this.forceLoggingSilent = true;

                T inst;
                while (this.totalCount < this.preloadAmount) // Total count will update
                {
                    // Preload...
                    // This will parent, position and orient the instance
                    //   under the SpawnPool.group
                    inst = this.SpawnNew();
                    this.DespawnInstance(inst, false);
                }

                // Restore the previous setting
                this.forceLoggingSilent = false;
            }
        }

        private IEnumerator PreloadOverTime()
        {
            yield return new WaitForSeconds(this.preloadDelay);

            T inst;

            // subtract anything spawned by other scripts, just in case
            int amount = this.preloadAmount - this.totalCount;
            if (amount <= 0)
                yield break;

            // Doesn't work for Windows8...
            //  This does the division and sets the remainder as an out value.
            //int numPerFrame = System.Math.DivRem(amount, this.preloadFrames, out remainder);
            int remainder = amount % this.preloadFrames;
            int numPerFrame = amount / this.preloadFrames;

            // Reduce debug spam: Turn off this.logMessages then set it back when done.
            this.forceLoggingSilent = true;

            int numThisFrame;
            for (int i = 0; i < this.preloadFrames; i++)
            {
                // Add the remainder to the *last* frame
                numThisFrame = numPerFrame;
                if (i == this.preloadFrames - 1)
                {
                    numThisFrame += remainder;
                }

                for (int n = 0; n < numThisFrame; n++)
                {
                    // Preload...
                    // This will parent, position and orient the instance
                    //   under the SpawnPool.group
                    inst = this.SpawnNew();
                    if (inst != null)
                        this.DespawnInstance(inst, false);

                    yield return null;
                }

                // Safety check in case something else is making instances. 
                //   Quit early if done early
                if (this.totalCount > this.preloadAmount)
                    break;
            }

            // Restore the previous setting
            this.forceLoggingSilent = false;
        }

        #endregion Pool Functionality

        #region Utilities
        /// <summary>
        /// 检查是否包含了该对象。spawned、despawned从这两个列表里检查
        /// </summary>
        /// <param name="inst">A instance to test.</param>
        /// <returns>bool</returns>
        public bool Contains(T inst)
        {
            bool contains;

            contains = this.spawned.Contains(inst);
            if (contains)
                return true;

            contains = this.despawned.Contains(inst);
            if (contains)
                return true;

            return false;
        }
       
        #endregion Utilities




        #region Instantiate Method

        private Type _type;
        public Type type
        {
            get
            {
                if (_type == null)
                {
                    _type = typeof(T);
                }
                return _type;
            }
        }


        /** T是否是实现IPoolItem接口 */
        private bool? _IsImplementIPoolItem;
        public bool IsImplementIPoolItem
        {
            get
            {
                if (_IsImplementIPoolItem == null)
                {
                    _IsImplementIPoolItem = typeof(IPoolItem).IsAssignableFrom(type);
                }
                return _IsImplementIPoolItem.Value;
            }
        }


        /** T是否继承了ScriptableObject */
        private bool? _IsScriptableObject;
        public bool IsScriptableObject
        {
            get
            {
                if (_IsScriptableObject == null)
                {
                    _IsScriptableObject = typeof(ScriptableObject).IsAssignableFrom(type);
                }
                return _IsScriptableObject.Value;
            }
        }


        /** T是否继承了MonoBehaviour */
        private bool? _IsMonoBehaviour;
        public bool IsMonoBehaviour
        {
            get
            {
                if (_IsMonoBehaviour == null)
                {
                    _IsMonoBehaviour = typeof(MonoBehaviour).IsAssignableFrom(type);
                }
                return _IsMonoBehaviour.Value;
            }
        }


        /** T是否是GameObject */
        private bool? _IsGameObject;
        public bool IsGameObject
        {
            get
            {
                if (_IsGameObject == null)
                {
                    _IsGameObject = typeof(GameObject).IsAssignableFrom(typeof(T));
                }
                return _IsGameObject.Value;
            }
        }



        /** T是否是Transform */
        private bool? _IsTransform;
        public bool IsTransform
        {
            get
            {
                if (_IsTransform == null)
                {
                    _IsTransform = typeof(Transform).IsAssignableFrom(typeof(T));
                }
                return _IsTransform.Value;
            }
        }





        /** 调用对象方法--销毁 */
        virtual protected void ItemDestruct(T instance)
        {
            if (IsImplementIPoolItem)
            {
                IPoolItem item = (IPoolItem) instance;
                item.PDestruct();
            }
        }

        /** 调用对象方法--设置为闲置状态消息 */
        virtual protected void ItemOnDespawned(T instance)
        {

            if (IsImplementIPoolItem)
            {
                IPoolItem item = (IPoolItem) instance;
                item.POnDespawned(this);
            }
        }


        /** 调用对象方法--设置为使用状态消息 */
        virtual internal void ItemOnSpawned(T instance)
        {
            if (IsImplementIPoolItem)
            {
                IPoolItem item = (IPoolItem) instance;
                item.POnSpawned(this);
            }
        }

        /** 调用对象方法--设置是否激活 */
        virtual protected void ItemSetActive(T instance, bool value)
        {
            if (IsImplementIPoolItem)
            {
                IPoolItem item = (IPoolItem) instance;
                item.PSetActive(value);
            }
        }


        /** 调用对象方法--实例对象重设参数 */
        virtual protected void ItemSetArg(T instance, params object[] args)
        {
            if (IsImplementIPoolItem)
            {
                IPoolItem item = (IPoolItem) instance;
                item.PSetArg(args);
            }
        }

        /** 给实例对象重命名 */
        virtual protected void nameInstance(T instance)
        {
            if (IsImplementIPoolItem)
            {
                IPoolItem item = (IPoolItem) instance;
                item.PName += (this.totalCount + 1).ToString("#000");
            }

        }

        /** 实例化一个对象 */
        virtual protected T Instantiate(params object[] arg)
        {
            T instance = System.Activator.CreateInstance<T>();
            ItemSetArg(instance, arg);
            return instance;
        }


        /** 启动协程 */
        virtual public Coroutine StartCoroutine(IEnumerator routine)
        {
            if (poolGroup != null)
            {
                return poolGroup.StartCoroutine(routine);
            }
            else
            {
                return PoolManager.Instance.StartCoroutine(routine);
            }
        }
        #endregion

        public override string ToString()
        {
            return string.Format("[{3}: spawned={0}, despawned={1}, totalCount={2}]", spawned.Count, despawned.Count, totalCount, name);
        }

    }
}