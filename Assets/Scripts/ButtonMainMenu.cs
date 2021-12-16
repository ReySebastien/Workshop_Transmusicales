using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMainMenu : MonoBehaviour
{
    public void LoadHub()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Hub");
    }

    public void LoadControlScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("ControlScene");
    }
    public void CloseGame()
    {
        Time.timeScale = 1;
        Application.Quit();
    }
}
