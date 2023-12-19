using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    public CharacterEnum charactereType;

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

    public bool canRightWeaponAttack = true;
    public bool canLeftWeaponAttack = true;

    public float coolDownRight, coolDownLeft, currentCDleft, currentCDright;

    public UnityEvent cdLeftChangedEvent { get; private set; } = new();

    public UnityEvent cdRightChangedEvent { get; private set; } = new();

    public UnityEvent equipWeaponREvent { get; private set; } = new();
    public UnityEvent equipWeaponLEvent { get; private set; } = new();

    [Header("VFXs")]
    public ScreenShake damagesScreenShake;
    public AttackSlash attackSlash;
    public MuzzleFlashManager fire;

    [SerializeField] AudioSource audioSource;


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

    public void clampHP()
    {
        pv = Mathf.Clamp(pv, 0, pvMax);
    }

    public void clampEnergy()
    {
        energy = Mathf.Clamp(energy, 0, energyMax);
    }

    public void TakeDamage(float amount)
    {
        float newAmount = amount - def;
        if (newAmount <= 0) 
            return;

        pv -= newAmount;
        pv = Mathf.Clamp(pv, 0, pvMax);
        damagesScreenShake.start = true;
        if (pv == 0)
        {
            //tuer personage
            dead = true;
            audioSource.PlayOneShot(controler.dyingSFX);
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
        if (currentCDright > 0)
        {
            currentCDright -= Time.deltaTime;
            if (currentCDright <= 0) 
            { 
                currentCDright = 0;
                ResetRightWeaponCoolDown();
            }
            cdRightChangedEvent.Invoke();
        }
        if (currentCDleft > 0) 
        { 
            currentCDleft -= Time.deltaTime;
            if (currentCDleft <= 0) 
            { 
                currentCDleft = 0;
                ResetLeftWeaponCoolDown();
            }
            cdLeftChangedEvent.Invoke();
        }
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
        if (canLeftWeaponAttack)
        {
            WeaponAttack(leftWeapon);
            canLeftWeaponAttack = false;
            coolDownLeft = leftWeapon.Cooldown;
            currentCDleft = coolDownLeft;
        }
    }

    private void ResetLeftWeaponCoolDown()
    {
        canLeftWeaponAttack = true;
    }

    public void RightWeaponAttack()
    {
        if (canRightWeaponAttack)
        {
            WeaponAttack(rightWeapon);
            canRightWeaponAttack = false;
            coolDownRight = rightWeapon.Cooldown;
            currentCDright = coolDownRight;
        }
    }

    private void ResetRightWeaponCoolDown()
    {
        canRightWeaponAttack = true;
    }

    private void WeaponAttack(Weapons newWeapon)
    {
            if (newWeapon == null)
            {
                pAttack.Attack(brutDamages);
                audioSource.PlayOneShot(controler.hitSFX);
            }
            else
            {
                float damages = newWeapon.damages;
                if (Random.Range(0f, 100f) <= tauxCrit)
                {
                    damages *= dgtCritMult;
                }
                if (newWeapon.itemType == Type.DistanceWeapon && newWeapon is DistanceWeapon)
                {
                    damages *= distDmgMult;
                    DistanceWeapon distWeapon = newWeapon as DistanceWeapon;
                    if (newWeapon.itemName == "Pistolet")
                {
                    audioSource.PlayOneShot(controler.makarovSFX);
                }
                    else if (newWeapon.itemName == "Shotgun")
                {
                    audioSource.PlayOneShot(controler.shotgunSFX);
                }
                    else { audioSource.PlayOneShot(controler.sniperSFX); }
                    pAttack.DistantAttack(damages, distWeapon.shootDistance);
                    fire.start = true;
                }
                else if (newWeapon.itemType == Type.MeleeWeapon)
                {
                    damages *= cacDmgMult;
                    if (newWeapon.itemName == "Mass" || newWeapon.itemName == "Matraque")
                {
                    audioSource.PlayOneShot(controler.massHitSFX);
                }
                    else { audioSource.PlayOneShot(controler.slashhitSFX); }
                    pAttack.Attack(damages);
                    attackSlash.start = true;
            }
                else if (newWeapon.itemType == Type.HealWeapon)
                {
                    damages *= healMult;
                    pAttack.Heal(controler, damages);
                audioSource.PlayOneShot(controler.healSFX);
                }
            }
    }

    public void EquipeRightWeapon(Weapons weapon)
    {
        rightWeapon = weapon;
        equipWeaponREvent.Invoke();
    }

    public void EquipeLeftWeapon(Weapons weapon)
    {
        leftWeapon = weapon;
        equipWeaponLEvent.Invoke();
    }

}
