using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask itemMask;
    [SerializeField] private int caseRange;
    GameGrid gameGrid;


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
            if (Input.GetMouseButtonDown(0) && hit.transform.gameObject.layer == 6)
            {
                Item item = hit.transform.gameObject.GetComponent<LootBox>().item;
                LootBox lootBox = hit.transform.gameObject.GetComponent<LootBox>();
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
        }
    }
}
