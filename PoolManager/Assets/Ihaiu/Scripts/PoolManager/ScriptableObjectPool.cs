using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Ihaius
{
    public class ScriptableObjectPool<T> : ObjectPool<T> where T : ScriptableObject
    {
       
        /** 给实例对象重命名 */
        protected override void nameInstance(T instance)
        {
            base.nameInstance(instance);
            instance.name += (this.totalCount + 1).ToString("#000");
        }

        /** 实例化一个对象 */
        protected override T Instantiate(params object[] args)
        {
            T instance = ScriptableObject.CreateInstance<T>();
            ItemSetArg(instance, args);
            return instance;
        }
    }
}