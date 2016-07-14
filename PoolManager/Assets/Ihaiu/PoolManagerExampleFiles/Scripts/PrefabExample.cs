using UnityEngine;
using System.Collections;
using Ihaius;
using System.Collections.Generic;

namespace PoolManagerExampleFiles
{
    
    public class PrefabExample : MonoBehaviour {

        public Transform prefab;
        public PrefabPool pool = new PrefabPool();


        public int count;
        public int spawned;
        public int despawned;
        public string status = "";
        public Transform current;
        public List<Transform> list = new List<Transform>();

    	void Start ()
        {
            StartCoroutine(TestCache());
//            StartCoroutine(TestCull());
    	}

        void Update()
        {
            count = pool.totalCount;
            spawned = pool.spawned.Count;
            despawned = pool.despawned.Count;
        }


        public IEnumerator TestCull()
        {


            pool.prefab = prefab;

            // 缓存池这个Prefab的预加载数量。意思为一开始加载的数量！
            pool.preloadAmount = 3;
            // 如果勾选表示缓存池所有的gameobject可以“异步”加载。
            pool.preloadTime = true;
            // 每几帧加载一个。
            pool.preloadFrames = 2;
            // 延迟多久开始加载。
            pool.preloadDelay = 0;


            // 是否开启对象实例化的限制功能。
            pool.limitInstances = false;
            // 限制实例化Prefab的数量，也就是限制缓冲池的数量，它和上面的preloadAmount是有冲突的，如果同时开启则以limitAmout为准。
            pool.limitAmount = 5;
            // 如果我们限制了缓存池里面只能有10个Prefab，如果不勾选它，那么你拿第11个的时候就会返回null。如果勾选它在取第11个的时候他会返回给你前10个里最不常用的那个。
            pool.limitFIFO = true;

            // 是否开启缓存池智能自动清理模式
            pool.cullDespawned = true;
            // 缓存池自动清理，但是始终保留几个对象不清理。
            pool.cullAbove = 5;
            // 每过多久执行一遍自动清理，单位是秒。从上一次清理过后开始计时
            pool.cullDelay = 2;
            // 每次自动清理几个游戏对象。
            pool.cullMaxPerPass = 2;

            // 是否打印日志信息
            pool.logMessages = true;


            PoolManager.groups.common.CreatePool(pool);
            status = "Init";

            yield return new WaitForSeconds(5);
            for(int i = 0; i < 20; i ++)
            {
                Debug.LogFormat("-----Spawn {0}----", i);
                status = "Spawn ";
                for(int j = 0; j < 10; j ++)
                {
                    Transform item = PoolManager.groups.common.Spawn(prefab);
                    item.position = Vector3.forward * (j + 0.5f);
                    list.Add(item);
                    Debug.LogFormat("[Spawn] {0}, {1}" , j, item);
                    Debug.Log(pool);
                    status = "Spawn " + j;
                    current =item;
                    yield return new WaitForSeconds(1);
                }
                yield return new WaitForSeconds(5);


                Debug.Log("-----Despawn 1----");
                for(int j = list.Count - 1; j >= 0; j --)
                {
                    Transform item = list[j];
                    PoolManager.groups.common.Despawn(item);
                    Debug.Log(pool);
                    status = "Despawn " + j;
                    current =item;
                    yield return new WaitForSeconds(1);
                }
                list.Clear();

                yield return new WaitForSeconds(2);
            }
        }


        public IEnumerator TestCache()
        {

            pool.prefab = prefab;

            // 缓存池这个Prefab的预加载数量。意思为一开始加载的数量！
            pool.preloadAmount = 3;
            // 如果勾选表示缓存池所有的gameobject可以“异步”加载。
            pool.preloadTime = true;
            // 每几帧加载一个。
            pool.preloadFrames = 2;
            // 延迟多久开始加载。
            pool.preloadDelay = 0;

        

            // 是否打印日志信息
            pool.logMessages = true;


            PoolManager.groups.common.CreatePool(pool);
            status = "Init";

            yield return new WaitForSeconds(5);
            for(int i = 0; i < 20; i ++)
            {
                Debug.LogFormat("-----Spawn {0}----", i);
                status = "Spawn ";
                for(int j = 0; j < 10; j ++)
                {
                    Transform item = PoolManager.groups.common.Spawn(prefab);
                    item.position = Vector3.forward * (j + 0.5f);
                    list.Add(item);
                    Debug.LogFormat("[Spawn] {0}, {1}" , j, item);
                    Debug.Log(pool);
                    status = "Spawn " + j;
                    current =item;
                    yield return new WaitForSeconds(1);
                }
                yield return new WaitForSeconds(5);


                Debug.Log("-----Despawn 1----");
                for(int j = list.Count - 1; j >= 0; j --)
                {
                    Transform item = list[j];
                    PoolManager.groups.common.Despawn(item);
                    Debug.Log(pool);
                    status = "Despawn " + j;
                    current =item;
                    yield return new WaitForSeconds(1);
                }
                list.Clear();

                yield return new WaitForSeconds(2);
            }
        }



    }
}