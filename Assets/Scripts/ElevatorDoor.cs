using UnityEngine;
using UnityEngine.SceneManagement;

public class ElevatorDoor : MonoBehaviour
{
    [SerializeField] GameGrid gameGrid;
    [SerializeField] private Camera cam;
    [SerializeField] private int doorRange;
    [SerializeField] private LayerMask itemMask;
    [SerializeField] private string targetScene;
    Door selectedDoor = null;
    private void Start()
    {
        gameGrid = GameGrid.instance;
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, (doorRange * gameGrid.cellSpacement), itemMask))
        {
            if (hit.transform.gameObject.layer == 8)
            {
                Door door = hit.transform.gameObject.GetComponent<Door>();

                if (selectedDoor != door)
                {
                    if (selectedDoor != null)
                        selectedDoor.UnSelect();

                    selectedDoor = door;
                    selectedDoor.Select();
                }
                if (Input.GetMouseButtonDown(0))
                {
                    SceneManager.LoadScene(targetScene);
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
