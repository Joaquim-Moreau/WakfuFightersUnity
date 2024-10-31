using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Boost Effect", menuName = "Effect/Boost Effect")]
public class BoostEffect : DurableEffect
{
    [SerializeField] private Stat _stat;
    [SerializeField] private int _amount;
    
    public override void Apply(Entity target)
    {
        var boost = new BoostEffectObject(_stat, _amount, Duration, cleanAble, particleSystem, Caster);
        boost.Apply(target);
    }

    private void Boost(Entity target, int amount)
    {
        switch (_stat)
        {
            case Stat.Damage:
                target.DamageAmp += amount;
                break;
            case Stat.Durability:
                target.DurabilityAmp += amount;
                break;
            case Stat.RecievedHeal:
                target.ReceivedHealAmp += amount;
                break;
            case Stat.Range:
                target.BonusRange += amount;
                break;
            case Stat.PhyDmg:
                target.BonusPhyDmg += amount;
                break;
            case Stat.MagDmg:
                target.BonusMagDmg += amount;
                break;
            case Stat.PushDmg:
                target.BonusPushDmg += amount;
                break;
            case Stat.CritChance:
                target.BonusCritChance += amount;
                break;
            case Stat.CritDmg:
                target.BonusCritDmg += amount;
                break;
            case Stat.LifeSteal:
                target.BonusLifeSteal += amount;
                break;
            case Stat.AttackSpeed:
                target.BonusAttackSpeed += amount;
                break;
            case Stat.PhyRes:
                target.BonusPhyRes += amount;
                break;
            case Stat.MagRes:
                target.BonusMagRes += amount;
                break;
            case Stat.Heal:
                target.BonusHeal += amount;
                break;
            case Stat.Tenacity:
                target.BonusTenacity += amount;
                break;
            case Stat.MovementSpeed:
                target.BonusMovementSpeed += amount;
                break;
        }
    }
}
