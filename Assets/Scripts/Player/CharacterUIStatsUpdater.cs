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
    }

    public Character ResetCharacter()
    {
        Character newChar = currentCharacter;

        if (currentCharacter != null)
        {
            currentCharacter.PvChangeEvent.RemoveListener(pvGaugeUi);
            currentCharacter.PvChangeEvent.RemoveListener(energyGaugeUi);
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

}
