using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyActionManager : EntityActionManager
{
    private Animator _animator;
    private NavMeshAgent _agent;

    private Player _player;
    private float _timeBeforeChangingDestination = 0f;
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
        _player = MainSceneManager.instance.player.GetComponent<Player>();
    }

    private void Update()
    {
        if (Self.visibleToOpponent && !_player.IsInvisible())
        {
            CurrentTarget = _player;
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
        _agent.SetDestination(transform.position); 
        movementState = MovementState.Stopped;
    }
    
    private void TryToAttack(float distanceToTarget)
    {
        if (distanceToTarget <= Self.RangePoints)
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
            Self.LaunchAuto(CurrentTarget);
        }
    }

    private IEnumerator UpdateDestination()
    {
        yield return new WaitForSeconds(0.1f);
        LookAt(Destination);
        _agent.SetDestination(Destination);
    }
    
    private void ChooseDestination()
    {
        if (_timeBeforeChangingDestination <= 0f) //done with path
        {
            if (RandomPoint(transform.position, 5, out Destination)) //pass in our centre point and radius of area
            {
                Debug.DrawRay(Destination, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
            }
            _timeBeforeChangingDestination = 3f;
            StartCoroutine(UpdateDestination());
        }
        _timeBeforeChangingDestination -= Time.deltaTime; 
    }
    
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector2 randomPoint2D = new Vector2(center.x, center.y) + Random.insideUnitCircle * range;
        Vector3 randomPoint = new Vector3(randomPoint2D.x, randomPoint2D.y, transform.position.z);
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        { 
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
