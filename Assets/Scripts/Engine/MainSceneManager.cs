using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject[] classes;
    [SerializeField] private GameObject[] spells;
    [SerializeField] private Sprite[] sprites;

    [NonSerialized] public static GameObject player;
    
    private Vector3 _startPosition = new Vector3(11f, 2.7f, 0f);
    
    void Awake()
    {
        player = Instantiate(classes[(int)SelectionScreenData.ChosenClass]);
        player.transform.position = _startPosition;
        
        Instantiate(spells[(int)SelectionScreenData.ChosenClass]);
    }
    
    public static void ResetGame()
    {
        player.GetComponent<Player>()?.ResetCoolDowns();
        Destroy(player);
    }
}
