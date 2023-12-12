using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartBtn()
    {
        SceneManager.LoadScene("Test_Lore");
    }

    public void CreditsBtn()
    {
        SceneManager.LoadScene("Test_Credits");
    }

    public void QuitBtn()
    {
        Application.Quit();
    }
}
