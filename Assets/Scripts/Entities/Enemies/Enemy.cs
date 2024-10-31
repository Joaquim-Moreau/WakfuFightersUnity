using UnityEngine.Events;

public class Enemy : Entity
{
    protected override void Die()
    {
        BattleSystem.AddKillCount();
        base.Die();
    }

    public override void HandleSpellLaunch(SpellName spellName)
    {
        if (spellName == GetAutoAttack().spellName)
        {
            _Animator.SetTrigger("Attack");
        }
    }
}
