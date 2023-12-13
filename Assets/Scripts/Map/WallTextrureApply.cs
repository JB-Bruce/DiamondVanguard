using System.Collections.Generic;
using UnityEngine;

public class WallTextrureApply : MonoBehaviour
{
    private List<GameObject> faces = new List<GameObject>();
    [SerializeField] private Material faceMaterial;
    [SerializeField] private float factor;

    private void Start()
    {
        Apply();
    }

    public void Apply()
    {
        foreach (Transform tr in gameObject.GetComponentInChildren<Transform>())
        {
            faces.Add(tr.gameObject);
            tr.GetComponent<MeshRenderer>().material = faceMaterial;
            tr.GetComponent<WallTextureCorrection>().Init(factor);
        }
    }
}
