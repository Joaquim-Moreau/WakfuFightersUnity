using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityActionManager : MonoBehaviour
{
    protected Entity CurrentTarget = null;

    protected Camera MainCamera;
    protected Entity Self;
    protected Vector3 Destination;
    public MovementState movementState;
    
    protected SpriteRenderer SpriteRenderer;
    protected SpellData SpellBuffer = null;
    protected bool PathReset = false;
    
    protected virtual void Awake()
    {
        Self = GetComponent<Entity>();
        MainCamera = Camera.main;
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    public virtual Entity GetTarget(Vector3 position)
    {
        return null;
    }
    
    public void LookAt(Vector3 objectPosition)
    {
        SpriteRenderer.flipX = transform.position.x >= objectPosition.x;
    }

    public void AddToBuffer(SpellData spellData, Entity target)
    {
        SpellBuffer = spellData;
        CurrentTarget = target;
    }
}

public enum MovementState
{
    Walking,
    Dashing,
    Stopped
}

