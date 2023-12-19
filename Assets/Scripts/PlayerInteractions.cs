using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private int caseRange;
    GameGrid gameGrid;
    [SerializeField] Camera cam;

    DoorLever selectedLever = null;


    private void Start()
    {
        gameGrid = GameGrid.instance;
    }

    private void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, (caseRange * gameGrid.cellSpacement)))
        {
            if (hit.transform.TryGetComponent<DoorLever>(out DoorLever dl))
            {
                if (selectedLever != dl)
                {
                    if (selectedLever != null)
                        selectedLever.UnSelect();

                    selectedLever = dl;
                    selectedLever.Select();
                }

                if (Input.GetMouseButtonDown(0))
                { 
                    
                
                    dl.Interact();
                }
                return;
            }
        }

        if (selectedLever != null)
        {
            selectedLever.UnSelect();
            selectedLever = null;
        }
    }
}
