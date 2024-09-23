using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AOE : Holder
{
    [SerializeField] private float refreshPeriod;
    
    protected float _refresh;
    protected bool _touchingEntity;
    
    public override void Init(Entity caster, Vector3 launchPosition)
    {
        base.Init(caster, launchPosition);
        transform.position = launchPosition;
        _refresh = 0f;
        _touchingEntity = false;
        InitDirection(caster);
        RotateObject();
        
        StopAllCoroutines();
        StartCoroutine(ManageLifeTime());
    }
    
    // Update is called once per frame
    void Update()
    {
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
        if (other.CompareTag("Entity") || other.CompareTag("Player"))
        {
            Entity target = other.GetComponent<Entity>();
            if (!spellData.CanSpellAffect(target)) return;
            if (AlreadyHit.Contains(target)) return;
            // 
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
