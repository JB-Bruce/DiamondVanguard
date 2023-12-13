using UnityEngine;

public class WallTextureCorrection : MonoBehaviour
{
    [SerializeField] private float factor;

    private void Start()
    {
        Init(factor);
    }

    public void Init(float factor)
    {
        this.factor = factor;

        MeshFilter mf = GetComponent<MeshFilter>();
        if (mf != null)
        {
            var bounds = mf.sharedMesh.bounds;

            Vector3 size = Vector3.Scale(bounds.size, transform.lossyScale) * factor;

            if (size.y < .001)
                size.y = size.z;
            GetComponent<MeshRenderer>().sharedMaterial.mainTextureScale = size;
        }
    }
}