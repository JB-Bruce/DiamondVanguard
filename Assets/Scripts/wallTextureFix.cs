using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallTextureFix : MonoBehaviour
{
    [SerializeField] float factor = 1.0f;

    private void Update()
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        if (mf != null)
        {
            var bounds = mf.mesh.bounds;

            Vector3 size = Vector3.Scale(bounds.size, transform.lossyScale) * factor;

            if (size.y < .001)
                size.y = size.z;
            GetComponent<MeshRenderer>().material.mainTextureScale = size;
        }
    }
}
