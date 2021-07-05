using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{

    public GameObject MainMenuPanel;
    public GameObject HelpPanel;
    public AudioSource audioCtrl;
    public Image musicImg;
    public Sprite muteSprite;
    public Sprite playSprite;
    public Text tips;
    private bool isMute;
    private bool isLoad = false;
    public static StartScene Instance;
    void Awake()
    {
        Instance = this;
    }
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
    public void MusicCtrl()
    {
        isMute = audioCtrl.GetComponent<AudioSource>().mute;
        Debug.LogError(isMute);
        if (isMute)
        {
            audioCtrl.GetComponent<AudioSource>().mute = false;
            musicImg.sprite = playSprite;
        }
        else
        {
            audioCtrl.GetComponent<AudioSource>().mute = true;
            musicImg.sprite = muteSprite;
        }
    }
    public void SetLoadStatus(bool status = true)
    {
        if (PlayerPrefs.GetInt("level") == 0)
        {
            StartCoroutine(Tips());
            return;
        }
        isLoad = status;
        SceneManager.LoadScene(1);
    }
    public bool GetLoadStatus()
    {
        return isLoad;
    }
    IEnumerator Tips()
    {
        tips.text = "无存档";
        yield return new WaitForSeconds(2.0f);
        tips.text = "";
    }
}
