using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;
using System.Linq;

public static class ObjectPooler
{
    private static Dictionary<SpellName, Queue<GameObject>> _spellPoolDictionary = new Dictionary<SpellName, Queue<GameObject>>();
    private static Dictionary<SummonType, Queue<GameObject>> _summonPoolDictionary = new Dictionary<SummonType, Queue<GameObject>>();
    
    public static void InitializeSpellPool(Dictionary<SpellName, Queue<GameObject>> spellPoolDict)
    {
        _spellPoolDictionary = _spellPoolDictionary.Concat(spellPoolDict).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    }
    
    public static void InitializeSummonPool(Dictionary<SummonType, Queue<GameObject>> summonPoolDict)
    {
        _summonPoolDictionary = summonPoolDict;
    }

    public static GameObject CreateSpell(SpellName name, Vector3 startPosition)
    {
        if (!_spellPoolDictionary.ContainsKey(name))
        {
            throw new ArgumentException("Type of spell doesn't exist");
        }
        
        GameObject spellToSpawn = _spellPoolDictionary[name].Dequeue();
        spellToSpawn.SetActive(true);
        spellToSpawn.transform.position = startPosition;
        spellToSpawn.transform.rotation = Quaternion.identity;
        
        _spellPoolDictionary[name].Enqueue(spellToSpawn);
        return spellToSpawn; // TODO : Change out type to Spell instead of gameObject
    }

    public static GameObject CreateInvocation(SummonType type, Vector3 startPosition)
    {
        if (!_summonPoolDictionary.ContainsKey(type))
        {
            throw new ArgumentException("Type of invocation doesn't exist");
        }
        
        GameObject summonToSpawn = _summonPoolDictionary[type].Dequeue();
        summonToSpawn.SetActive(true);
        summonToSpawn.transform.position = startPosition;
        summonToSpawn.transform.rotation = Quaternion.identity;
        summonToSpawn.GetComponent<Summon>()?.OnSummon();
        
        _summonPoolDictionary[type].Enqueue(summonToSpawn);
        return summonToSpawn;
    }

    public static void Reset()
    {
        _spellPoolDictionary = new Dictionary<SpellName, Queue<GameObject>>();
        _summonPoolDictionary = new Dictionary<SummonType, Queue<GameObject>>();
    }
}
