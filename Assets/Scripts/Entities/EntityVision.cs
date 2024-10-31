using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityVision : MonoBehaviour
{
    public LayerMask layerMask;
    public HashSet<Entity> EntitiesInCollider = new HashSet<Entity>();
    public HashSet<Entity> visibleEntities = new HashSet<Entity>();

    [SerializeField] private Side enemySide;
    [SerializeField] private float colliderRadius;

    private void Update()
    {
        visibleEntities = new HashSet<Entity>();
        foreach (var entity in EntitiesInCollider)
        {
            RaycastHit2D ray = Physics2D.Raycast(transform.position, entity.transform.position - transform.position, 10, layerMask);
            
            if (ray.collider && !ray.collider.CompareTag("Wall") && !entity.IsInvisible())
            {
                visibleEntities.Add(entity);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Entity") || other.CompareTag("Player"))
        {
            var entity = other.GetComponent<Entity>();
            if (entity.side != enemySide) return;
            
            EntitiesInCollider.Add(entity);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Entity") || other.CompareTag("Player"))
        {
            EntitiesInCollider.Remove(other.GetComponent<Entity>());
        }
    }
}
