using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterDamageIndicator : MonoBehaviour
{
    [SerializeField] GameObject damagePrefab;
    [SerializeField] GameObject healPrefab;

    [SerializeField] RectTransform t;

    public void TakeDamage(float amount)
    {
        GameObject go = Instantiate(damagePrefab, t.position, Quaternion.identity, t);
        go.GetComponent<TextMeshProUGUI>().text = "-" + Mathf.RoundToInt(amount).ToString();
    }

    public void Heal(float amount)
    {
        GameObject go = Instantiate(healPrefab, t.position, Quaternion.identity, t);
        go.GetComponent<TextMeshProUGUI>().text = "+" + Mathf.RoundToInt(amount).ToString();
    }
}
