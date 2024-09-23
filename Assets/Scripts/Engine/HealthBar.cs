using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider hpBar;
    [SerializeField] private Slider manaBar;
    [SerializeField] private Slider shieldsBar;
    
    public void UpdateHealthBar(int hp, int maxHp)
    {
        hpBar.value = hp * 1f / maxHp;
    }

    public void UpdateShieldsBar(int shield, int maxHp)
    {
        shieldsBar.value = 3 * shield * 1f / maxHp;
    }

    public void UpdateManaBar(int mana, int maxMana)
    {
        manaBar.value = mana * 1f / maxMana;
    }
}
