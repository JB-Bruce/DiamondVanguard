using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AttackSlash : MonoBehaviour
{
    public GameObject Slash;
    public bool start;
    public float Timer;
    public TrailRenderer TrailRenderer;

    private void Start()
    {
        TrailRenderer.enabled = false;
    }

    public void StartSlash()
    {
        Slash.GetComponent<Animator>().Play("SlashAttack");
    }


    public void StopSlash()
    {
        Slash.GetComponent<Animator>().Play("StopSlash");
    }

    private void Update()
    {
        Timer += Time.deltaTime;
        if (start)
        {
            TrailRenderer.enabled = true;
            Timer = 0f;
            StartSlash();
            start = false;
        }
        if (Timer > 0.35f)
        {
            StopSlash();
            TrailRenderer.enabled = false;
        }
    }

}
