using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class doorDetection : MonoBehaviour
{
    [SerializeField] Camera playerCam;
    [SerializeField] float maxInteractionRange;
    [SerializeField] string doorTag;

    [SerializeField] MapLoader mapLoader;

    private Door selectedDoor = null;


    private void Update()
    {
        //object detection
        Vector3 mousePos = playerCam.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(playerCam.ScreenPointToRay(Input.mousePosition), out hit, maxInteractionRange))
        {
            if (hit.transform.gameObject.TryGetComponent<Door>(out Door door))
            {
                if (selectedDoor != door)
                {

                    if (selectedDoor != null)
                        selectedDoor.UnSelect();

                    selectedDoor = door;
                    selectedDoor.Select();
                }
                if (Input.GetMouseButtonDown(0))
                {
                    if (hit.collider.gameObject.tag == doorTag)
                    {
                          mapLoader.ChangeMap();
                    }
                }
                return;
            }
        }
        if (selectedDoor != null)
        {
            selectedDoor.UnSelect();
            selectedDoor = null;
        }
    }
}
