using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIController : MonoBehaviour
{
    public static UIController Instance;
    public Animator levelFade;
    public Button txtHP;
    public Text txtLevel;
    public Button txtTime;
    public Button txtEnemy;
    public Text txtLevelTitle;
    public GameObject topBar; //游戏暂停界面
    public GameObject gameOverPanel; //游戏结束界面
    public GameObject pausePanel; //游戏暂停界面
    public AudioSource audioCtrl;
    public Button musicButton;
    public Sprite muteSprite;
    public Sprite playSprite;
    public Image buttonIcon;
    private bool isMute;
    //public Button restart; //重新开始按钮
    //public Button menu; //主菜单按钮

    //刷新UI
    public void ReFresh(int hp, int level, int time, int enemy)
    {
        txtHP.GetComponentInChildren<Text>().text = "    " + hp.ToString();
        txtTime.GetComponentInChildren<Text>().text = "    " + time.ToString();
        txtEnemy.GetComponentInChildren<Text>().text = "    " + enemy.ToString();
        txtLevel.text = "Lv." + level.ToString();
    }

    void Awake()
    {
        Instance = this;
        //restart.onClick.AddListener(() => { });
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    public void ShowPausePanel()
    {
        pausePanel.SetActive(true);
    }
    public void PlayLevelFadeAnim(int levelIndex)
    {
        txtLevelTitle.text = "Level:" + levelIndex.ToString();
        levelFade.Play("LevelFade", 0, 0);
    }

    //按钮事件绑定
    public void Resume()
    {
        Time.timeScale = 1; //恢复游戏时间
        topBar.SetActive(true);
        pausePanel.SetActive(false);
        GameController.Instance.PlayMusic();
    }
    public void ReStart()
    {
        Time.timeScale = 1; //恢复游戏时间
        SceneManager.LoadScene(1);
    }
    //回到主界面
    public void ReturnMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void MusicCtrl()
    {
        isMute = audioCtrl.GetComponent<AudioSource>().mute;
        if (isMute)
        {
            audioCtrl.GetComponent<AudioSource>().mute = false;
            musicButton.GetComponentInChildren<Text>().text = "关闭音乐";
            buttonIcon.GetComponent<Image>().sprite = playSprite;
        }
        else
        {
            audioCtrl.GetComponent<AudioSource>().mute = true;
            musicButton.GetComponentInChildren<Text>().text = "开启音乐";
            buttonIcon.GetComponent<Image>().sprite = muteSprite;
        }
    }
}
