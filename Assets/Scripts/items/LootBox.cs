using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal.Internal;

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
    private Dictionary<int, Rarity> rarityTable = new Dictionary<int, Rarity>()
    {
        {1, Rarity.Commun},
        {2, Rarity.Rare},
        {3, Rarity.Epique}
    };

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
            Debug.Log("1");
            int rarity = 1;
            if (randomValue <= 85f && randomValue >= 55f)
            {
                Debug.Log("2");
                rarity = 2;
            }
            else if ( randomValue <= 100f && randomValue >= 85f)
            {
                Debug.Log("3");
                rarity = 3;
            }
            List<LootTable> droppableItems = new List<LootTable>();
            foreach (LootTable item in items)
            {
                if(item.item.rarity == rarityTable[rarity])
                {
                    Debug.Log("add");
                    droppableItems.Add(item);
                }
            }
            randomValue = Random.Range(0f, 100f);
            pourcentPasse = 0;
            float lootchance = (1f / droppableItems.Count) * 100;
            Debug.Log(lootchance);
            for (int i = 0; i < droppableItems.Count; i++)
            {
                Debug.Log("ch");
                if (randomValue >= pourcentPasse && randomValue < (pourcentPasse + lootchance))
                {
                    Debug.Log("tr");
                    return droppableItems[i].item;
                }
                pourcentPasse += lootchance;
            }

        }

        return null;
    }
}
