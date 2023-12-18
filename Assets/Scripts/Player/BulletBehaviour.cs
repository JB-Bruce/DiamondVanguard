using System.Collections;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private float Timer;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private Transform startPos;
    [SerializeField] private float travelTime;

    public static BulletBehaviour instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("2 bullet behaviour");
            Destroy(this);
            return;
        }
    }

    private void Start()
    {

    }

    private void Update()
    {
        Timer += Time.deltaTime;
        if(Timer > 0.5f)
        {
            ReturnBullet();
        }
    }

    private void ReturnBullet()
    {
        transform.position = startPos.position;
        trailRenderer.enabled = false;
    }

    IEnumerator BulletMove(int distance, Vector3 dir)
    {
        trailRenderer.enabled = true;
        float timer = 0;
        while(timer < travelTime)
        {
            transform.position = Vector3.Lerp(startPos.position, startPos.position + dir * distance * 2  , 0.5f);
            timer += Time.deltaTime;
            yield return null;
        }
        trailRenderer.enabled = false;
    }

    public void BulletAdvence(int distance, Vector3 dir)
    {
        StartCoroutine(BulletMove(distance, dir));
    }
}
