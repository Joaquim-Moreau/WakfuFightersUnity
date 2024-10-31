using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boss : Entity
{
    public UnityEvent onDeath;
    protected override void Die()
    {
        StartCoroutine(PlayDeathAnim());
    }

    private IEnumerator PlayDeathAnim()
    {
        _Animator.SetTrigger("Teleport");
        yield return new WaitForSeconds(0.7f);
        onDeath?.Invoke();
        base.Die();
    }
    
    public override void HandleSpellLaunch(SpellName spellName)
    {
        if (spellName == GetAutoAttack().spellName)
        {
            _Animator.SetTrigger("Attack");
        } else if (spellName == SpellName.BossSpell)
        {
            _Animator.SetTrigger("Spell");
        } else if (spellName == SpellName.BossTeleport)
        {
            _Animator.SetTrigger("Teleport");
        }
    }
}
