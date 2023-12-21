using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsDisplayer : MonoBehaviour
{
    private Vector3 mousePos;
    [SerializeField] TextMeshProUGUI Header;
    [SerializeField] TextMeshProUGUI Content;
    [SerializeField] TextMeshProUGUI NegativeContent;
    [SerializeField] RectTransform rectTransform;

    [SerializeField] float xRatio;
    [SerializeField] float yRatio;

    int sWidth;
    int sHeight;

    private void Start()
    {
        sWidth = Screen.width;
        sHeight = Screen.height;
        gameObject.SetActive(false);
    }

    void Update()
    {
        UpdatePosition();
    }

    public void UpdatePosition()
    {
        mousePos = Input.mousePosition;
        transform.position = mousePos;

        if (mousePos.x > xRatio * sWidth)
        {
            rectTransform.pivot = new Vector2(1, rectTransform.pivot.y);
        }
        else
        {
            rectTransform.pivot = new Vector2(0, rectTransform.pivot.y);
        }

        if(mousePos.y < yRatio * sHeight)
        {
            rectTransform.pivot = new Vector2(rectTransform.pivot.x, 0);
        }
        else
        {
            rectTransform.pivot = new Vector2(rectTransform.pivot.x, 1);
        }
    }

    public void UpdateTexts(Item item)
    {
        Content.text = "";
        NegativeContent.text = "";
        Header.text = item.itemName;
        if(item.itemType == Type.MeleeWeapon || item.itemType == Type.HealWeapon)
        {
            Weapons items = (Weapons)item;
            Content.text = "\nEnery consomation : " + items.EnergyConso.ToString() + "\nCooldown : " + items.Cooldown.ToString() + "\nDamages : " + items.damages.ToString() + "\n ";
        }
        else if (item.itemType == Type.DistanceWeapon)
        {
            DistanceWeapon items = (DistanceWeapon)item;
            Content.text = "\nEnery consomation : " + items.EnergyConso.ToString() + "\nCooldown : " + items.Cooldown.ToString() + "\nDamages : " + items.damages.ToString() + "\nDistance : " + items.shootDistance.ToString() + "\n ";
        }
        else if (item.itemType == Type.Implant)
        {
            Implants items = (Implants)item;
            Dictionary<string, float> allStats = new()
            {
                { "Critical chance : ", items.critChance },
                { "Critical damages : ", items.critDamage },
                { "HP : ", items.HP },
                { "Healing : ", items.healing },
                { "Melee damages : ", items.cacDamage },
                { "Distance damages : ", items.distanceDamage },
                { "Heal Power : ", items.heal },
                { "Energy : ", items.energy },
                { "Defense : ", items.def }
            };
            foreach (string key in allStats.Keys)
            {
                float value = allStats[key];
                if (value > 0)
                {
                    Content.text +="\n" + key + "+" + value.ToString("F1");
                }
                else if (value < 0)
                {
                    NegativeContent.text += "\n" + key + value.ToString("F1");
                }
            }
        }

        else if (item.itemType == Type.Helmet || item.itemType == Type.Chestplate || item.itemType == Type.Leging)
        {
            Armor items = (Armor)item;
            if (items.HP != 0)
            {
                Content.text += "\nHP : " + items.HP.ToString();
            }
            if (items.Defense != 0)
            {
                Content.text += "\nDefense : " + items.Defense.ToString();
            }
            if (items.Energy != 0)
            {
                Content.text +="\nEnergy : " + items.Energy.ToString();
            }
            Content.text += "\n ";
        }
    }
}
