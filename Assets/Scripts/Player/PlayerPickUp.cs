using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask itemMask;


    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if ( Physics.Raycast(ray, out hit, itemMask))
        {
            if (Input.GetMouseButtonDown(0))
            {
                Item item = hit.transform.gameObject.GetComponent<LootBox>().item;
                inventory.addItem(item);
                hit.transform.gameObject.SetActive(false);
            }
            

            
        }
    }
}
