using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Outline outline;

    public void Open()
    {
        animator.Play("Open");
    }

    public void Select()
    {
        outline.enabled = true;
    }

    public void UnSelect()
    {
        outline.enabled = false;
    }
}
