using UnityEngine;
using System.Collections;
using Ihaius;
using System.Collections.Generic;

namespace PoolManagerExampleFiles
{
    public class UnitData : ScriptableObject
    {
        private static int ID = 0;
        public int id;

        public UnitData()
        {
            id = ID ++;
        }


        public override string ToString()
        {
            return string.Format("[UnitData] id={0}", id);
        }
    }
    public class ClassExample : MonoBehaviour {

       

        ScriptableObjectPool<UnitData> unitDataPool = new ScriptableObjectPool<UnitData>();
        public int count;
        public int spawned;
        public int despawned;
        public string status = "";
        public string current = "";
        public List<UnitData> unitDatas = new List<UnitData>();
    	void Start ()
        {
    //        StartCoroutine(TestCache());
    //        StartCoroutine(TestLimit());
            StartCoroutine(TestCull());
    	}

        void Update()
        {
            count = unitDataPool.totalCount;
            spawned = unitDataPool.spawned.Count;
            despawned = unitDataPool.despawned.Count;
        }


        public IEnumerator TestCull()
        {

            // 缓存池这个Prefab的预加载数量。意思为一开始加载的数量！
            unitDataPool.preloadAmount = 3;
            // 如果勾选表示缓存池所有的gameobject可以“异步”加载。
            unitDataPool.preloadTime = true;
            // 每几帧加载一个。
            unitDataPool.preloadFrames = 2;
            // 延迟多久开始加载。
            unitDataPool.preloadDelay = 0;


            // 是否开启对象实例化的限制功能。
            unitDataPool.limitInstances = false;
            // 限制实例化Prefab的数量，也就是限制缓冲池的数量，它和上面的preloadAmount是有冲突的，如果同时开启则以limitAmout为准。
            unitDataPool.limitAmount = 5;
            // 如果我们限制了缓存池里面只能有10个Prefab，如果不勾选它，那么你拿第11个的时候就会返回null。如果勾选它在取第11个的时候他会返回给你前10个里最不常用的那个。
            unitDataPool.limitFIFO = true;

            // 是否开启缓存池智能自动清理模式
            unitDataPool.cullDespawned = true;
            // 缓存池自动清理，但是始终保留几个对象不清理。
            unitDataPool.cullAbove = 5;
            // 每过多久执行一遍自动清理，单位是秒。从上一次清理过后开始计时
            unitDataPool.cullDelay = 2;
            // 每次自动清理几个游戏对象。
            unitDataPool.cullMaxPerPass = 2;

            // 是否打印日志信息
            unitDataPool.logMessages = true;


            PoolManager.groups.common.CreatePool<UnitData>(unitDataPool);
            status = "Init";

            yield return new WaitForSeconds(5);
            Debug.Log("-----Spawn----");
            status = "Spawn ";
            for(int j = 0; j < 10; j ++)
            {
                UnitData unitData = PoolManager.groups.common.Spawn<UnitData>();
                unitDatas.Add(unitData);
                Debug.LogFormat("[Spawn] {0}, {1}" , j, unitData);
                Debug.Log(unitDataPool);
                status = "Spawn " + j;
                current =unitData != null ?  unitData.ToString() : "null";
                yield return new WaitForSeconds(1);
            }

            yield return new WaitForSeconds(2);
            Debug.Log("-----Despawn----");
            for(int j = unitDatas.Count - 1; j >= 0; j --)
            {
                UnitData unitData = unitDatas[j];
                PoolManager.groups.common.Despawn<UnitData>(unitData);
                Debug.Log(unitDataPool);
                status = "Despawn " + j;
                current =unitData != null ?  unitData.ToString() : "null";
                yield return new WaitForSeconds(1);
            }
        }



