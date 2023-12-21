using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Outline outline;
    [SerializeField] public string targetScene;
    [SerializeField] bool endDoor;

    private void Start()
    {
        if(endDoor) UnSelect();
    }

    public void Open()
    {
        animator.Play("Open");
    }

    public void Select()
    {
        if(endDoor) outline.enabled = true;
    }

    public void UnSelect()
    {
        if (endDoor) outline.enabled = false;
    }
}
