using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BattleSystem : MonoBehaviour
{
    public static int numberOfKills;
    
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject boss;
    
    //private static GameObject[] _staticEnemies; 
    private static GameObject _staticBoss;
    
    // Start is called before the first frame update
    void Start()
    {
        numberOfKills = 0;
        //_staticEnemies = enemies;
        _staticBoss = boss;
    }

    public static void SpawnBoss()
    {
        if (numberOfKills >= 7)
        {
            Debug.Log("Spawning Boss");
            _staticBoss.SetActive(true);
        }
    }
}
