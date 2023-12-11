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

    public void UpdateStats()
    {
        HP.GetComponent<TextMeshProUGUI>().text = character.pv.ToString();
        Stamina.GetComponent<TextMeshProUGUI>().text = character.energy.ToString();
        StaminaRegen.GetComponent<TextMeshProUGUI>().text = character.energyGainMult.ToString();
        Defense.GetComponent<TextMeshProUGUI>().text = character.def.ToString();
        Cac.GetComponent<TextMeshProUGUI>().text = character.cacDmgMult.ToString();
        Distance.GetComponent<TextMeshProUGUI>().text = character.distDmgMult.ToString();
        Heal.GetComponent<TextMeshProUGUI>().text = character.healMult.ToString();

    }

    private void Update()
    {
        UpdateStats();
    }
}
