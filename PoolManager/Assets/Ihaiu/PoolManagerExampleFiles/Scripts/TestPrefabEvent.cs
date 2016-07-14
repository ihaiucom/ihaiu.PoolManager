using UnityEngine;
using System.Collections;
using Ihaius;

public class TestPrefabEvent : MonoBehaviour {

    public void SetArg(params object[] args)
    {
        Debug.LogFormat("TestPrefabEvent.SetArg() {0} args.Length={1}", gameObject, args.Length);
    }

    public void OnSpawned(PrefabPool pool)
    {
        Debug.LogFormat("TestPrefabEvent.OnSpawned() {0}  pool={1}" , gameObject, pool);
    }


    public void OnDespawned(PrefabPool pool)
    {
        Debug.LogFormat("TestPrefabEvent.OnDespawned() {0}  pool={1}" , gameObject, pool);
    }
}
