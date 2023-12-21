using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorCD : MonoBehaviour
{
    [SerializeField] float deleteTime;

    private void Start()
    {
        Invoke("Delete", deleteTime);
    }

    private void Delete()
    {
        Destroy(gameObject);
    }
}
