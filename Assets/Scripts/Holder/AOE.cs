using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AOE : Holder
{
    [SerializeField] private float refreshPeriod;
    [SerializeField] private bool changingDirection = true;
    protected float _refresh;
    protected bool _touchingEntity;
    
    public override void Init(Entity caster, Vector3 launchPosition)
    {
        base.Init(caster, launchPosition);
        transform.position = launchPosition;
        _refresh = 0f;
        _touchingEntity = false;
        if (changingDirection) InitDirection(caster);
        RotateObject();
        
        StopAllCoroutines();
        StartCoroutine(ManageDelay());
        StartCoroutine(ManageLifeTime());
    }
    
    void Update()
    {
        if (!ReadyToApplyEffects) return; 
        
        if (_refresh > 0f)
        {
            _refresh -= Time.deltaTime;
        }
        else
        {
            AlreadyHit = new List<Entity>(); 
            _touchingEntity = false;
        }
    }
    
    protected virtual void OnTriggerStay2D(Collider2D other)
    {
        if (!ReadyToApplyEffects) return;
        
        if (other.CompareTag("Entity") || other.CompareTag("Player"))
        {
            Entity target = other.GetComponent<Entity>();
            if (!spellData.CanSpellAffect(target)) return;
            if (AlreadyHit.Contains(target)) return;
      
            ApplyEffects(target);
            AddToHit(target);

            if (!_touchingEntity)
            {
                _refresh += refreshPeriod;
                _touchingEntity = true;
            }
        }
    }
}
