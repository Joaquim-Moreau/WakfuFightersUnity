using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aura : Holder
{
    [SerializeField] private float refreshPeriod;
    public Side attachSide;
    
    private float _refresh;
    private bool _touchingEntity;
    private Entity _carrier;
    
    
    public override void Init(Entity caster, Entity target)
    {
        base.Init(caster, target);
        _carrier = target;
        _refresh = 0f;
        _touchingEntity = false;
        transform.position = _carrier.transform.position;
        
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

        transform.position = _carrier.transform.position;
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Entity") || other.CompareTag("Player"))
        {
            Entity target = other.GetComponent<Entity>();
            //if (!CanHitEntity(target)) return;
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
