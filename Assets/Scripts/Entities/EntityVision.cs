using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityVision : MonoBehaviour
{
    public LayerMask layerMask;
    public HashSet<Entity> visibleEntities = new HashSet<Entity>();
    
    [SerializeField] private float colliderRadius;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Entity"))
        {
            var entity = other.GetComponent<Entity>();
            if (entity.side == Side.Green) return;
            RaycastHit2D ray = Physics2D.Raycast(transform.position, entity.transform.position - transform.position, 10, layerMask);
            
            if (ray.collider && !ray.collider.CompareTag("Wall"))
            {
                visibleEntities.Add(entity);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Entity"))
        {
            visibleEntities.Remove(other.GetComponent<Entity>());
        }
    }
}
