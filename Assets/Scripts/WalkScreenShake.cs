using UnityEngine;

public class WalkScreenShake : MonoBehaviour
{
    public GameObject Camera;


    public void StartBobbing()
    {
        Camera.GetComponent<Animator>().Play("HeadBobbing");
    }

    public void StopBobbing()
    {
        Camera.GetComponent<Animator>().Play("StopBobbing");
    }
}
