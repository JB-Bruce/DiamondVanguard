using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

[ExecuteInEditMode]
public class WallManager : MonoBehaviour
{

    public bool applyTextures;
    void OnValidate()
    {
        if (!applyTextures)
            return;

        applyTextures = false;

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<WallTextrureApply>().Apply();
        }
    }

}
