using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUIStatsUpdater : MonoBehaviour
{
    public Character currentCharacter;

    public Slider pvSlider;
    public Slider energySlider;
    public Image imgPersonage;
    public Image rightWeapons;
    public Image leftWeapons;
    public Button rightAttack;
    public Button leftAttack;
    public Image rightWeaponCoolDown;
    public Image leftWeaponCoolDown;

    private void Start()
    {
        SetUpCharacter(currentCharacter);
    }

    private void SetUpCharacter(Character character)
    {
        rightAttack.interactable = true;
        leftAttack.interactable = true;
        currentCharacter = character;
        imgPersonage.sprite = currentCharacter.characterImage;
        leftWeapons.sprite = currentCharacter.leftWeapon.icon;
        rightWeapons.sprite = currentCharacter.rightWeapon.icon;
        currentCharacter.PvChangeEvent.AddListener(pvGaugeUi);
        currentCharacter.EnergyChangeEvent.AddListener(energyGaugeUi);
        pvGaugeUi();
        energyGaugeUi();
        currentCharacter.cdRightChangedEvent.AddListener(RightWeaponCoolDownChanged);
        currentCharacter.cdLeftChangedEvent.AddListener(LeftWeaponCoolDownChanged);
    }

    public Character ResetCharacter()
    {
        Character newChar = currentCharacter;

        if (currentCharacter != null)
        {
            currentCharacter.PvChangeEvent.RemoveListener(pvGaugeUi);
            currentCharacter.EnergyChangeEvent.RemoveListener(energyGaugeUi);
            currentCharacter.cdRightChangedEvent.RemoveListener(RightWeaponCoolDownChanged);
            currentCharacter.cdLeftChangedEvent.RemoveListener(LeftWeaponCoolDownChanged);
            currentCharacter = null;
        }
        
        pvSlider.value = 0;
        energySlider.value = 0;
        imgPersonage.sprite = null;
        leftWeapons.sprite = null;
        rightWeapons.sprite = null;
        rightAttack.interactable = false;
        leftAttack.interactable = false;

        return newChar;
    }

    public void SetNewCharacter(Character character)
    {
        ResetCharacter();
        SetUpCharacter(character);
    }

    public void pvGaugeUi()
    {
        pvSlider.value = currentCharacter.pv/currentCharacter.pvMax;
    }

    public void energyGaugeUi() 
    {
        energySlider.value =currentCharacter.energy/currentCharacter.energyMax;
    }

    public void LeftWeaponAttack() 
    {
        currentCharacter.LeftWeaponAttack();
    }

    public void RightWeaponAttack() 
    {
        currentCharacter.RightWeaponAttack();
    }

    public void RightWeaponCoolDownChanged()
    {
        if (currentCharacter.currentCDright == 0) 
        { 
            rightAttack.interactable = true;
            rightWeaponCoolDown.transform.localScale = new Vector3(1, 0, 1);
        }
        else
        {
            rightAttack.interactable = false;
            rightWeaponCoolDown.transform.localScale = new Vector3(1, currentCharacter.currentCDright/currentCharacter.coolDownRight, 1);
        }
    }

    public void LeftWeaponCoolDownChanged()
    {
        if (currentCharacter.currentCDleft == 0)
        {
            leftAttack.interactable = true;
            leftWeaponCoolDown.transform.localScale = new Vector3(1, 0, 1);
        } 
        else
        {
            leftAttack.interactable = false;
            leftWeaponCoolDown.transform.localScale = new Vector3(1, currentCharacter.currentCDleft/currentCharacter.coolDownLeft, 1);
        }
    }
}
