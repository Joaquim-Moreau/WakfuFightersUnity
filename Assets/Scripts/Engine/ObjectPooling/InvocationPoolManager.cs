using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvocationPoolManager : MonoBehaviour
{
    [System.Serializable]
    public class SummonPool
    {
        public SummonType name;
        public GameObject prefab;
        public int size;
    }

    public List<SummonPool> pools;
    private Dictionary<SummonType, Queue<GameObject>> _summonPoolDictionary;
    
    void Start()
    {
        _summonPoolDictionary = new Dictionary<SummonType, Queue<GameObject>>();

        foreach (SummonPool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, transform);
          
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            _summonPoolDictionary.Add(pool.name, objectPool);
        }

        ObjectPooler.InitializeSummonPool(_summonPoolDictionary);
    }
}
