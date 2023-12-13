using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class FormationChanger : MonoBehaviour
{
    private GameObject LastContainer;
    private GameObject image;
    [SerializeField] EventSystem eventSystem;
    [SerializeField] GraphicRaycaster m_Raycaster;
    private bool draging = false;
    [SerializeField] private GameObject DragNDrop;

    private void Update()
    {
        Vector3 MousePos = Input.mousePosition;
        PointerEventData pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = MousePos;

        DragNDrop.transform.position = MousePos;

        List<RaycastResult> results = new List<RaycastResult>();

        m_Raycaster.Raycast(pointerEventData, results);

        // if the mouse is in an element
        if (results.Count > 0)
        {
            GameObject result = results[0].gameObject;

            // stop drag and drop
            if (!draging && LastContainer != null && image != null)
            {
                ItemReturn();
                image.transform.SetParent(LastContainer.transform);
                LastContainer = null;
            }

            // draging item from slot
            if (Input.GetMouseButtonDown(0) && result.tag == "PersoSlot")
            {
                draging = true;
                image = result;
                LastContainer = image.transform.parent.gameObject;
                image.transform.SetParent(DragNDrop.transform);
            }

            if (Input.GetMouseButtonUp(0) && (draging || image != null))
            {
                draging = false;
                ItemReturn();
                image.transform.SetParent(LastContainer.transform);
                image = null;
            }
        }
    }


    // methode that return item to his old slot
    private void ItemReturn()
    {
        image.transform.position = LastContainer.transform.position;

    }
}

