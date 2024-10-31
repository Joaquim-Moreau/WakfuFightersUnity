using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Trap : Holder
{
    public Side triggerSide;
    [SerializeField] private float delayBeforeButtonActive;
    [SerializeField] private float activatedLifeTime;
    [SerializeField] private float refreshPeriod;
    
    private float _refresh;
    private bool _touchingEntity;
    public bool IsTriggered { get; private set; }
    public bool IsButtonActive { get; private set; }
    
    public override void Init(Entity caster, Vector3 launchPosition)
    {
        base.Init(caster, launchPosition);
        transform.position = launchPosition;
        _refresh = 0f;
        _touchingEntity = false;
        IsTriggered = false;
        IsButtonActive = false;
        
        StopAllCoroutines();
        StartCoroutine(StartDelay());
        StartCoroutine(ManageLifeTime());
    }

    void Update()
    {
        if (IsTriggered)
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
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!IsTriggered) return;
        
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

    public void PressButton()
    {
        IsTriggered = true;
        StopAllCoroutines();
        StartCoroutine(ManageLifeTime(activatedLifeTime));
    }

    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(delayBeforeButtonActive);
        IsButtonActive = true;
    }
}
