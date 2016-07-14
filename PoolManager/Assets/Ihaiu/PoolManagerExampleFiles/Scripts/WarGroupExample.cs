using UnityEngine;
using System.Collections;
using Ihaius;
using System.Collections.Generic;

public class WarGroupExample : MonoBehaviour {

    public Transform prefab;
    public PrefabPool pool = new PrefabPool();
    public PoolGroup group;


    public int count;
    public int spawned;
    public int despawned;
    public string status = "";
    public Transform current;
    public List<Transform> list = new List<Transform>();

    void Start ()
    {
        StartCoroutine(TestCache());
    }

    public void DestoryGroup()
    {
        StopAllCoroutines();

        if (group != null)
        {
            group.Destroy();
            group = null;
        }
    }

    void Update()
    {
        count = pool.totalCount;
        spawned = pool.spawned.Count;
        despawned = pool.despawned.Count;
    }

    public IEnumerator TestCache()
    {

        group = PoolManager.groups.Create("WarPoolGroup");



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
        pool.logMessages = false;


        group.CreatePool(pool);
        status = "Init";

        yield return new WaitForSeconds(1);
        for(int i = 0; i < 20; i ++)
        {
            Debug.LogFormat("-----Spawn {0}----", i);
            status = "Spawn ";
            for(int j = 0; j < 10; j ++)
            {
                Transform item = group.Spawn(prefab);
                item.position = Vector3.forward * (j + 0.5f);
                list.Add(item);
                Debug.LogFormat("[Spawn] {0}, {1}" , j, item);
                Debug.Log(pool);
                status = "Spawn " + j;
                current =item;
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(1);


            Debug.Log("-----Despawn 1----");
            for(int j = list.Count - 1; j >= 0; j --)
            {
                Transform item = list[j];
                group.Despawn(item);
                Debug.Log(pool);
                status = "Despawn " + j;
                current =item;
                yield return new WaitForSeconds(0.2f);
            }
            list.Clear();

            yield return new WaitForSeconds(1);
        }
    }



}
