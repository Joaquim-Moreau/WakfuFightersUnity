using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> bots;
    [SerializeField] private Transform botParentTransform;

    public List<GameObject> activeBots;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var entity in bots)
            {
                var bot = Instantiate(entity, botParentTransform);
                bot.SetActive(true);
                activeBots.Add(bot);
            }
            gameObject.SetActive(false);
        }
    }
}
