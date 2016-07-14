using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Ihaius
{
    public partial class PoolGroupDict 
    {
        #region Event Handling
        public delegate void OnCreatedDelegate(PoolGroup poolGroup);

        internal Dictionary<string, OnCreatedDelegate> onCreatedDelegates = 
            new Dictionary<string, OnCreatedDelegate>();
        /** 添加监听创建PoolGroup */
        public void AddOnCreatedDelegate(string groupName, OnCreatedDelegate createdDelegate)
        {
            // Assign first delegate "just in time"
            if (!this.onCreatedDelegates.ContainsKey(groupName))
            {
                this.onCreatedDelegates.Add(groupName, createdDelegate);
                return;
            }

            this.onCreatedDelegates[groupName] += createdDelegate;
        }

        /** 移除监听创建PoolGroup */
        public void RemoveOnCreatedDelegate(string groupName, OnCreatedDelegate createdDelegate)
        {
            if (!this.onCreatedDelegates.ContainsKey(groupName))
                throw new KeyNotFoundException
                (
                    "No OnCreatedDelegates found for PoolGroup name '" + groupName + "'."
                );

            this.onCreatedDelegates[groupName] -= createdDelegate;
        }

        #endregion Event Handling


        private Dictionary<string, PoolGroup> _groups = new Dictionary<string, PoolGroup>();


        /** 销毁PoolGroup */
        public bool Destroy(string groupName)
        {
            PoolGroup poolGroup;
            if (!this._groups.TryGetValue(groupName, out poolGroup))
            {
                Debug.LogError(
                    string.Format("PoolManager: Unable to destroy '{0}'. Not in PoolManager",
                        groupName));
                return false;
            }


            poolGroup.OnDestruct();
            return true;
        }

        /** 销毁所有PoolGroup */
        public void DestroyAll()
        {
            foreach (KeyValuePair<string, PoolGroup> pair in this._groups)
                pair.Value.OnDestruct();

            this._groups.Clear();
        }

        /** 创建PoolGroup */
        public PoolGroup Create(string groupName)
        {
            PoolGroup poolGroup = new PoolGroup(groupName);
            poolGroup.Start();

            return poolGroup;
        }

        private bool assertValidGroupName(string groupName)
        {
            // Cannot request a name with the word "Pool" in it. This would be a 
            //   rundundant naming convention and is a reserved word for GameObject
            //   defaul naming
            string tmpGroupName;
            tmpGroupName = groupName.Replace("Pool", "");
            if (tmpGroupName != groupName)  // Warn if "Pool" was used in poolName
            {
                // Log a warning and continue on with the fixed name
                string msg = string.Format("'{0}' has the word 'Pool' in it. " +
                    "This word is reserved for GameObject defaul naming. " +
                    "The pool name has been changed to '{1}'",
                    groupName, tmpGroupName);

                Debug.LogWarning(msg);
                groupName = tmpGroupName;
            }

            if (this.ContainsKey(groupName))
            {
                Debug.Log(string.Format("A pool with the name '{0}' already exists",
                    groupName));
                return false;
            }

            return true;
        }

        internal void Add(PoolGroup group)
        {
            // Don't let two pools with the same name be added. See error below for details
            if (this.ContainsKey(group.groupName))
            {
                Debug.LogError(string.Format("PoolManager.Add(PoolGroup group), 已经存在groupName={0}的PoolGroup",
                    group.groupName));
                return;
            }

            this._groups.Add(group.groupName, group);

            if (this.onCreatedDelegates.ContainsKey(group.groupName))
                this.onCreatedDelegates[group.groupName](group);
        }

        internal bool Remove(PoolGroup group)
        {
            if (!this.ContainsKey(group.groupName))
            {
                Debug.LogError(string.Format("PoolManager: Unable to remove '{0}'. " +
                    "GroupPool not in PoolManager",
                    group.groupName));
                return false;
            }

            this._groups.Remove(group.groupName);
            return true;
        }



        #region Dict Functionality
        public int Count { get { return this._groups.Count; } }

        /** 是否已经存在groupName的PoolGroup */
        public bool ContainsKey(string groupName)
        {
            return this._groups.ContainsKey(groupName);
        }

        /** 尝试获取groupName的PoolGroup */
        public bool TryGetValue(string groupName, out PoolGroup poolGroup)
        {
            return this._groups.TryGetValue(groupName, out poolGroup);
        }

        /** 获取key的PoolGroup */
        public PoolGroup this[string key]
        {
            get
            {
                PoolGroup group;
                try
                {
                    group = this._groups[key];
                }
                catch (KeyNotFoundException)
                {
                    string msg = string.Format("A PoolGroup with the name '{0}' not found. " +
                        "\nPools={1}",
                        key, this.ToString());
                    throw new KeyNotFoundException(msg);
                }

                return group;
            }
            set
            {
                string msg = "Cannot set PoolManager.Group[key] directly. " +
                    "SpawnPools add themselves to PoolManager.Pools when created, so " +
                    "there is no need to set them explicitly. Create pools using " +
                    "PoolManager.Pools.Create() or add a SpawnPool component to a " +
                    "GameObject.";
                throw new System.NotImplementedException(msg);
            }
        }
        #endregion Dict Functionality



        /** 默认PoolGroup，可以自己扩展 */
        public PoolGroup common
        {
            get
            {
                if (!ContainsKey("CommonPoolGroup"))
                {
                    Create("CommonPoolGroup");
                }

                return this["CommonPoolGroup"];
            }
        }
    }
}