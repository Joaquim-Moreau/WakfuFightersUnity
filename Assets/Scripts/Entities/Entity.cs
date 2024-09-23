using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(EntityActionManager))]
[RequireComponent(typeof(SpellManager))]
[RequireComponent(typeof(SpellBook))]
public class Entity : MonoBehaviour
{
    public Side side;
    public bool visibleToOpponent = false;
    private bool _previouslyVisible = true;
    protected HealthBar _healthBar;
    
    
    [field: SerializeField] public int ShieldPoints { get; protected set; }
    [field: SerializeField] public int maxHP { get; protected set; } = 1000;
    [field: SerializeField] public int hp { get; protected set; }
    public int mana { get; protected set; }
    [field: SerializeField] public int maxMana { get; protected set; } = 180;
    [SerializeField] protected float hpRegen = 6f;
    [SerializeField] protected float manaRegen = 6f;
 
    [SerializeField] protected int phyDmg;
    [SerializeField] protected int pushDmg;
    [SerializeField] protected int magDmg;
    protected int CritChance = 15;
    protected int CritDmg = 50;
    protected int Heal = 0;
    protected int Tenacity = 100;
    [SerializeField] protected int LifeSteal;
    [SerializeField] protected float AttackSpeed = 1f;
    
    [SerializeField] protected int phyRes;
    [SerializeField] protected int magRes;
    [SerializeField] protected float moveSpeed;
 
    public float RangePoints = 10f;
    
    
    // Bonus stats
    [System.NonSerialized] public int DamageAmp = 0;
    [System.NonSerialized] public int DurabilityAmp = 0;
    [System.NonSerialized] public int ReceivedHealAmp = 0;
    [System.NonSerialized] public int BonusRange = 0;
    [System.NonSerialized] public int BonusPhyDmg = 0;
    [System.NonSerialized] public int BonusPushDmg = 0;
    [System.NonSerialized] public int BonusMagDmg = 0;
    [System.NonSerialized] public int BonusCritChance = 0;
    [System.NonSerialized] public int BonusCritDmg = 0;
    [System.NonSerialized] public int BonusLifeSteal = 0;
    [System.NonSerialized] public float BonusAttackSpeed = 0f;
    [System.NonSerialized] public int BonusPhyRes = 0;
    [System.NonSerialized] public int BonusMagRes = 0;
    [System.NonSerialized] public int BonusHeal = 0;
    [System.NonSerialized] public int BonusTenacity = 0;
    [System.NonSerialized] public float BonusMovementSpeed = 0f;
    
    // CC 
    protected bool isAirBorne;
    

    [System.NonSerialized] public int stun;
    [System.NonSerialized] public int root;
    [System.NonSerialized] public int silence;
    [System.NonSerialized] public int disarm;
    [System.NonSerialized] public int exhaust;

    // Special effects
    [System.NonSerialized] public int grounded;
    [System.NonSerialized] public int invincibility;
    [System.NonSerialized] public int unHealAble;
    [System.NonSerialized] public int pacifist;
    [System.NonSerialized] public int immovable;
    [System.NonSerialized] public int controlImmune;
    [System.NonSerialized] public int Invisible;
    
    // Effects management
    [System.NonSerialized] public List<Effect> delayedEffects = new List<Effect>();
    [System.NonSerialized] public List<DurableEffect> currentEffects = new List<DurableEffect>();
    [System.NonSerialized] public List<ShieldEffect> currentShields = new List<ShieldEffect>();
    [System.NonSerialized] public List<PoisonEffect> currentPoisons = new List<PoisonEffect>();
    [System.NonSerialized] public PushEffect currentPush = null;
    
    // Actions
    [System.NonSerialized] public EntityActionManager _actionManager;
    [System.NonSerialized] public Animator _Animator;
    protected Camera _camera;
    [FormerlySerializedAs("SpellBook")] public SpellBook spellBook;
    protected SpellManager spellManager;
    
    
    //
    protected Renderer Renderer;
    protected Canvas _healthBarCanvas;
    private bool _pushDmgImmunity = false;

    private float _regenCounter = 0f;

