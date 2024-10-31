using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    public float lifeTime;
    protected Entity Caster;

    [SerializeField] protected float delayBeforeSpawn;
    [SerializeField] protected SpellData spellData;

    protected bool ReadyToApplyEffects = false;
    void OnDisable()
    {
        StopAllCoroutines();
    }
    
    protected IEnumerator ManageLifeTime()
    {
        yield return new WaitForSeconds(lifeTime + delayBeforeSpawn);
        gameObject.SetActive(false);
    }
    
    protected IEnumerator ManageLifeTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }

    protected IEnumerator ManageDelay()
    {
        ReadyToApplyEffects = false;
        yield return new WaitForSeconds(delayBeforeSpawn);
        ReadyToApplyEffects = true;
    }
    
    public abstract void Init(Entity caster, Vector3 launchPosition);
    
    public abstract void Init(Entity caster, Entity target);
}
