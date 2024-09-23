using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;

public class MainSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject[] classes;
    [SerializeField] private GameObject[] spells;
    [SerializeField] private Sprite[] sprites;

    [NonSerialized] public GameObject player;
    public static MainSceneManager instance;
    public Camera mainCamera;
    
    [Header("Tooltip objects")]
    public GameObject tooltipBox; 
    public TextMeshProUGUI tooltipText;
    public TextMeshProUGUI spellNameText;
    public TextMeshProUGUI spellCostText;
    
    
    void Awake()
    {
        if (instance is not null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        
        player = Instantiate(classes[(int)SelectionScreenData.ChosenClass]);
        mainCamera.transform.SetParent(player.transform);
        
        Instantiate(spells[(int)SelectionScreenData.ChosenClass]);
        
        Image playerSprite = transform.Find("Player Sprite").gameObject.GetComponent<Image>();
        playerSprite.sprite = sprites[(int)SelectionScreenData.ChosenClass];
    }

    private void Start()
    {
        tooltipBox.SetActive(false);
    }

    private void Update()
    {
        tooltipBox.transform.position = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        tooltipBox.transform.position = new Vector3(tooltipBox.transform.position.x, 
                                                    tooltipBox.transform.position .y, 0);
    }

    public void ShowTooltip(string tooltip, string nameSpell, string cost, string cooldown)
    {
        tooltipBox.SetActive(true);
        tooltipText.text = tooltip;
        spellNameText.text = nameSpell;
        spellCostText.text = $"Cost : {cost}, CD : {cooldown}";
    }

    public void HideTooltip()
    {
        tooltipBox.SetActive(false);
        tooltipText.text = String.Empty;
        spellNameText.text = String.Empty;
        spellCostText.text = String.Empty;
    }
}
