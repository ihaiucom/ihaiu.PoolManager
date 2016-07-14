using UnityEngine;
using System.Collections;

namespace Ihaius
{
    public interface IPoolItem
    {
        /** 对象池的名称描述 */
        string PName{ get; set;}

        /** 销毁 */
        void PDestruct();

        /** 对象池设置--设置为使用状态消息 */
        void POnSpawned<T>(ObjectPool<T> pool);

        /** 对象池设置--该对象为闲置状态 */
        void POnDespawned<T>(ObjectPool<T> pool);

        /** 对象池设置--该对象是否激活 */
        void PSetActive(bool value);

        /** 对象池设置--该对象重设参数 */
        void PSetArg(params object[] args);

    }
}