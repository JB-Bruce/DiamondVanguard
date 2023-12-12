using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    public float pv { get; private set; }
    public float energy { get; private set; }
    public Sprite characterImage;

    [SerializeField] public float def;

    [SerializeField] public float cacDmgMult;
    [SerializeField] public float distDmgMult;
    [SerializeField] public float tauxCrit;
    [SerializeField] public float dgtCritMult;
    [SerializeField] public float energyGainMult;
    [SerializeField] public float healMult;


    [SerializeField] EquipementSlot ImplantSlot1;
    [SerializeField] EquipementSlot ImplantSlot2;
    [SerializeField] EquipementSlot ImplantSlot3;
    [SerializeField] EquipementSlot HelmetSlot;
    [SerializeField] EquipementSlot ChesplateSlot;
    [SerializeField] EquipementSlot LegingSlot;
    List<Implants> implants = new List<Implants>();
    List<EquipementSlot> armors = new List<EquipementSlot>();


    public CharactersControler controler;
    [SerializeField] float brutDamages;

    public float pvMax;
    public float energyMax; 
    private float dgt;

    public bool dead;

    public UnityEvent PvChangeEvent { get; private set; } = new();
    public UnityEvent EnergyChangeEvent { get; private set; } = new();

    public Weapons rightWeapon;
    public Weapons leftWeapon;

    private PlayerAttack pAttack;

    void Start()
    {
        energy = energyMax;
        pv = pvMax;
        pAttack = PlayerAttack.Instance;

        implants.Add((Implants)ImplantSlot1.item);
        implants.Add((Implants)ImplantSlot2.item);
        implants.Add((Implants)ImplantSlot3.item);
        armors.Add(HelmetSlot);
        armors.Add(ChesplateSlot);
        armors.Add(LegingSlot);
    }

    public void TakeDamage(float amount)
    {
        float newAmount = amount - def;
        if (newAmount <= 0) 
            return;

        pv -= newAmount;
        pv = Mathf.Clamp(pv, 0, pvMax);
        if (pv == 0)
        {
            //tuer personage
            dead = true;
            controler.Die(this);
        }
        PvChangeEvent.Invoke();
    }

    public void EnvironmentDamage(float amount)
    {
        if (amount <= 0)
            return;

        pv -= amount;
        pv = Mathf.Clamp(pv, 0, pvMax);
        if (pv == 0)
        {
            //tuer personage
            dead = true;
            controler.Die(this);
        }
        PvChangeEvent.Invoke();
    }


    void Update()
    {
        //reger d'energie
        energy += Time.deltaTime * energyGainMult;
        energy = Mathf.Clamp(energy, 0, energyMax);
    }

    public void Heal(float amount)
    {
        pv += amount;
        pv = Mathf.Clamp(pv, 0, pvMax);
        PvChangeEvent.Invoke();
    }

    public void ConsumeEnergy(float amount) 
    {
        energy -= amount;
        energy = Mathf.Clamp(energy, 0, energyMax);
        EnergyChangeEvent.Invoke();
    }

    public float atkLeft()
    {
        return dgt;
    }

    public float atkRight() 
    {
        return dgt;
    }

    public void LeftWeaponAttack()
    {
        if (leftWeapon == null)
        {
            pAttack.Attack(brutDamages);
        }
        else
        {
            pAttack.Attack(leftWeapon.damages);
        }
    }

    public void RightWeaponAttack()
    {
        if (rightWeapon == null)
        {
            pAttack.Attack(brutDamages);
        }
        else
        {
            pAttack.Attack(rightWeapon.damages);
        }
    }

    public void EquipeRightWeapon(Weapons weapon)
    {
        rightWeapon = weapon;
    }

    public void EquipeLeftWeapon(Weapons weapon)
    {
        rightWeapon = weapon;
    }

}
