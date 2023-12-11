using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipementSlot : ItemContainer
{
    [System.Serializable]
    public enum containerType
    {
        Weapon = 0,
        Implant = 1,
        Helmet = 2,
        Chestplate = 3,
        Leging = 4
    }

    [SerializeField] Character character;
    [SerializeField] Weapons startingItem;
    public bool isRightItem;

    public containerType type;

    private void Start()
    {
        if (item == null)
        {
            SetHandItem();
        }
    }

    public bool TryAddEquipement(Item _item)
    {
        if ((type == containerType.Weapon && (_item.itemType == Type.MeleeWeapon || _item.itemType == Type.DistanceWeapon || _item.itemType == Type.HealWeapon))
            || (type == containerType.Implant && _item.itemType == Type.Implant) || (type == containerType.Helmet && _item.itemType == Type.Helmet)
            || (type == containerType.Chestplate && _item.itemType == Type.Chestplate) || (type == containerType.Leging && _item.itemType == Type.Leging))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetHandItem()
    {
        item = startingItem;
        itemImage.sprite = startingItem.icon;
        itemImage.gameObject.SetActive(true);
        UpdateStats(startingItem);
    }

    public void UpdateStats(Weapons weapon)
    {
        if(isRightItem)
        {
            character.rightWeapon = weapon;
        }
        else
        {
            character.leftWeapon = weapon;
        }
    }
}
