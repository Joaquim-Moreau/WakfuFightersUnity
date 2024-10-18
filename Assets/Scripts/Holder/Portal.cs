using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Portal : Spell
{
    public float closingDuration;
    
    private Animator _animator;
    private static readonly int AnimatorID = Animator.StringToHash("opened");
    
    private bool _isOpen;
    private static Portal _activePortal1 = null;
    private static Portal _activePortal2 = null;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        _isOpen = false;
        
        if (this == _activePortal1) _activePortal1 = null;
        if (this == _activePortal2) _activePortal2 = null;
        
        StopAllCoroutines();
    }
    
    public override void Init(Entity caster, Vector3 launchPosition)
    {
        _isOpen = true;
        
        NavMeshHit hit;
        if (NavMesh.SamplePosition(launchPosition, out hit, 2.0f, NavMesh.AllAreas)) {
            transform.position = hit.position;
        }

        if (_activePortal1 == null)
        {
            _activePortal1 = this;
        }
        else if (_activePortal2 == null)
        {
            _activePortal2 = this;
        }
        
        StopAllCoroutines();
        StartCoroutine(PortalLifetime(lifeTime));
    }
    
    public override void Init(Entity caster, Entity target) {}

    // Update is called once per frame
    void Update()
    {
        if (_activePortal1 && _activePortal2)
        {
            if (_activePortal1 == this)
            {
                if (_activePortal2._isOpen)
                {
                    OpenPortal();
                }
            }
            else 
            {
                if (_activePortal1._isOpen)
                {
                    OpenPortal();
                }
            }
        }
        else
        {
            ClosePortal();
        }
    }
    
    private IEnumerator PortalLifetime(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        gameObject.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!_isOpen) return;
        
        if (other.CompareTag("Entity") || other.CompareTag("Player"))
        {
            TeleportEntityToOtherPortal(other.transform);
            StartCoroutine(_activePortal1.ClosingPortalTemporarily(closingDuration));
            StartCoroutine(_activePortal2.ClosingPortalTemporarily(closingDuration));
        }
        else if (other.CompareTag("MovingSpell"))
        {
            TeleportToOtherPortal(other.transform);
            StartCoroutine(_activePortal1.ClosingPortalTemporarily(closingDuration / 5f));
            StartCoroutine(_activePortal2.ClosingPortalTemporarily(closingDuration / 5f));
        }
    }

    public void ClosePortal()
    {
        _isOpen = false;
        _animator.SetBool(AnimatorID, _isOpen);
    }

    public void OpenPortal()
    {
        _isOpen = true;
        _animator.SetBool(AnimatorID, _isOpen);
    }

    public IEnumerator ClosingPortalTemporarily(float duration)
    {
        ClosePortal();
        yield return new WaitForSeconds(duration);
        OpenPortal();
    }

    private void TeleportEntityToOtherPortal(Transform entity)
    {
        entity.GetComponent<NavMeshAgent>().enabled = false;
        TeleportToOtherPortal(entity);
        entity.GetComponent<NavMeshAgent>().enabled = true;
    }

    private void TeleportToOtherPortal(Transform entity)
    {
        if (this == _activePortal1)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(_activePortal2.transform.position, out hit, 2.0f, NavMesh.AllAreas)) {
                entity.position = hit.position;
            }
        }
        else if (this == _activePortal2)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(_activePortal1.transform.position, out hit, 2.0f, NavMesh.AllAreas)) {
                entity.position = hit.position;
            }
        }
    }
}
