using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    public float pv { get; private set; }
    public float energy { get; private set; }
    public Sprite characterImage;

    [SerializeField] float def;

    [SerializeField] float cacDmgMult;
    [SerializeField] float distDmgMult;
    [SerializeField] float tauxCrit;
    [SerializeField] float dgtCritMult;
    [SerializeField] float energyGainMult;
    [SerializeField] float healMult;

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
        print("healed");
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
        WeaponAttack(leftWeapon);
    }

    public void RightWeaponAttack()
    {
        WeaponAttack(rightWeapon);
    }

    private void WeaponAttack(Weapons newWeapon)
    {
        if (newWeapon == null)
        {
            pAttack.Attack(brutDamages);
        }
        else
        {
            float damages = newWeapon.damages;
            if (Random.Range(0f, 100f)<=tauxCrit)
            {
                damages *= dgtCritMult;
            }
            if (newWeapon.itemType == Type.DistanceWeapon && newWeapon is DistanceWeapon)
            {
                damages *= distDmgMult;
                DistanceWeapon distWeapon = newWeapon as DistanceWeapon;
                pAttack.DistantAttack(damages, distWeapon.shootDistance);
            }
            else if (newWeapon.itemType == Type.MeleeWeapon)
            {
                damages *= cacDmgMult;
                pAttack.Attack(damages);
            }
            else if (newWeapon.itemType == Type.HealWeapon)
            {
                damages *= healMult;
                pAttack.Heal(controler, damages);
            }
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
