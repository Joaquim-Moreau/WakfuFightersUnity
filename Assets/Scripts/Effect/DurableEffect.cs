using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DurableEffect : Effect
{
    [SerializeField] protected float Duration;
    [SerializeField] protected ParticleSystem particleSystem;
    
    protected ParticleSystem instance = null;
    protected float DurationTimer;

    public override void Prepare(Entity caster, Entity target)
    {
        Caster = caster;
        DurationTimer = Duration;
    }
    
    public void UpdateDuration()
    {
        DurationTimer -= Time.deltaTime;
    }

    public bool IsExpired()
    {
        return DurationTimer <= 0f;
    }

    public void Cleanse()
    {
        DurationTimer = 0f;
    }

    protected void ApplyParticle(Entity target)
    {
        if (particleSystem)
        {
            instance = Instantiate(particleSystem, target.transform.position, Quaternion.identity);
            instance.transform.SetParent(target.transform);

            if (target.side == Side.Green)
            {
                instance.gameObject.layer = 0;
                foreach (Transform child in instance.gameObject.transform)
                {
                    child.gameObject.layer = 0;
                }
            }
            else
            {
                instance.gameObject.layer = target.visibleToOpponent ? 0 : 7;
                foreach (Transform child in instance.gameObject.transform)
                {
                    child.gameObject.layer = target.visibleToOpponent ? 0 : 7;
                }
            }
            
            instance.transform.localPosition = Vector3.zero;
            instance.transform.localRotation = Quaternion.identity;
        }
        else
        {
            instance = null;
        }
    }

    protected void RemoveParticle()
    {
        if (instance is null) return;
        
        instance.Stop();
        instance.Clear();
        Destroy(instance.gameObject);
        instance = null;
    }
}
