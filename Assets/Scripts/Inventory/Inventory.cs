using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;



public class Inventory : MonoBehaviour
{

    [SerializeField] private List<ItemContainer> inventoryGrid = new();
    [SerializeField]
    private int slots;
    [SerializeField] private GameObject Slots;
    [SerializeField] private GameObject SlotsPanel;
    private int i;



    private void Start()
    {
        gameObject.SetActive(false);
        for (int j = 0; j < 12; j++)
        {
            GameObject go = Instantiate(Slots, SlotsPanel.transform);
            inventoryGrid.Add(go.GetComponent<ItemContainer>());
        }
    }

    public bool TryAddItem(Item item)
    {
        for(i = 0; i < 12 ; i++)
        {
            if (!inventoryGrid[i].HasItem())
            {
                return true;
            }
        }
        return false;
    }

    public void addItem(Item item)
    {  
        if (TryAddItem(item))
        {
            inventoryGrid[i].item = item;
            inventoryGrid[i].itemImage.sprite = item.icon;
            inventoryGrid[i].imageUpdate();
        }
    }
}
