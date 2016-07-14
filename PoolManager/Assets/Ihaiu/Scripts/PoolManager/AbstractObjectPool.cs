using UnityEngine;
using System.Collections;

namespace Ihaius
{
    public abstract class AbstractObjectPool 
    {
        public PoolGroup poolGroup;

        public string name { get; set;}

        #region 配置属性
        /** 缓存池这个Prefab的预加载数量。意思为一开始加载的数量！*/
        public int preloadAmount = 1;

        /** 如果勾选表示缓存池所有的gameobject可以“异步”加载。*/
        public bool preloadTime = false;

        /** 每几帧加载一个。*/
        public int preloadFrames = 2;

        /** 延迟多久开始加载。*/
        public float preloadDelay = 0;



        /** 是否开启对象实例化的限制功能。 */
        public bool limitInstances = false;

        /** 限制实例化Prefab的数量，也就是限制缓冲池的数量，它和上面的preloadAmount是有冲突的，如果同时开启则以limitAmout为准。 */
        public int limitAmount = 100;

        /** 如果我们限制了缓存池里面只能有10个Prefab，如果不勾选它，那么你拿第11个的时候就会返回null。如果勾选它在取第11个的时候他会返回给你前10个里最不常用的那个。*/
        public bool limitFIFO = false;



        /** 是否开启缓存池智能自动清理模式。*/
        public bool cullDespawned = false;

        /** 缓存池自动清理，但是始终保留几个对象不清理。 */
        public int cullAbove = 50;

        /** 每过多久执行一遍自动清理，单位是秒。从上一次清理过后开始计时 */
        public int cullDelay = 60;

        /** 每次自动清理几个游戏对象。 */
        public int cullMaxPerPass = 5;




        /** 是否打印日志信息 */
        public bool logMessages = true;

        /** 强制关闭打印日志 */
        internal bool forceLoggingSilent = false;
        #endregion




        /// <summary>
        /// 已经预加载
        /// 在PreloadInstances（）方法中用到，用来判断是否需要执行预加载
        /// </summary>
        private bool _preloaded = false;
        internal bool preloaded
        {
            get { return this._preloaded; }
            private set { this._preloaded = value; }
        }


        virtual internal void SelfDestruct()
        {
            
        }


    }
}