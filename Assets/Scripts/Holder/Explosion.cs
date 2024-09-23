using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private ExplodingProjectile projectile;
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        _renderer.enabled = true;
    }

    private void OnDisable()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (projectile.HasExploded) return;
        
        if (other.CompareTag("Entity") || other.CompareTag("Player"))
        {
            Entity target = other.GetComponent<Entity>();
            if (projectile.TriggerSide == Side.Both || target.side == projectile.TriggerSide)
            { 
                _renderer.enabled = false;
                projectile.Explode();
            }
        }
    }
}
