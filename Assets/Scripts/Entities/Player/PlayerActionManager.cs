using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class PlayerActionManager : EntityActionManager
{
    private bool _holdingClick;
    private Animator _animator;
    private NavMeshAgent _agent;
    private SpellManager spellManager;

    [SerializeField] private LayerMask clickable;
    
    protected override void Awake()
    {
        base.Awake();
        _animator = GetComponent<Animator>();
        spellManager = GetComponent<SpellManager>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (movementState == MovementState.Dashing) return;
        
        if (_holdingClick)
        {
            ChooseDestination();
        }

        if (CurrentTarget is null || !CurrentTarget.IsAlive)
        {
            TryToWalk();
        }
        else
        {
            Destination = CurrentTarget.transform.position;
            _agent.SetDestination(CurrentTarget.transform.position);
            float distanceToTarget = Statics.GetDistance(Self, CurrentTarget);
            
            if (SpellBuffer is null)
            {
                TryToAttack(distanceToTarget);
            }
            else
            {
                TryToCast(distanceToTarget);
            }
        }
        
        _animator.SetBool("walking", movementState == MovementState.Walking);
    }

    public void OnRightClick(InputAction.CallbackContext context)
    {
        HoldRightClick(context);
        ChooseDestination();
    }

    private void HoldRightClick(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _holdingClick = true;
        }

        if (context.canceled)
        {
            _holdingClick = false;
        }
    }
    
    public void ChooseDestination()
    {
        if (movementState == MovementState.Dashing) return;
        
        Vector3 mousePos = MainCamera.ScreenToWorldPoint(Input.mousePosition);
        CurrentTarget = GetTarget(mousePos);
        if (CurrentTarget && CurrentTarget.side == Self.side) CurrentTarget = null;
        if (CurrentTarget is null)
        {
            Destination = mousePos;
            Destination.z = 0;
            
            SpellBuffer = null;
        }
        else
        {
            Destination = CurrentTarget.transform.position;
        }
        _agent.SetDestination(Destination);
    }

    public override Entity GetTarget(Vector3 position)
    {
        Vector2 pos2D = new Vector2(position.x, position.y);
        
        var targetCollider = Physics2D.OverlapPoint(pos2D, clickable);
        if (targetCollider is null) return null;
        
        if (targetCollider.CompareTag("Entity") || targetCollider.CompareTag("Player"))
        {
            var target = targetCollider.GetComponent<Entity>();
            
            if (target.visibleToOpponent || target == Self)
            {
                return target;
            }
        }
        return null;
    }

    private void TryToWalk()
    {
        if (Self.CannotWalk() || _agent.remainingDistance <= _agent.stoppingDistance)
        {
            Stop();
        }
        else
        {
            Walk();
        }
    }
    
    private void Walk()
    {
        movementState = MovementState.Walking;
        _agent.speed = Self.GetMoveSpeed();
        LookAt(Destination);
    }

    private void Stop()
    {
        _agent.SetDestination(transform.position); 
        movementState = MovementState.Stopped;
    }

    private void TryToAttack(float distanceToTarget)
    {
        if (distanceToTarget <= Self.GetRange())
        {
            Attack();
        }
        else
        {
            TryToWalk();
        }
    }
    
    private void Attack()
    {
        Stop();
        if (Self.GetAutoAttack().IsReady())
        {
            Self.LaunchAuto(CurrentTarget);
        }
    }

    protected void TryToCast(float distanceToTarget)
    {
        if (distanceToTarget <= SpellBuffer.ActualRange)
        {
            Stop();
            spellManager.Launch(SpellBuffer, CurrentTarget);
            SpellBuffer = null;
            CurrentTarget = null;
        }
        else
        {
            Walk();
        }
    }
}
