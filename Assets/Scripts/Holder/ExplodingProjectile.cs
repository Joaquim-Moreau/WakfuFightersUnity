using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Animator))]
public class ExplodingProjectile : Projectile
{
    public Side TriggerSide;
    [SerializeField] private float AreaOfEffectLifeTime;
    [SerializeField] private float refreshPeriod;
    public bool HasExploded { get; private set; } = false;
    
    private float _refresh;
    private bool _touchingEntity;
    private Renderer _renderer;
    private Animator _animator;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        _renderer.enabled = false;
        HasExploded = false;
        _refresh = 0f;
    }

    void Update()
    {
        if (HasExploded)
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
        }
        else
        {
            transform.position += speed * Time.deltaTime * Direction;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!HasExploded) return;
        
        if (other.CompareTag("Entity") || other.CompareTag("Player"))
        {
            Entity target = other.GetComponent<Entity>();
            if (!spellData.CanSpellAffect(target)) return;
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

    public void Explode()
    {
        HasExploded = true;
        _renderer.enabled = true;
        AlreadyHit = new List<Entity>();
        _animator.SetTrigger("Explode");
        StopAllCoroutines();
        StartCoroutine(ManageLifeTime(AreaOfEffectLifeTime));
    }
}
