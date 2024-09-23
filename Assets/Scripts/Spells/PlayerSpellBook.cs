using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSpellBook : SpellBook
{
    [Header("Interface")] 
    private Image _imageSpace;
    private Image _onCoolDownImageSpace;
    private TMP_Text _timerSpace;
    
    private Image _imageA;
    private Image _onCoolDownImageA;
    private TMP_Text _timerA;
    
    private Image _imageZ;
    private Image _onCoolDownImageZ;
    private TMP_Text _timerZ;
    
    private Image _imageE;
    private Image _onCoolDownImageE;
    private TMP_Text _timerE;
    
    private Image _imageR;
    private Image _onCoolDownImageR;
    private TMP_Text _timerR;
    
    private void Start()
    {
        GameObject spellInterface = GameObject.FindWithTag("UI_spells");

        GameObject space = spellInterface.transform.Find("SpellSpace").gameObject;
        GameObject a = spellInterface.transform.Find("SpellA").gameObject;
        GameObject z = spellInterface.transform.Find("SpellZ").gameObject;
        GameObject e = spellInterface.transform.Find("SpellE").gameObject;
        GameObject r = spellInterface.transform.Find("SpellR").gameObject;

        _imageSpace = space.transform.Find("SpellIcon").gameObject.GetComponent<Image>();
        _onCoolDownImageSpace = space.transform.Find("CoolDownImage").gameObject.GetComponent<Image>();
        _timerSpace = space.transform.Find("TextCoolDown").gameObject.GetComponent<TMP_Text>();
        
        _imageA = a.transform.Find("SpellIcon").gameObject.GetComponent<Image>();
        _onCoolDownImageA = a.transform.Find("CoolDownImage").gameObject.GetComponent<Image>();
        _timerA = a.transform.Find("TextCoolDown").gameObject.GetComponent<TMP_Text>();
        
        _imageZ = z.transform.Find("SpellIcon").gameObject.GetComponent<Image>();
        _onCoolDownImageZ = z.transform.Find("CoolDownImage").gameObject.GetComponent<Image>();
        _timerZ = z.transform.Find("TextCoolDown").gameObject.GetComponent<TMP_Text>();
        
        _imageE = e.transform.Find("SpellIcon").gameObject.GetComponent<Image>();
        _onCoolDownImageE = e.transform.Find("CoolDownImage").gameObject.GetComponent<Image>();
        _timerE = e.transform.Find("TextCoolDown").gameObject.GetComponent<TMP_Text>();
        
        _imageR = r.transform.Find("SpellIcon").gameObject.GetComponent<Image>();
        _onCoolDownImageR = r.transform.Find("CoolDownImage").gameObject.GetComponent<Image>();
        _timerR = r.transform.Find("TextCoolDown").gameObject.GetComponent<TMP_Text>();

        _imageSpace.sprite = SpaceSpell[0].spellIcon;
        _onCoolDownImageSpace.fillAmount = 0f;
        _timerSpace.gameObject.SetActive(false);

        _imageA.sprite = ASpell[0].spellIcon;
        _onCoolDownImageA.fillAmount = 0f;
        _timerA.gameObject.SetActive(false);
        
        _imageZ.sprite = ZSpell[0].spellIcon;
        _onCoolDownImageZ.fillAmount = 0f;
        _timerZ.gameObject.SetActive(false);
        
        _imageE.sprite = ESpell[0].spellIcon;
        _onCoolDownImageE.fillAmount = 0f;
        _timerE.gameObject.SetActive(false);
        
        _imageR.sprite = RSpell[0].spellIcon;
        _onCoolDownImageR.fillAmount = 0f;
        _timerR.gameObject.SetActive(false);
    }

    protected override void Update()
    {
        UpdateSpellData();
        UpdateInterface();
    }
    
    private void UpdateInterface()
    {
        if (SpaceSpell[0].IsReady())
        {
            _onCoolDownImageSpace.fillAmount = 0f;
            _timerSpace.gameObject.SetActive(false);
        }
        else
        {
            _timerSpace.gameObject.SetActive(true);
            _onCoolDownImageSpace.fillAmount = SpaceSpell[0].GetCoolDownTimerPercent();
            _timerSpace.text = Mathf.Floor(SpaceSpell[0]._cooldownTimer).ToString();
        }
        
        if (ASpell[0].IsReady())
        {
            _onCoolDownImageA.fillAmount = 0f;
            _timerA.gameObject.SetActive(false);
        }
        else
        {
            _timerA.gameObject.SetActive(true);
            _onCoolDownImageA.fillAmount = ASpell[0].GetCoolDownTimerPercent();
            _timerA.text = Mathf.Floor(ASpell[0]._cooldownTimer).ToString();
        }
        
        if (ZSpell[0].IsReady())
        {
            _onCoolDownImageZ.fillAmount = 0f;
            _timerZ.gameObject.SetActive(false);
        }
        else
        {
            _timerZ.gameObject.SetActive(true);
            _onCoolDownImageZ.fillAmount = ZSpell[0].GetCoolDownTimerPercent();
            _timerZ.text = Mathf.Floor(ZSpell[0]._cooldownTimer).ToString();
        }
        
        if (ESpell[0].IsReady())
        {
            _onCoolDownImageE.fillAmount = 0f;
            _timerE.gameObject.SetActive(false);
        }
        else
        {
            _timerE.gameObject.SetActive(true);
            _onCoolDownImageE.fillAmount = ESpell[0].GetCoolDownTimerPercent();
            _timerE.text = Mathf.Floor(ESpell[0]._cooldownTimer).ToString();
        }
        
        if (RSpell[0].IsReady())
        {
            _onCoolDownImageR.fillAmount = 0f;
            _timerR.gameObject.SetActive(false);
        }
        else
        {
            _timerR.gameObject.SetActive(true);
            _onCoolDownImageR.fillAmount = RSpell[0].GetCoolDownTimerPercent();
            _timerR.text = Mathf.Floor(RSpell[0]._cooldownTimer).ToString();
        }
    }
}
