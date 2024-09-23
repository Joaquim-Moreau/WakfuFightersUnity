using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SelectionScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject firstAButton;
    [SerializeField] private GameObject secondAButton;
    [SerializeField] private GameObject thirdAButton;
    [SerializeField] private GameObject firstZButton;
    [SerializeField] private GameObject secondZButton;
    [SerializeField] private GameObject thirdZButton;
    [SerializeField] private GameObject firstEButton;
    [SerializeField] private GameObject secondEButton;
    [SerializeField] private GameObject thirdEButton;
    [SerializeField] private GameObject firstRButton;
    [SerializeField] private GameObject secondRButton;
    [SerializeField] private GameObject thirdRButton;

    private Image[] _imagesA;
    private Image[] _imagesZ;
    private Image[] _imagesE;
    private Image[] _imagesR;

    
    private TextMeshProUGUI[] _textA;
    private TextMeshProUGUI[] _textZ;
    private TextMeshProUGUI[] _textE;
    private TextMeshProUGUI[] _textR;
    
    private void Awake()
    {
        SelectionScreenData.ChosenClass = PlayerCLass.Cra;
    }

    public void EnterGame()
    {
        SceneManager.LoadScene("MainScene");
    }
    
    public void SelectCra()
    {
        SelectionScreenData.ChosenClass = PlayerCLass.Cra;
    }
    
    public void SelectEca()
    {
        SelectionScreenData.ChosenClass = PlayerCLass.Ecaflip;
    }
    
    public void SelectElia()
    {
        SelectionScreenData.ChosenClass = PlayerCLass.Eliatrope;
    }
    
    public void SelectEni()
    {
        SelectionScreenData.ChosenClass = PlayerCLass.Eniripsa;
    }
    
    public void SelectEnu()
    {
        SelectionScreenData.ChosenClass = PlayerCLass.Enutrof;
    }
    
    public void SelectFeca()
    {
        SelectionScreenData.ChosenClass = PlayerCLass.Feca;
    }
    
    public void SelectForge()
    {
        SelectionScreenData.ChosenClass = PlayerCLass.Forgelance;
    }
    
    public void SelectHupper()
    {
        SelectionScreenData.ChosenClass = PlayerCLass.Huppermage;
    }
    
    public void SelectIop()
    {
        SelectionScreenData.ChosenClass = PlayerCLass.Iop;
    }
    
    public void SelectOsa()
    {
        SelectionScreenData.ChosenClass = PlayerCLass.Osamodas;
    }
    
    public void SelectOugi()
    {
        SelectionScreenData.ChosenClass = PlayerCLass.Ouginak;
    }
    
    public void SelectPanda()
    {
        SelectionScreenData.ChosenClass = PlayerCLass.Pandawa;
       
    }
    
    public void SelectRoub()
    {
        SelectionScreenData.ChosenClass = PlayerCLass.Roublard;
        
    }
    
    public void SelectSacri()
    {
        SelectionScreenData.ChosenClass = PlayerCLass.Sacrieur;
       
    }
    
    public void SelectSadi()
    {
        SelectionScreenData.ChosenClass = PlayerCLass.Sadida;
    }
    
    public void SelectSram()
    {
        SelectionScreenData.ChosenClass = PlayerCLass.Sram;
    }
    
    public void SelectSteam()
    {
        SelectionScreenData.ChosenClass = PlayerCLass.Steamer;
    }
    
    public void SelectXel()
    {
        SelectionScreenData.ChosenClass = PlayerCLass.Xelor;
    }
    
    public void SelectZobal()
    {
        SelectionScreenData.ChosenClass = PlayerCLass.Zobal;
    }
}