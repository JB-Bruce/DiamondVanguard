using UnityEngine;

public class MuzzleFlashManager : MonoBehaviour
{
    public bool start = false;
    [SerializeField] private GameObject muzzleFlash;
    private float Timer;


    private void Start()
    {
        muzzleFlash.SetActive(false);
    }

    private void PlayAnim()
    {
        Timer = 0f;
        muzzleFlash.SetActive(true);

    }

    void Update()
    {
        Timer += Time.deltaTime;
        if (start)
        {
            PlayAnim();
            start = false;
        }
        if (Timer > 0.05f)
        {
            muzzleFlash.SetActive(false);
        }
    }
}
