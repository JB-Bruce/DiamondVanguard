using System.Collections.Generic;
using UnityEngine;

public class WallTextrureApply : MonoBehaviour
{
    private List<GameObject> faces = new List<GameObject>();
    [SerializeField] private Material faceMaterial;
    [SerializeField] private float factor;

    void Start()
    {
        GetAllFaces();
    }

    void GetAllFaces()
    {
        foreach (Transform tr in gameObject.GetComponentInChildren<Transform>())
        {
            faces.Add(tr.gameObject);
            tr.GetComponent<WallTextureCorrection>().factor = factor;
        }
        ApplyFaces();
    }

    void ApplyFaces()
    {
        for (int i  = 0; i < faces.Count; i++)
        {
            faces[i].GetComponent<MeshRenderer>().material = faceMaterial;
        }
    }
}
