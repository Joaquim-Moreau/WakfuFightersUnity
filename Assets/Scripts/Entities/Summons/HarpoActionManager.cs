using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class HarpoActionManager : EntityActionManager
{
    [SerializeField] private EntityVision entityVision;
    [SerializeField] private bool isStatic;
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
    
    private void Update()
    {
        if (entityVision.visibleEntities.Any())
        {
            CurrentTarget = GetClosestEnemy();
            float distanceToTarget = Statics.GetDistance(Self, CurrentTarget);
            TryToAttack(distanceToTarget);
        }
    }
    
    private void TryToAttack(float distanceToTarget)
    {
        if (distanceToTarget <= Self.GetRange())
        {
            Attack();
        }
    }
    
    private void Attack()
    {
        if (Self.GetAutoAttack().IsReady())
        {
            _animator.SetTrigger("Attack");
            Self.LaunchAuto(CurrentTarget);
        }
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
