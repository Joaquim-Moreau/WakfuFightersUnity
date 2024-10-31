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
    [SerializeField] private List<Sprite> classSprites;
    [SerializeField] private Image backgroundImage;
    
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
        ChangeBackgroundImage();
    }
    
    public void SelectEca()
    {
        SelectionScreenData.ChosenClass = PlayerCLass.Ecaflip;
        ChangeBackgroundImage();
    }
    
    public void SelectElia()
    {
        SelectionScreenData.ChosenClass = PlayerCLass.Eliatrope;
        ChangeBackgroundImage();
    }
    
    public void SelectEni()
    {
        SelectionScreenData.ChosenClass = PlayerCLass.Eniripsa;
        ChangeBackgroundImage();
    }
    
    public void SelectEnu()
    {
        SelectionScreenData.ChosenClass = PlayerCLass.Enutrof;
        ChangeBackgroundImage();
    }
    
    public void SelectFeca()
    {
        SelectionScreenData.ChosenClass = PlayerCLass.Feca;
        ChangeBackgroundImage();
    }
    
    public void SelectForge()
    {
        SelectionScreenData.ChosenClass = PlayerCLass.Forgelance;
        ChangeBackgroundImage();
    }
    
    public void SelectHupper()
    {
        SelectionScreenData.ChosenClass = PlayerCLass.Huppermage;
        ChangeBackgroundImage();
    }
    
    public void SelectIop()
    {
        SelectionScreenData.ChosenClass = PlayerCLass.Iop;
        ChangeBackgroundImage();
    }
    
    public void SelectOsa()
    {
        SelectionScreenData.ChosenClass = PlayerCLass.Osamodas;
        ChangeBackgroundImage();
    }
    
    public void SelectOugi()
    {
        SelectionScreenData.ChosenClass = PlayerCLass.Ouginak;
        ChangeBackgroundImage();
    }
    
    public void SelectPanda()
    {
        SelectionScreenData.ChosenClass = PlayerCLass.Pandawa;
        ChangeBackgroundImage();
    }
    
    public void SelectRoub()
    {
        SelectionScreenData.ChosenClass = PlayerCLass.Roublard;
        ChangeBackgroundImage();
    }
    
    public void SelectSacri()
    {
        SelectionScreenData.ChosenClass = PlayerCLass.Sacrieur;
        ChangeBackgroundImage();
    }
    
    public void SelectSadi()
    {
        SelectionScreenData.ChosenClass = PlayerCLass.Sadida;
        ChangeBackgroundImage();
    }
    
    public void SelectSram()
    {
        SelectionScreenData.ChosenClass = PlayerCLass.Sram;
        ChangeBackgroundImage();
    }
    
    public void SelectSteam()
    {
        SelectionScreenData.ChosenClass = PlayerCLass.Steamer;
        ChangeBackgroundImage();
    }
    
    public void SelectXel()
    {
        SelectionScreenData.ChosenClass = PlayerCLass.Xelor;
        ChangeBackgroundImage();
    }
    
    public void SelectZobal()
    {
        SelectionScreenData.ChosenClass = PlayerCLass.Zobal;
        ChangeBackgroundImage();
    }

    private void ChangeBackgroundImage()
    {
        backgroundImage.sprite = classSprites[(int)SelectionScreenData.ChosenClass];
    }
}
