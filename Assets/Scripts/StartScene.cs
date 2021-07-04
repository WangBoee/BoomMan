using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{

    public GameObject MainMenuPanel;
    public GameObject HelpPanel;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    public void Help()
    {
        MainMenuPanel.SetActive(false);
        HelpPanel.SetActive(true);
    }
    public void Back()
    {
        MainMenuPanel.SetActive(true);
        HelpPanel.SetActive(false);
    }
}
