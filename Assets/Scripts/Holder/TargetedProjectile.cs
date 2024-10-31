using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TargetedProjectile : Holder
{
    public float speed;

    //private Vector3 _direction;
    private Entity _target;

    public override void Init(Entity caster, Entity target)
    {
        base.Init(caster, target);
        _target = target;
        transform.position = caster.transform.position;
    }
    
    private void Update()
    {
        if (!_target.gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }   
        
        transform.position = Vector3.MoveTowards(
            transform.position,
            _target.transform.position,
            speed * Time.deltaTime);
        Direction = Statics.GetDirection(this, _target);
        RotateObject();
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Entity"))
        {
            Entity obstacle = other.GetComponent<Entity>();
            if (obstacle == _target)
            {
                ApplyEffects(obstacle);
                gameObject.SetActive(false);
            }
        }
    }
}
