using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WallManager : MonoBehaviour
{
    [SerializeField] List<WallTextrureApply> wallTextrures;

    public bool applyTextures;
    void OnValidate()
    {
        if (!applyTextures)
            return;

        applyTextures = false;
        foreach (var item in wallTextrures)
        {
            item.Apply();
        }
    }

}
