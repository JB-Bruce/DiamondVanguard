using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
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
    public EquipementSlot leftSlot;
    public EquipementSlot rightSlot;

    public GameObject greyFilter;

    private void Start()
    {
        SetUpCharacter(currentCharacter);
    }

    private void SetUpCharacter(Character character)
    {
        rightAttack.interactable = true;
        leftAttack.interactable = true;
        currentCharacter = character;

        leftSlot.SetUpSlot(currentCharacter, currentCharacter.leftWeapon);
        rightSlot.SetUpSlot(currentCharacter, currentCharacter.rightWeapon);

        imgPersonage.sprite = currentCharacter.characterImage;
        leftWeapons.sprite = currentCharacter.leftWeapon.icon;
        rightWeapons.sprite = currentCharacter.rightWeapon.icon;
        currentCharacter.PvChangeEvent.AddListener(pvGaugeUi);
        currentCharacter.EnergyChangeEvent.AddListener(energyGaugeUi);
        pvGaugeUi();
        energyGaugeUi();
        currentCharacter.cdRightChangedEvent.AddListener(RightWeaponCoolDownChanged);
        currentCharacter.cdLeftChangedEvent.AddListener(LeftWeaponCoolDownChanged);
        currentCharacter.equipWeaponREvent.AddListener(RWeaponChanged);
        currentCharacter.equipWeaponLEvent.AddListener(LWeaponChanged);

        rightAttack.interactable = currentCharacter.coolDownRight == 0f;
        rightWeaponCoolDown.transform.localScale = new Vector3(1, (currentCharacter.coolDownRight == 0f ? 0 : currentCharacter.currentCDright / currentCharacter.coolDownRight), 1);
        leftAttack.interactable = currentCharacter.coolDownLeft == 0f;
        leftWeaponCoolDown.transform.localScale = new Vector3(1, (currentCharacter.coolDownLeft == 0f ? 0 : currentCharacter.currentCDleft / currentCharacter.coolDownLeft), 1);
    }

    public Character ResetCharacter()
    {
        Character newChar = currentCharacter;

        if (currentCharacter != null)
        {
            leftSlot.ResetSlot();
            rightSlot.ResetSlot();

            currentCharacter.PvChangeEvent.RemoveListener(pvGaugeUi);
            currentCharacter.EnergyChangeEvent.RemoveListener(energyGaugeUi);
            currentCharacter.cdRightChangedEvent.RemoveListener(RightWeaponCoolDownChanged);
            currentCharacter.cdLeftChangedEvent.RemoveListener(LeftWeaponCoolDownChanged);
            currentCharacter.equipWeaponREvent.RemoveListener(RWeaponChanged);
            currentCharacter.equipWeaponLEvent.RemoveListener(LWeaponChanged);

            rightAttack.interactable = false;
            rightWeaponCoolDown.transform.localScale = new Vector3(1, 0, 1);
            leftAttack.interactable = false;
            leftWeaponCoolDown.transform.localScale = new Vector3(1, 0, 1);

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

    private void RWeaponChanged()
    {
        rightSlot.addItem(currentCharacter.rightWeapon, false);
    }
    private void LWeaponChanged()
    {
        leftSlot.addItem(currentCharacter.leftWeapon, false);
    }
}
