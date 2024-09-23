using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> bots;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var entity in bots)
            {
                entity.SetActive(true);
            }
            gameObject.SetActive(false);
        }
    }
}
