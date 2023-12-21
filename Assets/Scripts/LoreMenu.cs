using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoreMenu : MonoBehaviour
{
    [SerializeField] List<GameObject> texts = new();
    private int indice = 1;
    public void NextBtn()
    {
        if (indice >= texts.Count || texts[indice] == null)
        {
            SceneManager.LoadScene("FUCKGAME");
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