        public IEnumerator TestLimit()
        {

            // 缓存池这个Prefab的预加载数量。意思为一开始加载的数量！
            unitDataPool.preloadAmount = 3;
            // 如果勾选表示缓存池所有的gameobject可以“异步”加载。
            unitDataPool.preloadTime = true;
            // 每几帧加载一个。
            unitDataPool.preloadFrames = 2;
            // 延迟多久开始加载。
            unitDataPool.preloadDelay = 0;


            // 是否开启对象实例化的限制功能。
            unitDataPool.limitInstances = true;
            // 限制实例化Prefab的数量，也就是限制缓冲池的数量，它和上面的preloadAmount是有冲突的，如果同时开启则以limitAmout为准。
            unitDataPool.limitAmount = 5;
            // 如果我们限制了缓存池里面只能有10个Prefab，如果不勾选它，那么你拿第11个的时候就会返回null。如果勾选它在取第11个的时候他会返回给你前10个里最不常用的那个。
            unitDataPool.limitFIFO = true;

            // 是否开启缓存池智能自动清理模式
            unitDataPool.cullDespawned = false;
            // 缓存池自动清理，但是始终保留几个对象不清理。
            unitDataPool.cullAbove = 5;
            // 每过多久执行一遍自动清理，单位是秒。从上一次清理过后开始计时
            unitDataPool.cullDelay = 2;
            // 每次自动清理几个游戏对象。
            unitDataPool.cullMaxPerPass = 2;

            // 是否打印日志信息
            unitDataPool.logMessages = true;


            PoolManager.groups.common.CreatePool<UnitData>(unitDataPool);
            status = "Init";

            yield return new WaitForSeconds(5);
            for(int i = 0; i < 20; i ++)
            {
                Debug.LogFormat("-----Spawn {0}----", i);
                status = "Spawn ";
                for(int j = 0; j < 10; j ++)
                {
                    UnitData unitData = PoolManager.groups.common.Spawn<UnitData>();
                    unitDatas.Add(unitData);
                    Debug.LogFormat("[Spawn] {0}, {1}" , j, unitData);
                    Debug.Log(unitDataPool);
                    status = "Spawn " + j;
                    current =unitData != null ?  unitData.ToString() : "null";
                    yield return new WaitForSeconds(1);
                }
                yield return new WaitForSeconds(5);


                Debug.Log("-----Despawn 1----");
                for(int j = unitDatas.Count - 1; j >= 0; j --)
                {
                    UnitData unitData = unitDatas[j];
                    PoolManager.groups.common.Despawn<UnitData>(unitData);
                    Debug.Log(unitDataPool);
                    status = "Despawn " + j;
                    current =unitData != null ?  unitData.ToString() : "null";
                    yield return new WaitForSeconds(1);
                }
                unitDatas.Clear();

                yield return new WaitForSeconds(2);
            }
        }



        public IEnumerator TestCache()
        {

            // 缓存池这个Prefab的预加载数量。意思为一开始加载的数量！
            unitDataPool.preloadAmount = 3;
            // 如果勾选表示缓存池所有的gameobject可以“异步”加载。
            unitDataPool.preloadTime = true;
            // 每几帧加载一个。
            unitDataPool.preloadFrames = 2;
            // 延迟多久开始加载。
            unitDataPool.preloadDelay = 0;

            // 是否打印日志信息
            unitDataPool.logMessages = true;


            PoolManager.groups.common.CreatePool<UnitData>(unitDataPool);
            status = "Init";

            yield return new WaitForSeconds(5);
            for(int i = 0; i < 20; i ++)
            {
                Debug.LogFormat("-----Spawn {0}----", i);
                status = "Spawn ";
                for(int j = 0; j < 10; j ++)
                {
                    UnitData unitData = PoolManager.groups.common.Spawn<UnitData>();
                    unitDatas.Add(unitData);
                    Debug.LogFormat("[Spawn] {0}, {1}" , j, unitData);
                    Debug.Log(unitDataPool);
                    status = "Spawn " + j;
                    current =unitData != null ?  unitData.ToString() : "null";
                    yield return new WaitForSeconds(1);
                }
                yield return new WaitForSeconds(5);


                Debug.Log("-----Despawn 1----");
                for(int j = unitDatas.Count - 1; j >= 0; j --)
                {
                    UnitData unitData = unitDatas[j];
                    PoolManager.groups.common.Despawn<UnitData>(unitData);
                    Debug.Log(unitDataPool);
                    status = "Despawn " + j;
                    current =unitData != null ?  unitData.ToString() : "null";
                    yield return new WaitForSeconds(1);
                }
                unitDatas.Clear();

                yield return new WaitForSeconds(2);
            }
        }



    }
}