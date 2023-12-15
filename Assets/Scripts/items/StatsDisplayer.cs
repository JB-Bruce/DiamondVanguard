using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StatsDisplayer : MonoBehaviour
{
    private Vector3 mousePos;
    [SerializeField] EventSystem eventSystem;
    [SerializeField] GraphicRaycaster m_Raycaster;
    [SerializeField] TextMeshProUGUI Header;
    [SerializeField] TextMeshProUGUI Content;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        updatePosition();
    }

    private void updatePosition()
    {
        mousePos = Input.mousePosition;
        transform.position = mousePos;
    }

    public void UpdateTexts(Item item)
    {
        Header.text = item.name;
    }
}
