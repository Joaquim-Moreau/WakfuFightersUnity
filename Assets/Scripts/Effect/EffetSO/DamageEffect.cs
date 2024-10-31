using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Damage Effect", menuName = "Effect/Damage Effect")]
public class DamageEffect : Effect
{
    [SerializeField] private int _baseDmg;
    [SerializeField] private float _phyRatio;
    [SerializeField] private float _magRatio;
    [SerializeField] private float _baseLifeSteal;
    [SerializeField] private DmgType _type;
    [SerializeField] private bool _canCrit = false;

    private float _totalDamage;

    public override void Prepare(Entity caster, Entity target)
    {
        Caster = caster;
        _totalDamage = _baseDmg + (_phyRatio * Mathf.Max(0, caster.GetPhyDmg())) + (_magRatio * Mathf.Max(0, caster.GetMagDmg()));

        if (_canCrit)
        {
            if (Random.Range(0, 100) < caster.GetCritChance())
            {
                _totalDamage *= caster.GetCritDmg();
            }
        }

        _totalDamage *= (100f + caster.GetDmgMultiplier())/100f;
    }
    
    public override void Apply(Entity target)
    {
        if (target.CannotTakeDamage())
        {
            return;
        }

        if (Caster.CannotDealDamage())
        {
            return;
        }
        
        float actualDmg = target.TakeDmg(_totalDamage, _type);

        float stolenHp = actualDmg * (_baseLifeSteal + Caster.GetLifeSteal()) / 100f;
        Caster.HealHp(stolenHp);
    }
}
