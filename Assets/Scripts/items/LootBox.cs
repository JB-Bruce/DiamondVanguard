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

    public bool isInfinit;
    [SerializeField] private float respawnH;


    [SerializeField] private lootType type;

    private void Start()
    {
        item = CreateItem();
    }

    public void respawn()
    {
        Vector3 newPos = new Vector3(gameObject.transform.position.x , gameObject.transform.position.y + respawnH, gameObject.transform.position.z);
        gameObject.transform.position = newPos;
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
            int rarity = 1;
            if (randomValue <= 85f && randomValue >= 55f)
            {
                rarity = 2;
            }
            else if ( randomValue <= 100f && randomValue >= 85f)
            {
                rarity = 3;
            }
            List<LootTable> droppableItems = new List<LootTable>();
            foreach (LootTable item in items)
            {
                if(item.item.rarity == rarityTable[rarity])
                {
                    droppableItems.Add(item);
                }
            }
            randomValue = Random.Range(0f, 100f);
            pourcentPasse = 0;
            float lootchance = (1f / droppableItems.Count) * 100;
            for (int i = 0; i < droppableItems.Count; i++)
            {
                if (randomValue >= pourcentPasse && randomValue < (pourcentPasse + lootchance))
                {
                    return droppableItems[i].item;
                }
                pourcentPasse += lootchance;
            }

        }

        return null;
    }
}
