using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] float pv;
    [SerializeField] float def;
    [SerializeField] float energy;

    [SerializeField] float cacDmgMult;
    [SerializeField] float distDmgMult;
    [SerializeField] float tauxCrit;
    [SerializeField] float dgtCritMult;
    [SerializeField] float energyGainMult;
    [SerializeField] float healMult;

    private float pvMax;
    private float energyMax; 
    private float dgt;

    void Start()
    {
        energyMax = 100;
        pvMax = pv;
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
        }
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
        }
    }


    void Update()
    {
        //reger d'energie
        energy += Time.deltaTime * energyGainMult;
        energy = Mathf.Clamp(energy, 0, energyMax);
    }

    public void Heal(float amount)
    {
        pv += amount * healMult;
        pv = Mathf.Clamp(pv, 0, pvMax);
    }

    public void consumeEnergy(float amount) 
    {
        energy -= amount;
        energy = Mathf.Clamp(energy, 0, energyMax);
    }

    public float atkLeft()
    {
        return dgt;
    }

    public float atkRight() 
    {
        return dgt;
    }
}
