using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    public static UIController Instance;
    public Text txtHP;
    public Text txtLevel;
    public Text txtTime;
    public Text txtEnemy;

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
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
