using UnityEngine;


public class BoostEffectObject : DurableEffectObject
{
    private Stat _stat;
    private int _amount;
    
    public BoostEffectObject(Stat stat, int amount, float duration, bool cleanable, ParticleSystem particle, 
        Entity caster) : base(duration, cleanable, particle, caster)
    {
        _stat = stat;
        _amount = amount;
    }
    
    public override void Apply(Entity target)
    {
        target.currentEffects.Add(this);
        Boost(target, _amount);
        ApplyParticle(target);
    }

    public override void Remove(Entity target)
    {
        Boost(target, -_amount);
        RemoveParticle();
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
