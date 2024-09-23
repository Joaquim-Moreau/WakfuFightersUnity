using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class SpellInterface : MonoBehaviour
{
    private enum WhichSpell
    {
        Space,
        A,
        Z,
        E,
        R
    }
    [SerializeField] private WhichSpell whichSpell;
    
    private List<SpellData> _spellData;
    
    private Image _spellIcon;
    private Image _coolDown;
    private TMP_Text _timer;
    private String _description = String.Empty;
    
    // Start is called before the first frame update
    void Start()
    {
        _spellIcon = transform.Find("SpellIcon").gameObject.GetComponent<Image>();
        _coolDown = transform.Find("CoolDownImage").gameObject.GetComponent<Image>();
        _timer = transform.Find("TextCoolDown").gameObject.GetComponent<TMP_Text>();

        switch (whichSpell)
        {
            case WhichSpell.Space:
                _spellData = MainSceneManager.instance.player.GetComponent<Player>().spellBook.SpaceSpell;
                break;
            case WhichSpell.A:
                _spellData = MainSceneManager.instance.player.GetComponent<Player>().spellBook.ASpell;
                break;
            case WhichSpell.Z:
                _spellData = MainSceneManager.instance.player.GetComponent<Player>().spellBook.ZSpell;
                break;
            case WhichSpell.E:
                _spellData = MainSceneManager.instance.player.GetComponent<Player>().spellBook.ESpell;
                break;
            case WhichSpell.R:
                _spellData = MainSceneManager.instance.player.GetComponent<Player>().spellBook.RSpell;
                break;
            default:
                break;
        }

        _spellIcon.sprite = _spellData[0].spellIcon;
        _coolDown.fillAmount = 0f;
        _timer.gameObject.SetActive(false);
        _description = _spellData[0].description;
    }

    // Update is called once per frame
    void Update()
    {
        if (_spellData[0].IsReady())
        {
            _coolDown.fillAmount = 0f;
            _timer.gameObject.SetActive(false);
        }
        else
        {
            _timer.gameObject.SetActive(true);
            _coolDown.fillAmount = _spellData[0].GetCoolDownTimerPercent();
            _timer.text = Mathf.Floor(_spellData[0]._cooldownTimer).ToString();
        }
    }

    private void OnMouseEnter()
    {
        MainSceneManager.instance.ShowTooltip(_description, 
                                              _spellData[0].spellName.ToString(), 
                                              _spellData[0].ManaCost.ToString(), 
                                              _spellData[0].CoolDown.ToString());
    }

    private void OnMouseExit()
    {
        MainSceneManager.instance.HideTooltip();
    }
}
