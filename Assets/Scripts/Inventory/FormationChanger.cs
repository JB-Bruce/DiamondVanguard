using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;
using UnityEditor.Experimental.GraphView;

public class FormationChanger : MonoBehaviour
{
    private GameObject LastPanel;
    private GameObject NewPanel;
    private GameObject image;
    private GameObject newImage;
    private GameObject lastUiCharacter;
    private GameObject nextUiCharacter;

    [SerializeField] EventSystem eventSystem;
    [SerializeField] GraphicRaycaster m_Raycaster;
    private bool draging = false;
    [SerializeField] private GameObject DragNDrop;
    private bool isInOtherImage;

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
            if (!draging && LastPanel != null && image != null)
            {
                ItemReturn();
                image.transform.SetParent(LastPanel.transform);
                LastPanel = null;
            }

            // draging personnages from slot
            if (Input.GetMouseButtonDown(0) && result.tag == "PersoSlot")
            {
                draging = true;
                image = result;
                LastPanel = image.transform.parent.gameObject;
                image.transform.SetParent(DragNDrop.transform);
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (results.Count > 1)
                {
                    isInOtherImage = false;
                    for (int i = 1; i < results.Count; i++)
                    {
                        if (results[i].gameObject.tag == "PersoSlot")
                        {
                            NewPanel = results[i].gameObject.transform.parent.gameObject;
                            newImage = results[i].gameObject;
                            isInOtherImage = true;
                        }
                    }
                }
                if (!isInOtherImage)
                {
                    draging = false;
                    ItemReturn();
                    image.transform.SetParent(LastPanel.transform);
                    image = null;
                }
                else if (results.Count > 1 && isInOtherImage)
                {
                    lastUiCharacter = LastPanel.transform.parent.gameObject;
                    nextUiCharacter = NewPanel.transform.parent.gameObject;
                    ChangeImage(lastUiCharacter, nextUiCharacter);
                    ItemReturn();
                    draging = false;
                    image.transform.SetParent(LastPanel.transform);
                    image = null;
                }
            }
        }
    }


    // methode that return personnages to his old slot
    private void ItemReturn()
    {
        image.transform.position = new Vector3(LastPanel.transform.position.x - 80, LastPanel.transform.position.y, LastPanel.transform.position.z);

    }

    // methode that switch personnage with an other
    private void ChangeImage(GameObject lastUiCharacter, GameObject nextUiCharacter)
    {
        GameObject lastUiCharacterParent = lastUiCharacter.transform.parent.gameObject;
        lastUiCharacter.transform.SetParent(nextUiCharacter.transform.parent.gameObject.transform);
        nextUiCharacter.transform.SetParent(lastUiCharacterParent.transform);
        lastUiCharacter.transform.position = lastUiCharacter.transform.parent.gameObject.transform.position;
        nextUiCharacter.transform.position = nextUiCharacter.transform.parent.gameObject.transform.position;
        newImage.transform.position = new Vector3(NewPanel.transform.position.x - 80, NewPanel.transform.position.y, NewPanel.transform.position.z);
        Debug.Log("change");
    }
}

