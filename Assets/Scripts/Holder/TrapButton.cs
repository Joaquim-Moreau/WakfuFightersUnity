using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapButton : MonoBehaviour
{
    public Trap trap;
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!trap.IsButtonActive) return;
        if (trap.IsTriggered) return;
        
        if (other.CompareTag("Entity") || other.CompareTag("Player"))
        {
            Entity target = other.GetComponent<Entity>();
            if (trap.triggerSide == Side.Both || target.side == trap.triggerSide)
            { 
                trap.PressButton();
            }
        }
    }
}
