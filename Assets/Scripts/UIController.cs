using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIController : MonoBehaviour
{
    public static UIController Instance;
    public Text txtHP;
    public Text txtLevel;
    public Text txtTime;
    public Text txtEnemy;
    public GameObject gameOverPanel; //游戏结束界面
    //public Button restart; //重新开始按钮
    //public Button menu; //主菜单按钮

    //刷新UI
    public void ReFresh(int hp, int level, int time, int enemy)
    {
        txtHP.text = "HP:" + hp.ToString();
        txtLevel.text = "Level:" + level.ToString();
        txtTime.text = "Time:" + time.ToString();
        txtEnemy.text = "Enemy:" + enemy.ToString();
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
    //按钮事件绑定
    public void ReStart()
    {
        Time.timeScale = 1; //恢复游戏时间
        SceneManager.LoadScene(0);
    }
    //回到主界面
    public void ReturnMenu()
    {
        SceneManager.LoadScene(1);
    }
}
