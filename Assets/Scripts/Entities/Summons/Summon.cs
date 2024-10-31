using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon : Entity
{
    public void OnSummon()
    {
        hp = maxHP;
    }
}
