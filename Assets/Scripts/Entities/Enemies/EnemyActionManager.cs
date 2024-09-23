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
        }
        else
        {
            CurrentTarget = null;
        }
        
        //
        ChooseDestination();
        //
        Attack();
        //
        if (_agent.remainingDistance <= _agent.stoppingDistance)
        {
            movementState = MovementState.Stopped;
        }
        else
        {
            if (Self.CannotWalk())
            {
                _agent.SetDestination(transform.position);
                movementState = MovementState.Stopped;
            }
            else
            {
                movementState = MovementState.Walking;
                LookAt(Destination);
                _agent.speed = Self.GetMoveSpeed();
            }
        }
        
        _animator.SetBool("walking", movementState == MovementState.Walking);
    }
    
    private void Walk()
    {
        Debug.Log($"Walking : {movementState}");
        if (movementState == MovementState.Stopped)
        { 
            if (!PathReset) 
            { 
                _agent.ResetPath(); 
                PathReset = true; 
            }
            return;
        }
        //if (movementState != MovementState.Walking) return;
        _agent.SetDestination(Destination);
        _agent.speed = Self.GetMoveSpeed();
        PathReset = false;
        
        Debug.Log($"Real Distance : {Statics.GetDistance(transform.position, Destination)}");
        Debug.Log($"Other distance : {_agent.remainingDistance}");
        if (_agent.remainingDistance <= _agent.stoppingDistance)
        {
            //Debug.Log("Stopping");
            movementState = MovementState.Stopped;
        }
        else
        {
            LookAt(Destination);
        }
    }
    
    private void Attack()
    {
        if (CurrentTarget && SpellBuffer is null)
        {
            float distanceToTarget = Statics.GetDistance(Self, CurrentTarget);
            // TODO : Mettre une touche qui permet d'attaquer l'ennemi visible le plus proche
            if (distanceToTarget > Self.RangePoints) // TODO : function get Range
            {
                movementState = MovementState.Walking;
            }
            else
            {
                if (Self.GetAutoAttack().IsReady())
                { 
                    Self.LaunchAuto(CurrentTarget);
                }

                _agent.SetDestination(transform.position);
                movementState = MovementState.Stopped;
            }
        }
    }

    private void ChooseDestination()
    {
        if (CurrentTarget is null)
        {
            if (_timeBeforeChangingDestination <= 0f) //done with path
            {
                Vector3 point;
                if (RandomPoint(transform.position, 5, out point)) //pass in our centre point and radius of area
                {
                    Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                    Destination = point;
                    _agent.SetDestination(Destination);
                    _timeBeforeChangingDestination = 3f;
                }
            }
            _timeBeforeChangingDestination -= Time.deltaTime; 
        }
        else
        { 
            Destination = CurrentTarget.transform.position;
            _agent.SetDestination(Destination);
        }
    }
    
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
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
