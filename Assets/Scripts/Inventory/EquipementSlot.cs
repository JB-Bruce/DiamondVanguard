using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

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
    [SerializeField] public Item startingItem;
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
        if (type == containerType.Weapon) 
        {
            UpdateWeapons((Weapons)startingItem);
        }
       
    }

    public void UpdateWeapons(Weapons weapon)
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

    public void UpdateWeaponsImage()
    {
        if (type == containerType.Weapon)
        {
            if (character.leftWeapon != item && !isRightItem)
            {
                addItem((Item)character.leftWeapon);
                imageUpdate();
            }
            if (character.rightWeapon != item && isRightItem)
            {
                addItem((Item)character.rightWeapon);
                imageUpdate();
            }
        }
    }

    public void AddStats()
    {
        if(type == containerType.Implant)
        {
            Implants itemstats = (Implants)item;
            character.pvMax += itemstats.HP;
            character.clampHP();
            character.dgtCritMult += itemstats.critDamage;
            character.tauxCrit += itemstats.critChance;
            character.cacDmgMult += itemstats.cacDamage;
            character.distDmgMult += itemstats.distanceDamage;
            character.healMult += itemstats.heal;
            character.energyMax += itemstats.energy;
            character.clampEnergy();
            character.def += itemstats.def;
        }
        else if (type == containerType.Helmet || type == containerType.Leging || type == containerType.Chestplate)
        {
            Armor itemstats = (Armor)item;
            character.pvMax += itemstats.HP;
            character.energyMax += itemstats.Energy;
            character.def += itemstats.Defense;
        }
    }

    public void SubStats()
    {
        if (type == containerType.Implant)
        {
            Implants itemstats = (Implants)item;
            character.pvMax -= itemstats.HP;
            character.clampHP();
            character.dgtCritMult -= itemstats.critDamage;
            character.tauxCrit -= itemstats.critChance;
            character.cacDmgMult -= itemstats.cacDamage;
            character.distDmgMult -= itemstats.distanceDamage;
            character.healMult -= itemstats.heal;
            character.energyMax -= itemstats.energy;
            character.clampEnergy();
            character.def -= itemstats.def;
        }
        else if (type == containerType.Helmet || type == containerType.Leging || type == containerType.Chestplate)
        {
            Armor itemstats = (Armor)item;
            character.pvMax -= itemstats.HP;
            character.energyMax -= itemstats.Energy;
            character.def -= itemstats.Defense;
        }
    }

    private void Update()
    {
        UpdateWeaponsImage();
    }
}
