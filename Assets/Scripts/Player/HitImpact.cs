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

    public void PlayImpact(int distance, Entity entity)
    {
        transform.position = entity.transform.position;
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
