using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    public float lifeTime;
    protected Entity Caster;

    [SerializeField] protected SpellData spellData;
    void OnDisable()
    {
        StopAllCoroutines();
    }
    
    protected IEnumerator ManageLifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }
    
    protected IEnumerator ManageLifeTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }
    
    public abstract void Init(Entity caster, Vector3 launchPosition);
    
    public abstract void Init(Entity caster, Entity target);
}
