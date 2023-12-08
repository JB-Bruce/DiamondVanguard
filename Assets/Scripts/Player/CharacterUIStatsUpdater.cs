using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUIStatsUpdater : MonoBehaviour
{
    [SerializeField] Character currentCharacter;

    public Slider pvSlider;
    public Slider energySlider;

    private void Start()
    {
        currentCharacter.PvChangeEvent.AddListener(pvGaugeUi);
        currentCharacter.EnergyChangeEvent.AddListener(energyGaugeUi);
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
