using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dash : Holder
{
    [SerializeField] private float speed;
    private Rigidbody2D _rb;
    
    // Targeted
    private Entity _target = null;
    
    // Non targeted
    private float _duration;
    
    public override void Init(Entity caster, Vector3 launchPosition)
    {
        Caster = caster;
        _rb = Caster.GetComponent<Rigidbody2D>();
        transform.position = Caster.transform.position;
        
        Caster._actionManager.movementState = MovementState.Dashing;
        DashInDirection(Caster.transform.position, launchPosition);
    }
    
    public override void Init(Entity caster, Entity target)
    {
        Caster = caster;
        _rb = Caster.GetComponent<Rigidbody2D>();
        transform.position = Caster.transform.position;
        _target = target;
        
        Caster._actionManager.movementState = MovementState.Dashing;
        Caster.GetComponent<Collider2D>().isTrigger = true;
        Caster.GetComponent<NavMeshAgent>().enabled = false;
    }

    private void Update()
    {
        transform.position = Caster.transform.position;
        
        _duration -= Time.deltaTime;
        if (!spellData.targeted && _duration <= 0)
        {
            EndDash();
        }
    }

    private void FixedUpdate()
    {
        if (spellData.targeted)
        {
            Vector3 direction = Statics.GetDirection(Caster.transform.position, _target.transform.position);
            
            Direction = direction;
            _rb.velocity = direction * speed;
        }
    }

    private void DashInDirection(Vector3 startingPoint, Vector3 endPoint)
    {
        Caster.GetComponent<Collider2D>().isTrigger = true;
        Caster.GetComponent<NavMeshAgent>().enabled = false;
        
        float distance = Statics.GetDistance(startingPoint, endPoint);
        Vector3 direction = Statics.GetDirection(startingPoint, endPoint);

        _duration = distance / speed;
        _rb.velocity = direction * speed;
        Direction = direction;
        
        Caster._actionManager.movementState = MovementState.Dashing;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Entity") || other.CompareTag("Player"))
        {
            Entity target = other.GetComponent<Entity>();
            if (spellData.targeted)
            {
                if (target == _target)
                {
                    ApplyEffects(target);
                    EndDash();
                }
            }
            else
            {
                if (!spellData.CanSpellAffect(target)) return;
                if (AlreadyHit.Contains(target)) return;
                ApplyEffects(target);
                AddToHit(target);
            }
        }
    }
 
    private void EndDash()
    {
        _rb.velocity = Vector2.zero;
        Caster.GetComponent<Collider2D>().isTrigger = false;
        Caster.GetComponent<NavMeshAgent>().enabled = true;
        Caster._actionManager.movementState = MovementState.Stopped;
        gameObject.SetActive(false);
    }
}
