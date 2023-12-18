using UnityEngine;

public class HitImpact : MonoBehaviour
{
    public DistanceWeapon weapon;
    private float Timer;
    [SerializeField] private GameObject impact;

    void Start()
    {
        impact.SetActive(false);
    }

    public void PlayImpact(int distance)
    {
        transform.position = transform.parent.position + new Vector3(0, 0, (distance * 2));
        impact.SetActive(true);
        Timer = 0;
    }

    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer > 1f)
        {
            impact.SetActive(false);
        }
    }
}
