using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private int caseRange;
    GameGrid gameGrid;
    Camera cam;


    private void Start()
    {
        gameGrid = GameGrid.instance;
        cam = Camera.main;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, (caseRange * gameGrid.cellSpacement)))
            {
                if (hit.transform.TryGetComponent<DoorLever>(out DoorLever dl))
                {
                    dl.Interact();
                }
            }
        }
    }
}
