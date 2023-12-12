using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LootBox : MonoBehaviour
{
    [System.Serializable]
    public enum lootType
    {
        weapons = 0,
        armors = 1,
        implants = 2
    }
    public Item item;

    [System.Serializable]
    public struct LootTable
    {
        public Rarity rarity;
        public Item item;
        public float dropChance;
    }

    public List<LootTable> items = new List<LootTable>();

    [SerializeField] private lootType type;

    private void Start()
    {
        item = CreateItem();
    }

    public Item CreateItem()
    {
        float randomValue = Random.Range(0f, 100f);
        float pourcentPasse = 0;
        if (type == lootType.weapons)
        {
            foreach (LootTable item in items)
            {
                if (randomValue >= pourcentPasse && randomValue < (pourcentPasse + item.dropChance))
                {
                    return item.item;
                }
                pourcentPasse += item.dropChance;
            }
            return null;
        }
        else
        {

        }

        return null;
    }
}
