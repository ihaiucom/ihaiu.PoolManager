using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Ihaius
{
    public class PrefabPool : ObjectPool<Transform>
    {
        // 勾选后实例化的游戏对象的缩放比例将全是1，不勾选择用Prefab默认的。
        public bool matchPoolScale = false;
        // 勾选后实例化的游戏对象的Layer将用Prefab默认的。
        public bool matchPoolLayer = false;

        public Transform parent;
        public Transform prefab;
        private GameObject _prefabGO;
        internal GameObject prefabGO
        {
            get
            {
                if (_prefabGO == null)
                {
                    _prefabGO = prefab.gameObject;
                }
                return _prefabGO;
            }
        }
        private bool _dontDestroyOnLoad;
        public bool dontDestroyOnLoad
        {
            get
            {
                return this._dontDestroyOnLoad;
            }

            set
            {
                this._dontDestroyOnLoad = value;

                if (this.parent != null)
                    Object.DontDestroyOnLoad(this.parent.gameObject);
            }
        }

        internal override void inspectorInstanceConstructor()
        {
            base.inspectorInstanceConstructor();

            _prefabGO = prefab.gameObject;

            if (parent == null)
            {
                parent = new GameObject(prefabGO.name.Replace("(Clone)", "") + "Pool").transform;
            }

            if (dontDestroyOnLoad)
            {
                Object.DontDestroyOnLoad(this.parent.gameObject);
            }
        }


        internal override void SelfDestruct()
        {
            base.SelfDestruct();
            if (parent != null)
            {
                GameObject.Destroy(parent.gameObject);
            }
        }




        /** 调用对象方法--销毁 */
        protected override void ItemDestruct(Transform instance)
        {
            Object.Destroy(instance.gameObject);
        }


        /** 调用对象方法--设置为使用状态消息 */
        internal override void ItemOnSpawned(Transform instance)
        {
            instance.gameObject.BroadcastMessage(
                "OnSpawned",
                this,
                SendMessageOptions.DontRequireReceiver
            );

        } 
       

        /** 调用对象方法--设置为闲置状态消息 */
        protected override void ItemOnDespawned(Transform instance)
        {
            instance.gameObject.BroadcastMessage(
                "OnDespawned",
                this,
                SendMessageOptions.DontRequireReceiver
            );
        }

        /** 调用对象方法--设置是否激活 */
        protected override void ItemSetActive(Transform instance, bool value)
        {
            instance.gameObject.SetActive(value);
        }

        /** 调用对象方法--实例对象重设参数 */
        protected override void ItemSetArg(Transform instance, params object[] args)
        {
            instance.gameObject.BroadcastMessage(
                "SetArg",
                args,
                SendMessageOptions.DontRequireReceiver
            );
        }

        /** 给实例对象重命名 */
        protected override void nameInstance(Transform instance)
        {
            instance.gameObject.name = instance.name.Replace("(Clone)", string.Empty) + (this.totalCount + 1).ToString("#000");
        }

        /** 实例化一个对象 */
        protected override Transform Instantiate(params object[] args)
        {
            Transform instance = GameObject.Instantiate(prefabGO).transform;
            if(parent != null) instance.SetParent(parent, false);
            ItemSetArg(instance, args);
            return instance;
        }


    }
}