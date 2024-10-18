using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class SummonActionManager : EntityActionManager
{
    [SerializeField] private EntityVision entityVision;
    [SerializeField] private bool isStatic;
    private Player _owner;
    private Animator _animator;
    private NavMeshAgent _agent;
    
    protected override void Awake()
    {
        base.Awake();
        
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.ResetPath(); 
        PathReset = true;
    }
    
    private void Start()
    {
        _owner = MainSceneManager.instance.player.GetComponent<Player>();
    }

    private void Update()
    {
        if (entityVision.visibleEntities.Any())
        {
            CurrentTarget = GetClosestEnemy();
            Destination = CurrentTarget.transform.position;
            float distanceToTarget = Statics.GetDistance(Self, CurrentTarget);
            TryToAttack(distanceToTarget);
        }
        else
        {
            CurrentTarget = null;
            ChooseDestination();
            TryToWalk();
        }
        _animator.SetBool("walking", movementState == MovementState.Walking);
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
        _agent.ResetPath();
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
            StartCoroutine(UpdateDestination());
            TryToWalk();
        }
    }
    
    private void Attack()
    {
        Stop();
        if (Self.GetAutoAttack().IsReady())
        {
            _animator.SetTrigger("Attack");
            Self.LaunchAuto(CurrentTarget);
        }
    }

    private IEnumerator UpdateDestination()
    {
        yield return new WaitForSeconds(0.1f);
        if (!Self.CannotWalk())
        {
            LookAt(Destination);
            _agent.SetDestination(Destination);
        }
    }
    
    private void ChooseDestination()
    {
        float distanceToOwner = Statics.GetDistance(Self, _owner);
        if (distanceToOwner < Self.GetRange())
        {
            Destination = Self.transform.position;
        }
        else
        {
            Destination = _owner.transform.position;
        }
        StartCoroutine(UpdateDestination());
    }

    private Entity GetClosestEnemy()
    {
        float distanceToClosest = 10f;
        Entity closestEnemy = null;
        
        foreach (var entity in entityVision.visibleEntities)
        {
            float distanceToNewEntity = Statics.GetDistance(Self, entity);
            if (distanceToNewEntity < distanceToClosest)
            {
                distanceToClosest = distanceToNewEntity;
                closestEnemy = entity;
            }
        }

        return closestEnemy;
    }
}
