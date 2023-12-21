using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLoreMenu : MonoBehaviour
{
    [SerializeField] List<GameObject> texts = new();
    private int indice = 1;

    public void EndBtn()
    {
        if (indice >= texts.Count || texts[indice] == null)
        {
            SceneManager.LoadScene("Test_Menu");
        }
        else
        {
            texts[indice].SetActive(true);
            if (indice - 1 >= 0)
            {
                texts[indice - 1].SetActive(false);
            }
        }
        indice++;
    }
}
