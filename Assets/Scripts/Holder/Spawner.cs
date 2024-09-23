using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Spell
{
    [SerializeField] private SummonType summonType;
    private bool _summonDone;
    
    public override void Init(Entity caster, Vector3 launchPosition)
    {
        Initialize(summonType, caster, launchPosition);
    }
    
    public override void Init(Entity caster, Entity target) {}
    
    public void Initialize(SummonType type, Entity caster, Vector3 launchPosition)
    {
        summonType = type;
        Caster = caster;
        transform.position = launchPosition;
        _summonDone = false;
      
        StopAllCoroutines();
        StartCoroutine(Summon());
    }

    private void Update()
    {
        if (_summonDone)
        {
            gameObject.SetActive(false);
        }
    }

    private IEnumerator Summon()
    {
        yield return new WaitForSeconds(1f);
        GameObject summon = ObjectPooler.CreateInvocation(summonType, transform.position);
        _summonDone = true;
    }
}

public enum SummonType
{
    Graine,
    Arbre,
    PoupeeSacrifiee,
    PoupeeGonflable,
    PoupeeBloqueuse,
    PoupeeMaudite,
    Double,
    Balise,
    Lance,
    Bombe,
    Harponneuse,
    Gardienne,
    Tactirelle,
    Bouftou,
    Crapaud,
    Tofu,
    Dragonnet
}
