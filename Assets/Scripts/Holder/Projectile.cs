using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Projectile : Holder
{
    public float speed;
    public int targetLimit;

    private int _currentTargetLimit;
    
    public override void Init(Entity caster, Vector3 launchPosition)
    {
        base.Init(caster, launchPosition);
        transform.position = caster.transform.position;
        
        Vector3 displacement = launchPosition - caster.transform.position;
        displacement.z = 0f;
        float dist = Mathf.Sqrt(displacement.x * displacement.x + displacement.y * displacement.y);
        Direction = displacement/dist;
        RotateObject();

        lifeTime = spellData.ActualRange/speed;
        _currentTargetLimit = targetLimit;
        
        StopAllCoroutines();
        StartCoroutine(ManageLifeTime());
    }
    

    // Update is called once per frame
    void Update()
    {
        transform.position += speed * Time.deltaTime * Direction;
        
        if (_currentTargetLimit <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (_currentTargetLimit <= 0) return;
        
        if (other.CompareTag("Entity"))
        {
            Entity target = other.GetComponent<Entity>();
            if (!spellData.CanSpellAffect(target)) return;
            if (AlreadyHit.Contains(target)) return;
            
            ApplyEffects(target);
            _currentTargetLimit -= 1;
            AddToHit(target);
        }
    }
}
