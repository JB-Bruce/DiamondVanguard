using System.Collections;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private TrailRenderer trailRenderer;
    private Vector3 startPos;
    private Vector3 dir;
    private float speed;
    private float distance;
    private bool isInitialized;

    public void Init(Vector3 startPos, Vector3 dir, int distance, float speed = 50) 
    {
        this.startPos = startPos;
        this.dir = dir;
        this.speed = speed;
        this.distance = distance;
        transform.position = startPos;
        isInitialized = true;
    }


    private void Update()
    {
        if (!isInitialized)
            return;
        print(Vector3.Distance(startPos, transform.position));
        transform.Translate(dir * speed * Time.deltaTime);
        if (Vector3.Distance(startPos, transform.position) > distance)
        {
            Destroy(gameObject);
        }
    }
}
