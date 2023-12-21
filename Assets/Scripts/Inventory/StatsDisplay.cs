using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsDisplay : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private TextMeshProUGUI HP;
    [SerializeField] private TextMeshProUGUI Stamina;
    [SerializeField] private TextMeshProUGUI StaminaRegen;
    [SerializeField] private TextMeshProUGUI Defense;
    [SerializeField] private TextMeshProUGUI Cac;
    [SerializeField] private TextMeshProUGUI Distance;
    [SerializeField] private TextMeshProUGUI Heal;
    [SerializeField] private TextMeshProUGUI CritChance;
    [SerializeField] private TextMeshProUGUI CritDamage;


    public void UpdateStats()
    {
        HP.GetComponent<TextMeshProUGUI>().text = character.pv < 0 ? "0" : Mathf.Round(character.pv).ToString();
        Stamina.GetComponent<TextMeshProUGUI>().text = character.energy < 0 ? "0" : Mathf.Round(character.energy).ToString();
        StaminaRegen.GetComponent<TextMeshProUGUI>().text = character.energyGainMult.ToString();
        Defense.GetComponent<TextMeshProUGUI>().text = character.def < 0 ? "0" : character.def.ToString();
        Cac.GetComponent<TextMeshProUGUI>().text = character.cacDmgMult < 0 ? "0" : character.cacDmgMult.ToString();
        Distance.GetComponent<TextMeshProUGUI>().text = character.distDmgMult < 0 ? "0" : character.distDmgMult.ToString();
        Heal.GetComponent<TextMeshProUGUI>().text = character.healMult < 0 ? "0" : character.healMult.ToString();
        CritChance.GetComponent<TextMeshProUGUI>().text = character.tauxCrit < 0 ? "0" : character.tauxCrit.ToString();
        CritDamage.GetComponent<TextMeshProUGUI>().text = character.dgtCritMult < 0 ? "0" : character.dgtCritMult.ToString();

    }

    private void Update()
    {
        UpdateStats();
    }
}
