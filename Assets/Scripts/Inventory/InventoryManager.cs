using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using Unity.VisualScripting;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Item handItem;
    private ItemContainer LastItemContainer;
    private Item item = null;
    [SerializeField] private Inventory inventory;
    [SerializeField] EventSystem eventSystem;
    [SerializeField] GraphicRaycaster m_Raycaster;
    private bool draging = false;
    [SerializeField] private GameObject DragNDrop;


    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (inventory.gameObject.activeInHierarchy)
        {
            Vector3 MousePos = Input.mousePosition;
            PointerEventData pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = MousePos;

            List<RaycastResult> results = new List<RaycastResult>();

            m_Raycaster.Raycast(pointerEventData, results);


            // drag and drop
            if (draging)
            {
                LastItemContainer.GetComponent<ItemContainer>().itemImage.transform.SetParent(DragNDrop.transform);
                DragNDrop.transform.position = MousePos;
            }

            // stop drag and drop
            else if (!draging && LastItemContainer != null)
            {
                LastItemContainer.GetComponent<ItemContainer>().itemImage.transform.SetParent(LastItemContainer.transform);
            }

            // if the mouse is in an element
            if (results.Count > 0)
            {
                // draging item from slot
                if (Input.GetMouseButtonDown(0) && ( (results[0].gameObject.tag == "Slots" && results[0].gameObject.GetComponent<ItemContainer>().item != null) 
                    || (results[0].gameObject.tag == "Equipement" && (results[0].gameObject.GetComponent<EquipementSlot>().item != handItem || results[0].gameObject.GetComponent<ItemContainer>().item == null) )))
                {
                    GameObject itemContainer = results[0].gameObject;
                    item = itemContainer.GetComponent<ItemContainer>().item;
                    LastItemContainer = itemContainer.GetComponent<ItemContainer>();
                    draging = true;
                    DragNDrop.transform.position = MousePos;
                }


                // mouse click off
                if (Input.GetMouseButtonUp(0) && draging)
                {
                    draging = false;


                    // if in a slot
                    if ((results[0].gameObject.tag == "Equipement" && results[0].gameObject.GetComponent<EquipementSlot>().item == handItem && results[0].gameObject.GetComponent<EquipementSlot>().TryAddEquipement(item))
                        || (results[0].gameObject.tag == "Slots" && results[0].gameObject.GetComponent<ItemContainer>().item == null))
                    {
                        SetItemInSlot(results);
                        SetItemInLastContainer(LastItemContainer);
                        item = null;
                    }

                    // if in trash
                    else if(results[0].gameObject.tag == "Trash")
                    {
                        item = null;
                        draging = false;
                        LastItemContainer.gameObject.GetComponent<ItemContainer>().item = null;
                        LastItemContainer.gameObject.GetComponent<ItemContainer>().imageUpdate();
                        LastItemContainer.GetComponent<ItemContainer>().itemImage.transform.position = LastItemContainer.transform.position;
                    }

                    // if not in slots
                    else
                    {
                        itemReturn();
                    }
                }
            }
            // if in nothing
            if (Input.GetMouseButtonUp(0) && draging)
            {
                itemReturn();
            }
                
        }
    }

    // methode that return item to his old slot
    private void itemReturn()
    {
        LastItemContainer.gameObject.GetComponent<ItemContainer>().addItem(item);
        LastItemContainer.gameObject.GetComponent<ItemContainer>().imageUpdate();
        item = null;
        draging = false;
        LastItemContainer.GetComponent<ItemContainer>().itemImage.transform.position = LastItemContainer.transform.position;
    }

    // methode to set item in a slot
    private void SetItemInSlot(List<RaycastResult> results)
    {
        results[0].gameObject.GetComponent<ItemContainer>().addItem(item);
        results[0].gameObject.GetComponent<ItemContainer>().imageUpdate();
    }

    private void SetItemInLastContainer(ItemContainer LastItemContainer)
    {
        LastItemContainer.item = null;
        LastItemContainer.GetComponent<ItemContainer>().imageUpdate();
        LastItemContainer.GetComponent<ItemContainer>().itemImage.transform.position = LastItemContainer.transform.position;
        if (LastItemContainer.gameObject.tag == "Equipement")
        {
            LastItemContainer.GetComponent<EquipementSlot>().SetHandItem();
        }
    }
}
