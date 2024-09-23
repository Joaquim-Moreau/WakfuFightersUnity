using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InterfaceBarManager : MonoBehaviour
{
    private Player _player;
    
    [SerializeField] private Slider _hpBar;
    [SerializeField] private Slider _manaBar;
    [SerializeField] private TextMeshProUGUI hpText;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = MainSceneManager.instance.player.GetComponent<Player>();
        //_hpBar = transform.Find("HpBar").GetComponent<Slider>();
        //_manaBar = transform.Find("ManaBar").GetComponent<Slider>();
        //hpText = transform.Find("HpBar").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        _hpBar.value = _player.hp * 1f / _player.maxHP;
        hpText.text = _player.hp + " / " + _player.maxHP;
        _manaBar.value = _player.mana * 1f / _player.maxMana;
    }
}
