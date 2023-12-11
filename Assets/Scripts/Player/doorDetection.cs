using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class doorDetection : MonoBehaviour
{
    [SerializeField] Camera playerCam;
    [SerializeField] float maxInteractionRange;
    [SerializeField] string doorTag;

    private void Update()
    {
        //object detection
        Vector3 mousePos = playerCam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(mousePos, playerCam.transform.forward, out RaycastHit hit, maxInteractionRange))
            {
                if (hit.collider.gameObject.tag == doorTag)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
        }
    }

}
