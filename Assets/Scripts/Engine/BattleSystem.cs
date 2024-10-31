using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class BattleSystem : MonoBehaviour
{
    public UnityEvent onBossSpawn;
    public static int numberOfKills;
    
    public GameObject boss;
    
    //private static GameObject[] _staticEnemies; 
    public GameObject activeBoss;

    private bool _alreadySpawned;

    void Start()
    {
        numberOfKills = 0;
        _alreadySpawned = false;
    }

    public void SpawnBoss()
    {
        activeBoss = Instantiate(boss);
        activeBoss.GetComponent<Boss>()?.onDeath.AddListener(ResetCount);
        activeBoss.SetActive(true);
        onBossSpawn?.Invoke();
    }

    private void Update()
    {
        if (!_alreadySpawned && numberOfKills >= 7)
        {
            SpawnBoss();
            _alreadySpawned = true;
        }
    }

    public static void AddKillCount()
    {
        numberOfKills += 1;
    }

    public void ResetCount()
    {
        numberOfKills = 0;
        activeBoss.GetComponent<Boss>()?.onDeath.RemoveListener(ResetCount);
    }
}
