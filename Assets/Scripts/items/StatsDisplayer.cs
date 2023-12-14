using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class StatsDisplayer : MonoBehaviour
{
    private Vector3 mousePos;
    [SerializeField] EventSystem eventSystem;
    [SerializeField] GraphicRaycaster m_Raycaster;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        updatePositiion();
    }

    private void updatePositiion()
    {
        mousePos = Input.mousePosition;
        transform.position = mousePos;
    }
}
