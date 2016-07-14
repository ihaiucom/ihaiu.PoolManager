using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Ihaius
{
    public partial class PoolGroup 
    {
        /** 组名称 */
        public string groupName;

        /** 是否打印日志信息 */
        public bool logMessages = true;

        internal Dictionary<Type, AbstractObjectPool> typePools = new Dictionary<Type, AbstractObjectPool>();

        public PoolGroup(string groupName)
        {
            this.groupName = groupName;
        }


        public List<Coroutine> corutineList = new List<Coroutine>();
        /** 启动协程 */
        virtual public Coroutine StartCoroutine(IEnumerator routine)
        {
            Coroutine c = PoolManager.Instance.StartCoroutine(routine);
            corutineList.Add(c);
            return c;
        }

        virtual public void StopAllCoroutines()
        {
            Coroutine c;
            for(int i = corutineList.Count - 1; i >= 0; i --)
            {
                c = corutineList[i];
                PoolManager.Instance.StopCoroutine(c);
            }

            corutineList.Clear();
        }

        public void Start()
        {
            if (this.logMessages)
                Debug.Log(string.Format("PoolGroup {0}: Initializing..", this.groupName));

            PoolManager.groups.Add(this);
        }

        public void Destroy()
        {
            OnDestruct();
        }

        internal void OnDestruct()
        {
            if (this.logMessages)
                Debug.Log(string.Format("PoolGroup {0}: Destroying...", this.groupName));

            PoolManager.groups.Remove(this);

            this.StopAllCoroutines();

            foreach(var kvp in typePools)
            {
                kvp.Value.SelfDestruct();
            }

            typePools.Clear();

            OnDestruct_Prefab();

        }


        /** 添加一个对象池 */
        public void CreatePool<T>(ObjectPool<T> objectPool)
        {
            Type type = typeof(T);

            bool isAlreadyPool = this.GetPool(type) == null ? false : true;
            if (!isAlreadyPool)
            {
                objectPool.poolGroup = this;
                typePools.Add(type, objectPool);
            }

            objectPool.inspectorInstanceConstructor();
            // Preloading (uses a singleton bool to be sure this is only done once)
            if (objectPool.preloaded != true)
            {
                if (this.logMessages)
                    Debug.Log(string.Format("PoolGroup {0}: 预实例化对象中 preloadAmount={1},  {2}",
                        this.groupName,
                        objectPool.preloadAmount,
                        objectPool.name));

                objectPool.PreloadInstances();
            }
        }

        /** 添加一个实例到对应的Pool。despawn,true为闲置状态;false为真正使用状态 */
        public void Add<T>(T instance, bool despawn)
        {
            ObjectPool<T> pool = GetPool<T>();
            if (pool != null)
            {
                pool.AddUnpooled(instance, despawn);
            }
            else
            {
                Debug.LogError(string.Format("PoolGroup {0}: 添加实例对象时，没找到对应类型的对象池 {1} ",
                    this.groupName,
                    instance));
            }


        }

        /** 获取对应类型的Pool */
        public ObjectPool<T> GetPool<T>()
        {
            return (ObjectPool<T>) GetPool(typeof(T));
        }

        /** 获取对应类型的Pool */
        public AbstractObjectPool GetPool(Type type)
        {

            if (typePools.ContainsKey(type))
            {
                return typePools[type];
            }
            return null;
        }





        /** 获取一个对应类型的实例对象 */
        public T Spawn<T>(params object[] args)
        {
            T inst;

            ObjectPool<T> pool = GetPool<T>();

            if (pool == null)
            {
                pool = new ObjectPool<T>();
                CreatePool<T>(pool);
            }

            inst = pool.SpawnInstance(args);

            // This only happens if the limit option was used for this
            //   Prefab Pool.
            if (inst == null)
                return default(T);

            pool.ItemOnSpawned(inst);

            return inst;
        }

        /** 将实例对象设置为闲置状态 */
        public void Despawn<T>(T inst)
        {
            ObjectPool<T> pool = GetPool<T>();
            if (pool != null)
            {
                pool.DespawnInstance(inst);
            }
        }

        public void Despawn<T>(T instance, float seconds)
        {
            StartCoroutine(this.DoDespawnAfterSeconds(instance, seconds));
        }


        private IEnumerator DoDespawnAfterSeconds<T>(T instance, float seconds)
        {
            yield return  new WaitForSeconds(seconds);
            Despawn<T>(instance);
        }


    }
}
