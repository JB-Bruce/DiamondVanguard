using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkScreenShake : MonoBehaviour
{
    public GameObject Camera;

    public bool start;

    private void Update()
    {
        if (start)
            StartBobbing();
        else
            StopBobbing();
    }

    private void StartBobbing()
    {
        Camera.GetComponent<Animator>().Play("HeadBobbing");
    }

    private void StopBobbing()
    {
        Camera.GetComponent<Animator>().Play("StopBobbing");
    }
}
