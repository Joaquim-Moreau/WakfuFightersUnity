using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpellPoolManager : MonoBehaviour
{
    [System.Serializable]
    public class SpellPool
    {
        public SpellName name;
        public GameObject prefab;
        public int size;
    }

    public List<SpellPool> pools;
    private Dictionary<SpellName, Queue<GameObject>> _spellPoolDictionary;
    
    void Start()
    {
        _spellPoolDictionary = new Dictionary<SpellName, Queue<GameObject>>();

        foreach (SpellPool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            _spellPoolDictionary.Add(pool.name, objectPool);
        }
        ObjectPooler.InitializeSpellPool(_spellPoolDictionary);
    }
}
