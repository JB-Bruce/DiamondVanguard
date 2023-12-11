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

    private void Start()
    {
        SetUpCharacter(currentCharacter);
    }

    private void SetUpCharacter(Character character)
    {
        currentCharacter = character;
        imgPersonage.sprite = currentCharacter.characterImage;
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

        return newChar;
    }

    public void SetNewCharacter(Character character)
    {
        print("Set : " + character.name + " in " + transform.name);
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

}
