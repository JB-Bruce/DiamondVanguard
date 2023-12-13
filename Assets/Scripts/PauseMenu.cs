using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
    public void BackToMenu()
    {
        SceneManager.LoadScene("Test_Menu");
    }

    public void Continue()
    {
        Time.timeScale = 1f;
        player = PlayerMovement.instance;
        player.pauseCanvas.SetActive(false);
    }
}
