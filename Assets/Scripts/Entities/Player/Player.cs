using System;
using System.Collections;
using System.Collections.Generic;
//using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Player : Entity
{
    public UnityEvent onDeath;
    
    protected override void Die()
    {
        _camera.transform.SetParent(null);
        base.Die();
        onDeath?.Invoke();
    }

    public void ResetCoolDowns()
    {
        spellBook.ResetCoolDowns();
    }
    
    public virtual void CastSpace(InputAction.CallbackContext context) {}
    
    public virtual void CastA(InputAction.CallbackContext context) {}

    public virtual void CastZ(InputAction.CallbackContext context) {}
    
    public virtual void CastE(InputAction.CallbackContext context) {}
    
    public virtual void CastR(InputAction.CallbackContext context) {}

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
