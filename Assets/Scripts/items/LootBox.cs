using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBox : MonoBehaviour
{
    [System.Serializable]
    public enum lootType
    {
        Weapons = 0,
        Armors = 1,
        Implants = 2
    }

    public void CreateItem()
    {
        int ramdomValue = Random.Range(0, 100);
    }
}
