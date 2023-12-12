using UnityEngine;

public class WallTextureCorrection : MonoBehaviour
{
    public float factor;

    private void Start()
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