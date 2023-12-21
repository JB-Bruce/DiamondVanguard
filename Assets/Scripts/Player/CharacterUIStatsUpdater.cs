using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public GameObject rightCDBlockRaycast;
    public GameObject leftCDBlockRaycast;

    public EquipementSlot leftSlot;
    public EquipementSlot rightSlot;

    public GameObject greyFilter;
    public TextMeshProUGUI lifeText;
    public TextMeshProUGUI energyText;

    public CharacterDamageIndicator linkedCDI;

    private void Start()
    {
        rightCDBlockRaycast.SetActive(false);
        leftCDBlockRaycast.SetActive(false);
        SetUpCharacter(currentCharacter);
    }

    private void SetUpCharacter(Character character)
    {
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

        rightAttack.interactable = currentCharacter.canRightWeaponAttack;
        rightWeaponCoolDown.transform.localScale = new Vector3(1, (currentCharacter.canRightWeaponAttack ? 0 : currentCharacter.currentCDright / currentCharacter.coolDownRight), 1);
        leftAttack.interactable = currentCharacter.canLeftWeaponAttack;
        leftWeaponCoolDown.transform.localScale = new Vector3(1, (currentCharacter.canLeftWeaponAttack ? 0 : currentCharacter.currentCDleft / currentCharacter.coolDownLeft), 1);

        lifeText.text = currentCharacter.pv + "/" + currentCharacter.pvMax;
        energyText.text = Mathf.RoundToInt(currentCharacter.energy) + "/" + Mathf.RoundToInt(currentCharacter.energyMax);

        currentCharacter.CDI = linkedCDI;
    }

    public Character ResetCharacter()
    {
        Character newChar = currentCharacter;

        if (currentCharacter != null)
        {
            leftSlot.ResetSlot();
            rightSlot.ResetSlot();

            currentCharacter.CDI = null;

            currentCharacter.PvChangeEvent.RemoveListener(pvGaugeUi);
            currentCharacter.EnergyChangeEvent.RemoveListener(energyGaugeUi);
            currentCharacter.cdRightChangedEvent.RemoveListener(RightWeaponCoolDownChanged);
            currentCharacter.cdLeftChangedEvent.RemoveListener(LeftWeaponCoolDownChanged);
            currentCharacter.equipWeaponREvent.RemoveListener(RWeaponChanged);
            currentCharacter.equipWeaponLEvent.RemoveListener(LWeaponChanged);

            rightWeaponCoolDown.transform.localScale = new Vector3(1, 0, 1);
            rightCDBlockRaycast.SetActive(false);
            leftWeaponCoolDown.transform.localScale = new Vector3(1, 0, 1);
            leftCDBlockRaycast.SetActive(false);

            currentCharacter = null;
        }
        
        pvSlider.value = 0;
        energySlider.value = 0;
        imgPersonage.sprite = null;
        leftWeapons.sprite = null;
        rightWeapons.sprite = null;
        rightAttack.interactable = false;
        leftAttack.interactable = false;

        lifeText.text = "";
        energyText.text = "";

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

        lifeText.text = currentCharacter.pv + "/" + currentCharacter.pvMax;
    }

    public void energyGaugeUi() 
    {
        energySlider.value =currentCharacter.energy/currentCharacter.energyMax;

        energyText.text = Mathf.RoundToInt(currentCharacter.energy) + "/" + Mathf.RoundToInt(currentCharacter.energyMax);
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
            rightCDBlockRaycast.SetActive(false);
        }
        else
        {
            rightAttack.interactable = false;
            rightCDBlockRaycast.SetActive(true);
            rightWeaponCoolDown.transform.localScale = new Vector3(1, currentCharacter.currentCDright/currentCharacter.coolDownRight, 1);
        }
    }

    public void LeftWeaponCoolDownChanged()
    {
        if (currentCharacter.currentCDleft == 0)
        {
            leftAttack.interactable = true;
            leftWeaponCoolDown.transform.localScale = new Vector3(1, 0, 1);
            leftCDBlockRaycast.SetActive(false);
        } 
        else
        {
            leftAttack.interactable = false;
            leftCDBlockRaycast.SetActive(true);
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
