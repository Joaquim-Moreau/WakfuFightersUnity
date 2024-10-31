using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New heal Effect", menuName = "Effect/Heal Effect")]
public class HealEffect : Effect
{
    [SerializeField] private int _baseHeal;
    [SerializeField] private float _healRatio;
    
    private float _totalHeal;

    public override void Prepare(Entity caster, Entity target)
    {
        Caster = caster;
        _totalHeal = (int)(_baseHeal + _healRatio * caster.GetHeal());
    }
    
    public override void Apply(Entity target)
    {
        if (target.CannotBeHealed())
        {
            return;
        }
        target.HealHp(_totalHeal);
    }
}
