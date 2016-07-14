using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Ihaius
{
    public class PoolManager : MonoBehaviour
    {
        /** PoolGroup字典 */
        public static PoolGroupDict groups = new PoolGroupDict();


        private static PoolManager _instance;
        internal static PoolManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = GameObject.Find("GameManager");
                    if (go == null)
                    {
                        go = new GameObject("PoolManager");
                    }

                    _instance = go.AddComponent<PoolManager>();
                }
                return _instance;
            }
        }

        void Awake()
        {
            if (_instance != null)
            {
                Destroy(this);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }
}