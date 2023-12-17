using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask itemMask;
    [SerializeField] private int caseRange;
    GameGrid gameGrid;

    LootBox selectedLootBox = null;


    private void Start()
    {
        gameGrid = GameGrid.instance;
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if ( Physics.Raycast(ray, out hit, (caseRange * gameGrid.cellSpacement), itemMask))
        {
            if (hit.transform.gameObject.layer == 6)
            {
                LootBox lootBox = hit.transform.gameObject.GetComponent<LootBox>();
                
                if(selectedLootBox != lootBox)
                {
                    if (selectedLootBox != null)
                        selectedLootBox.UnSelect();

                    selectedLootBox = lootBox;
                    selectedLootBox.Select();
                }

                if (Input.GetMouseButtonDown(0))
                {
                    Item item = lootBox.item;
                    if (inventory.TryAddItem(item))
                    {
                        inventory.addItem(item);
                        if (lootBox.isInfinit)
                        {
                            lootBox.respawn();
                        }
                        else
                        {
                            hit.transform.gameObject.SetActive(false);
                        }
                    }
                }
                return;
                
            } 
        }

        if(selectedLootBox != null)
        {
            selectedLootBox.UnSelect();
            selectedLootBox = null;
        }
    }

    
}
