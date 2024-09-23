using System;
using System.Collections;
using System.Collections.Generic;
//using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    // Casts
    public virtual void CastSpace(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        Debug.Log("Press Space");
    }
    
    public virtual void CastA(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        Debug.Log("Press A");
    }

    public virtual void CastZ(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        Debug.Log("Press Z");
    }
    
    public virtual void CastE(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        Debug.Log("Press E");
    }
    
    public virtual void CastR(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        Debug.Log("Press R");
    }

    protected Vector3 GetMousePos()
    {
        return _camera.ScreenToWorldPoint(Input.mousePosition);
    }
}

public enum PlayerCLass
{
    Cra,
    Ecaflip,
    Eliatrope,
    Eniripsa,
    Enutrof,
    Feca, 
    Forgelance,
    Huppermage,
    Iop,
    Osamodas,
    Ouginak,
    Pandawa,
    Roublard,
    Sacrieur,
    Sadida,
    Sram,
    Steamer,
    Xelor,
    Zobal
}

public enum DmgType
{
    Physical,
    Magical,
    True
}

public enum Side
{
    Green,
    Red,
    Both
}

public enum Control
{
    // Affected by tenacity
    Stun,
    AirBorne,
    Root,
    Silence,
    Disarm,
    Exhausted
}

public enum Special
{
    // Not affected by tenacity
    Invincible,
    Gravitation,
    UnHeal,
    Pacifist,
    Immovable,
    ControlImmune,
    Invisible
}

public enum Stat
{
    Damage,
    Durability,
    RecievedHeal,
    Range,
    PhyDmg,
    MagDmg,
    PushDmg,
    CritChance,
    CritDmg,
    LifeSteal,
    AttackSpeed,
    PhyRes,
    MagRes,
    Heal,
    Tenacity,
    MovementSpeed,
}
