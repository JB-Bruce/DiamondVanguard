using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FormationChanger : MonoBehaviour
{
    private GameObject LastPanel;
    private GameObject NextPanel;
    private GameObject image;
    private CharacterUIStatsUpdater CharacterUi1;
    private CharacterUIStatsUpdater CharacterUi2;

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
                CharacterUi1 = LastPanel.GetComponentInParent<CharacterUIStatsUpdater>();
                Debug.Log(CharacterUi1);
            }

            if (Input.GetMouseButtonUp(0) && draging)
            {
                if (results.Count > 1)
                {
                    isInOtherImage = false;
                    for (int i = 1; i < results.Count; i++)
                    {
                        if (results[i].gameObject.tag == "PersoSlot")
                        {
                            isInOtherImage = true;
                            GameObject NewPanel = results[i].gameObject.transform.parent.gameObject;
                            CharacterUi2 = NewPanel.GetComponentInParent<CharacterUIStatsUpdater>();
                            Debug.Log(CharacterUi2);
                        }
                    }
                }
                draging = false;
                if (!isInOtherImage)
                {
                    ItemReturn();
                    image.transform.SetParent(LastPanel.transform);
                    image = null;
                }
                else if (results.Count > 1 && isInOtherImage)
                {
                    ChangeImage();
                    ItemReturn();
                    image.transform.SetParent(LastPanel.transform);
                    image = null;
                }
            }
        }
    }


    // methode that return personnages to his old slot
    private void ItemReturn()
    {
        if (image.tag == "PersoSlot")
        {
            image.transform.position = new Vector3(LastPanel.transform.position.x - 80, LastPanel.transform.position.y, LastPanel.transform.position.z);
        }
    }

    // methode that switch personnage with an other
    private void ChangeImage()
    {
        Character character1 = CharacterUi1.currentCharacter;
        Character character2 = CharacterUi2.currentCharacter;
        CharacterUi1.ResetCharacter();
        CharacterUi2.ResetCharacter();
        CharacterUi1.SetNewCharacter(character2);
        CharacterUi2.SetNewCharacter(character1);
        Debug.Log("change");
    }
}

