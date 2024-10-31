using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InterfaceBarManager : MonoBehaviour
{
    private Player _player;
    [SerializeField] private BattleSystem battleSystem;
    [SerializeField] private Camera mainCamera;
    
    [SerializeField] private Slider _hpBar;
    [SerializeField] private Slider _manaBar;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI manaText;
    
    [SerializeField] private Sprite[] sprites;
    
    [Header("Tooltip objects")]
    public GameObject tooltipBox; 
    public TextMeshProUGUI tooltipText;
    public TextMeshProUGUI spellNameText;
    public TextMeshProUGUI spellCostText;
    
    [Header("Endgame screen")] 
    [SerializeField] private Image endScreen;

    [SerializeField] private TextMeshProUGUI victoryText;
    [SerializeField] private TextMeshProUGUI defeatText;
    [SerializeField] private GameObject resetButton;
    
    [Header("BotSpawners")] 
    [SerializeField] private List<BotSpawner> botSpawners;

    private bool _bossExists;

    void Start()
    {
        _player = MainSceneManager.player.GetComponent<Player>();
        _player.onDeath.AddListener(LoadDefeatScreen);
        battleSystem.onBossSpawn?.AddListener(SpawnBoss);
        
        mainCamera?.transform.SetParent(_player.transform);
        Image playerSprite = transform.Find("Player Sprite").gameObject.GetComponent<Image>();
        playerSprite.sprite = sprites[(int)SelectionScreenData.ChosenClass];
        
        tooltipBox.SetActive(false);
        endScreen.enabled = false;
        victoryText.enabled = false;
        defeatText.enabled = false;
        resetButton.SetActive(false);
        _bossExists = false;
    }

    void Update()
    {
        _hpBar.value = _player.hp * 1f / _player.maxHP;
        hpText.text = _player.hp + " / " + _player.maxHP;
        _manaBar.value = _player.mana * 1f / _player.maxMana;
        manaText.text = _player.mana + " / " + _player.maxMana;
        
        tooltipBox.transform.position = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        tooltipBox.transform.position = new Vector3(tooltipBox.transform.position.x, 
            tooltipBox.transform.position.y, 0);
    }

    private void SpawnBoss()
    {
        battleSystem.activeBoss.GetComponent<Boss>()?.onDeath.AddListener(LoadVictoryScreen);
        _bossExists = true;
    }
    
    public void ShowTooltip(string tooltip, string nameSpell, string cost, string cooldown)
    {
        if (endScreen.enabled) return;
        tooltipBox.SetActive(true);
        tooltipText.text = tooltip;
        spellNameText.text = nameSpell;
        spellCostText.text = $"Cost : {cost}, CD : {cooldown}";
    }

    public void HideTooltip()
    {
        if (endScreen.enabled) return;
        tooltipBox.SetActive(false);
        tooltipText.text = String.Empty;
        spellNameText.text = String.Empty;
        spellCostText.text = String.Empty;
        
    }
    
    public void LoadDefeatScreen()
    {
        endScreen.enabled = true;
        defeatText.enabled = true;
        resetButton.SetActive(true);
    }

    public void LoadVictoryScreen()
    {
        endScreen.enabled = true;
        victoryText.enabled = true;
        resetButton.SetActive(true);
    }

    public void ResetInterface()
    {
        tooltipBox.SetActive(false);
        endScreen.enabled = false;
        victoryText.enabled = false;
        defeatText.enabled = false;
        resetButton.SetActive(false);
        
        foreach (var botSpawner in botSpawners)
        {
            botSpawner.gameObject.SetActive(true);

            foreach (var bot in botSpawner.activeBots)
            {
                Destroy(bot);
            }
        }
        
        ObjectPooler.Reset();
        mainCamera?.transform.SetParent(null);
        _player.onDeath.RemoveListener(LoadDefeatScreen);
        if (_bossExists)
        {
            battleSystem.activeBoss?.GetComponent<Boss>()?.onDeath.RemoveListener(LoadVictoryScreen);
        }
        MainSceneManager.ResetGame();
        SceneManager.LoadScene("SelectionScene");
    }
}
