using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;

public class PlayerVision : MonoBehaviour
{
    public LayerMask layerMask;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Entity"))
        {
            var entity = other.GetComponent<Entity>();
            if (entity.side == Side.Green) return;
            RaycastHit2D ray = Physics2D.Raycast(transform.position, entity.transform.position - transform.position, 10, layerMask);
            
            if (ray.collider)
            {
                entity.visibleToOpponent = !ray.collider.CompareTag("Wall");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Entity"))
        {
            var entity = other.GetComponent<Entity>();
            if (entity.side == Side.Green) return;
            entity.visibleToOpponent = false;
        }
    }
}