    protected virtual void Awake()
    {
        _healthBar = GetComponentInChildren<HealthBar>();
        _healthBarCanvas = GetComponentInChildren<Canvas>();
        Renderer = GetComponent<Renderer>();
        
        _actionManager = GetComponent<EntityActionManager>();
        _camera = Camera.main;
        _Animator = GetComponent<Animator>();
        spellManager = GetComponent<SpellManager>();
        spellBook = GetComponent<SpellBook>();
    }

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHP;
        mana = maxMana;
        if (side == Side.Green) visibleToOpponent = true;
    }
    

    // Update is called once per frame
    protected virtual void Update()
    {
        // Effects
        UpdateEffects();
        UpdateShields();
        UpdatePoisons();
        UpdateAirBorne();
        
        UpdateUI();
        UpdateVision();
        Regen();

        if (hp <= 0)
        {
            Die();
        }
    }
    
    protected void UpdateEffects()
    {
        if (!currentEffects.Any())
        {
            return;
        }

        for (int i = currentEffects.Count - 1; i >= 0; i--)
        {
            if (currentEffects[i].IsExpired())
            {
                currentEffects[i].Remove(this);
                currentEffects.RemoveAt(i);
            }
            else
            {
                currentEffects[i].UpdateDuration();
            }
        }
    }
    
    protected void UpdateShields()
    {
        int currentShield = 0;
        if (!currentShields.Any())
        {
            ShieldPoints = 0;
            return;
        }
        
        for (int i = currentShields.Count - 1; i >= 0; i--)
        {
            // Duration
            if (currentShields[i].IsExpired()|| currentShields[i].totalShield == 0)
            {
                currentShields[i].Remove(this);
                currentShields.RemoveAt(i);
            }
            else
            {
                currentShield += currentShields[i].totalShield;
                currentShields[i].UpdateDuration();
            }
        }
        ShieldPoints = currentShield;
    }
    
    protected void UpdatePoisons()
    {
        if (!currentPoisons.Any()) return;
        
        for (int i = currentPoisons.Count - 1; i >= 0; i--)
        {
            // Refresh the clock
            if (currentPoisons[i].refresh <= 0f)
            {
                currentPoisons[i].refresh += 0.5f;
                currentPoisons[i].ApplyTick(this);
                //currentPoisons[i].Apply(this);
            }
            else
            {
                currentPoisons[i].refresh -= Time.deltaTime;
            }
            
            // Duration
            if (currentPoisons[i].IsExpired())
            {
                currentPoisons[i].Remove(this);
                currentPoisons.RemoveAt(i);
            }
            else
            {
                currentPoisons[i].UpdateDuration();
            }
        }
    }
    
    protected void UpdateAirBorne()
    {
        if (currentPush is null) return;

        if (currentPush.IsExpired())
        {
            currentPush.Remove(this);
            currentPush = null;
            isAirBorne = false;
        }
        else
        {
            currentPush.UpdateMovement(this);
            currentPush.UpdateDuration();
            isAirBorne = true;
        }
    }
    

    protected void UpdateUI()
    {
        _healthBar.UpdateShieldsBar(ShieldPoints, maxHP);
        _healthBar.UpdateHealthBar(hp, maxHP);
        _healthBar.UpdateManaBar(mana, 180);

        // update cooldown visuals
    }

    protected void UpdateVision()
    {
        if (side == Side.Red)
        {
            //Renderer.enabled = visibleToOpponent;
            //_healthBarCanvas.enabled = visibleToOpponent;
            
        }
        
        if (side == Side.Green) return;
        
        if (_previouslyVisible != visibleToOpponent)
        {
            if (visibleToOpponent)
            {
                GoRecursivelyToLayer(0, gameObject);
            }
            else
            {
                GoRecursivelyToLayer(7, gameObject);
            }
        }

        _previouslyVisible = visibleToOpponent;
    }

    private void GoRecursivelyToLayer(int layer, GameObject obj)
    {
        //Debug.Log(obj.name);
        obj.layer = layer;
        foreach (Transform child in obj.transform)
        {
            GoRecursivelyToLayer(layer, child.gameObject);
        }
    }

    public float TakeDmg(float damage, DmgType type)
    {
        float finalDmg = 0;
        switch (type)
        {
            case DmgType.Physical:
                finalDmg = damage * (100f / (100f + Mathf.Max(0, GetPhyRes()))) * ((100f + DurabilityAmp) / 100f);
                break;
            case DmgType.Magical:
                finalDmg = damage * (100f / (100f + Mathf.Max(0, GetMagRes()))) * ((100f + DurabilityAmp) / 100f);
                break;
            case DmgType.True:
                finalDmg = damage * Mathf.Max(((100f + DurabilityAmp) / 100f), 1);
                break;
            default:
                break;
        }
        // update shields
        int postMitigatedDamage = (int)finalDmg;
        foreach (var shield in currentShields)
        {
            if (shield.totalShield > 0)
            {
                if (postMitigatedDamage > finalDmg)
                {
                    postMitigatedDamage -= shield.totalShield;
                    shield.totalShield = 0;
                }
                else
                {
                    shield.totalShield -= postMitigatedDamage;
                    postMitigatedDamage = 0;
                }
            }
        }
        
        
        if (postMitigatedDamage >= 50f)
        {
            _Animator.SetTrigger("TakeDmg");
        }
        hp -= postMitigatedDamage;
        
        return postMitigatedDamage;
    }
    
    public void HealHp(float amount)
    {
        // if pas insoignable
        hp += (int)(amount * (100f + ReceivedHealAmp)/100f );
    }

    public void ConsumeMana(int amount)
    {
        mana -= amount;
    }

    protected void Regen()
    {
        if (_regenCounter <= 0f)
        {
            _regenCounter += 0.5f;
            ConsumeMana(-(int)(manaRegen/2f));
            HealHp((hpRegen/2f));
        }
        else
        {
            _regenCounter -= Time.deltaTime;
        }

        hp = Mathf.Clamp(hp, 0, maxHP);
        mana = Mathf.Clamp(mana, 0, 180);
    }

    public virtual SpellData GetAutoAttack()
    {
        return spellBook.AutoAttack;
    }

    public virtual void LaunchAuto(Entity target)
    {
        var auto = GetAutoAttack();
        auto.UpdateParameters(this);
        auto.UpdateCooldown(this, GetAttackSpeed());
        spellManager.Launch(auto, target);
    }

    public virtual void HandleSpellLaunch(SpellName spellName) {}

    protected virtual void Die()
    {
        Debug.Log($"{name} is dead");
        gameObject.SetActive(false);
    }
    
    
    // Entity capacity according to controls
    public bool CannotCastSpell()
    {
        return isAirBorne || stun > 0 || silence > 0 || (_actionManager.movementState == MovementState.Dashing); 
    }

    public bool CannotAutoAttack()
    {
        return isAirBorne || stun > 0 || disarm > 0;
    }
    
    public bool CannotWalk()
    {
        return isAirBorne || stun > 0 || root > 0;
    }

    public bool CannotTakeDamage()
    {
        return invincibility > 0;
    }

    public bool CannotDealDamage()
    {
        return pacifist > 0;
    }

    public bool CannotBeHealed()
    {
        return unHealAble > 0;
    }

    public bool CannotBeMoved()
    {
        return immovable > 0 || controlImmune > 0;
    }

    public bool CannotTeleportOrDash()
    {
        return root > 0 || grounded > 0;
    }

    public bool CannotBeControlled()
    {
        return controlImmune > 0;
    }

    public bool IsExhausted()
    {
        return exhaust > 0;
    }

    public bool IsInvisible()
    {
        return Invisible > 0;
    }

    // Getters
    public int GetPhyDmg()
    {
        return phyDmg + BonusPhyDmg;
    }

    public int GetPushDmg()
    {
        return pushDmg + BonusPushDmg;
    }
    
    public int GetMagDmg()
    {
        return magDmg + BonusMagDmg;
    }

    public int GetDmgMultiplier()
    {
        return DamageAmp;
    }
    
    public int GetPhyRes()
    {
        return phyRes + BonusPhyRes;
    }
    
    public int GetMagRes()
    {
        return magRes + BonusMagRes;
    }

    public float GetCritDmg()
    {
        return (100 + CritDmg + BonusCritDmg) / 100f;
    }
    
    public int GetCritChance()
    {
        return CritChance + BonusCritChance;
    }
    
    public int GetLifeSteal()
    {
        return LifeSteal + BonusLifeSteal;
    }

    public int GetHeal()
    {
        return Heal + BonusHeal;
    }

    public int GetTenacity()
    {
        return Tenacity + BonusTenacity;
    }

    public float GetAttackSpeed()
    {
        return AttackSpeed * ((100 + BonusAttackSpeed) / 100);
    }

    public float GetMoveSpeed()
    {
        return moveSpeed * ((100 + BonusMovementSpeed) / 100);
    }
    
    protected void OnCollisionStay2D(Collision2D other)
    {
        if (!isAirBorne) return;
        if (_pushDmgImmunity) return;
        
        if (other.gameObject.CompareTag("Wall"))
        {
            currentPush.ApplyPushDamage(this);
            currentPush = null;
            
            StartCoroutine(PushDmgImmunity());
            isAirBorne = false;
        }
        else
        {
            if (other.gameObject.CompareTag("Entity"))
            {
                Entity collateral = other.gameObject.GetComponent<Entity>();
                collateral.TakeDmg(25, DmgType.Physical);
                
                currentPush.ApplySecondaryPushDamage(collateral);
                currentPush.ApplyPushDamage(this);
                currentPush = null;
                
                StartCoroutine(PushDmgImmunity());
                isAirBorne = false;
                
            }
        }
    }

    protected IEnumerator PushDmgImmunity()
    {
        _pushDmgImmunity = true;
        yield return new WaitForSeconds(1f);
        _pushDmgImmunity = false;
    }
}
