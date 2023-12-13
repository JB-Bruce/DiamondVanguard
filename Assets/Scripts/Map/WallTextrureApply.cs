using System.Collections.Generic;
using UnityEngine;

public class WallTextrureApply : MonoBehaviour
{
    private List<GameObject> faces = new List<GameObject>();
    private List<Material> facesMaterials = new List<Material>();
    [SerializeField] private Material faceMaterial;
    private float index;
    [SerializeField] private float factor;
    [SerializeField] private bool isMultiplesFaces;

    private void Start()
    {
        Apply();
    }

    public void Apply()
    {
        foreach (Transform tr in gameObject.GetComponentInChildren<Transform>())
        {
            faces.Add(tr.gameObject);
            if (!isMultiplesFaces)
            {
                tr.GetComponent<MeshRenderer>().material = new Material(faceMaterial);
                tr.GetComponent<WallTextureCorrection>().Init(factor);
            }
            else
            {
                return;
            }
        }
    }
}
