using UnityEngine;

public class SpellManager : MonoBehaviour
{
    private Entity _caster;
    // TODO : transformer en Scriptable Object / Singleton --> voir pour unity event ??
    private void Awake()
    {
        _caster = GetComponent<Entity>();
    }

    public void CastSpell(SpellData spellData, Vector3 targetPosition)
    {
        if (_caster.CannotCastSpell())
        {
            Debug.Log("Impossible to cast");
            return;
        }
        
        //if (!SpellCDManager.instance.IsSpellReady(spellData.spellName))
        if (!spellData.IsReady())
        { 
            Debug.Log("Spell is on cooldown");
            return;
        }
        
        if (_caster.mana < spellData.ManaCost)
        {
            Debug.Log("Not enough mana : " + _caster.mana + " < " + spellData.ManaCost);
            return;
        }
        
        
        spellData.UpdateParameters(_caster);
        
        if (!spellData.targeted)
        {
            Launch(spellData, targetPosition);
            return;
        }
        
        Entity target = _caster._actionManager.GetTarget(targetPosition);
        if (target is null)
        {
            Debug.Log("No enemy at target position or not visible.");
            return;
        }
        
        if(!spellData.CanSpellAffect(target))
        {
            Debug.Log("Can't cast on this enemy : " + target.side + " / " + spellData.affectedSide);
            return;
        }
        
        float distanceToEnemy = Statics.GetDistance(_caster, target);
        if (distanceToEnemy > spellData.ActualRange)
        {
            _caster._actionManager.AddToBuffer(spellData, target);
        }
        else
        {
            Launch(spellData, target);
        }
    }

    public void CastOnSelf(SpellData spellData)
    {
        if (_caster.CannotCastSpell())
        {
            Debug.Log("Impossible to cast");
            return;
        }
        
        //if (!SpellCDManager.instance.IsSpellReady(spellData.spellName))
        if (!spellData.IsReady())    
        {
            Debug.Log("Spell is on cooldown.");
            return;
        }
        
        if (_caster.mana < spellData.ManaCost)
        {
            Debug.Log("Not enough mana : " + _caster.mana + " < " + spellData.ManaCost);
            return;
        }
        
        spellData.UpdateParameters(_caster);
        if (!spellData.targeted)
        {
            Launch(spellData, _caster.transform.position);
        }
        else
        {
            Launch(spellData, spellData.Caster);
        }
    }
    
    private void Launch(SpellData spellData, Vector3 targetPosition)
    {
        GameObject spellObject = ObjectPooler.CreateSpell(spellData.spellName, _caster.transform.position);
        var spell = spellObject.GetComponent<Spell>();
        Vector3 launchPosition = GetLaunchPosition(targetPosition, spellData.ActualRange);
        spell.Init(_caster, launchPosition);
        
        _caster._actionManager.LookAt(launchPosition);
        
        ManageLaunch(spellData);
    }

    public void Launch(SpellData spellData, Entity target)
    {
        GameObject spellObject = ObjectPooler.CreateSpell(spellData.spellName, _caster.transform.position);
        var spell = spellObject.GetComponent<Spell>();
        spell.Init(_caster, target);

        _caster._actionManager.LookAt(target.transform.position);
        
        ManageLaunch(spellData);
    }
    
    private Vector3 GetLaunchPosition(Vector3 targetPosition, float range)
    {
        Vector3 displacement = Statics.GetVector(_caster.transform.position, targetPosition);
        float distance = Statics.GetDistance(_caster.transform.position, targetPosition);
        
        if (distance <= range)
        {
            return _caster.transform.position + displacement;
        }
        else
        {
            return _caster.transform.position + range * displacement / distance;
        }
    }

    private void ManageLaunch(SpellData spellData)
    {
        spellData.ResetTimer();
        _caster.ConsumeMana(spellData.ManaCost);
        _caster.HandleSpellLaunch(spellData.spellName);
    }
}

public enum SpellName
{
    // Cra
    CraAutoAttack,
    TirsPuissants,
    FlecheAssaillante,
    FlecheAssaillante2,
    FlecheEnpoisonnee,
    FlecheDeRecul,
    FlecheFulminante,
    
    // Ecaflip
    EcaAutoAttack,
    Roulette,
    Bluff,
    Griffe,
    BondDuFelin,
    Rekop,
    Rekop2,
    Rekop3,
    
    // Eliatrope
    EliaAutoAttack,
    Portail,
    EtoilesJumelles,
    Concentration,
    LameDeWakfu,
    RayonDeWakfu,
    
    // Eniripsa
    EniAutoAttack,
    ContreNature,
    MotVampirique,
    Murmure,
    MotDeJouvence,
    Reconstitution,
    
    // Enutrof
    EnuAutoAttack,
    Corruption,
    RoulageDePelle,
    RetraiteAnticipee,
    CoupDeGrisou,
    PelleMassacrante,
    
    // Feca
    FecaAutoAttack,
    Treve,
    Baton,
    Paturage,
    Transhumance,
    Barriere,
    
    // Forgelance
    ForgeAutoAttack,
    LanceDuLac,
    Estoc,
    Eclipse,
    Epilogue,
    Ragnarok,
    
    // Huppermage
    HupperAutoAttack,
    SurchargeRunique,
    
    Meteore,
    LameDeRoc,
    Seisme,
    Source,
    Glacier,
    Bulle,
    Bourrasque,
    Traversee,
    Ouragan,
    TraitArdent,
    Volcan,
    Deflagration,
    
    // Iop
    IopAutoAttack,
    Duel,
    Punch,
    EpeeCeleste,
    Bond,
    Colere,
    
    // Osamodas
    OsaAutoAttack,
    Invocation,
    Fouet,
    SuperBonbon,
    SouffleDudragon,
    Chimere,
    
    // Ouginak
    OugiAutoAttack,
    Proie,
    Molosse,
    Carcasse,
    Traque,
    Bestialite,
    
    // Pandawa
    PandaAutoAttack,
    Tonneau, 
    MainDePanawa,
    LaitDeBambou,
    GueuleDeBois,
    Chamrak,
    
    // Roublard
    RoubAutoAttack,
    Bombe,
    Tromblon,
    Fumigenes,
    Magnetisme,
    Poudre,
    
    // Sacrieur
    SacriAutoAttack,
    Mutilation,
    Hemorragie,
    Perfusion,
    LienDuSang,
    Chatiment,
    
    // Sadida
    SadiAutoAttack,
    Graine,
    RonceAggro,
    Mangrove,
    MaledictionVaudou,
    ForceDeLaNature,
    
    // Sram
    SramAutoAttack,
    Invisibilite,
    Larcin,
    PiegeMortel,
    CoupeGorge,
    MiseAMort,
    
    // Steamer
    SteamAutoAttack,
    Tourelle,
    Evolution,
    Surchauffe,
    Secourisme,
    LongueVue,
    Embuscade,
    
    // Xelor
    XelAutoAttack,
    Premonition,
    RayonObscur,
    Paradoxe,
    Aiguille,
    FrappeDuXelor,
    
    // Zobal
    ZobalAutoAttack,
    Masque,
    Furia,
    FuriaBoost,
    Transe,
    TranseBoost,
    Ponteira,
    PonteiraBoost,
    Mascarade,
    
    // Enemies
    Enemy1Auto,
    Enemy2Auto,
    Enemy3Auto,
    Enemy4Auto,
    Enemy5Auto,
    Enemy6Auto,
    Enemy7Auto,
    
    
    // Boss
    BossAuto,
    BossSpell,
    BossTeleport
}
