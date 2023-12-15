using System.Collections;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    [SerializeField] private float duration;
    public AnimationCurve curve;
    public bool start;
    void Update()
    {
        if (start)
        {
            start = false;
            StartCoroutine(Shaking());
        }
    }

    IEnumerator Shaking()
    {
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.localPosition = Random.insideUnitSphere * strength;
            yield return null;
        }
        transform.localPosition = Vector3.zero;
    }

}
