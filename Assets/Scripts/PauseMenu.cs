using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
    public void BackToMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Test_Menu");
    }

    public void Continue()
    {
        Time.timeScale = 1f;
        player = PlayerMovement.instance;
        player.ActivateBehaviours();
        player.pauseCanvas.SetActive(false);
    }
}
