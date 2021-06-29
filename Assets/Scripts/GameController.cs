using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject playerPre;//主角预制体
    public GameObject enemyPre;
    private PlayerController playerController;
    private MapController mapController;
    private int levelCount = 0; //关卡数
    private int enemyCount = 0; //敌人数量
    private GameObject player; //主角
    public static GameController Instance;
    void Awake()
    {
        Instance = this;
    }
    // Use this for initialization
    void Start()
    {
        LevelController();
    }

    // Update is called once per frame
    void Update()
    {
        //LoadNextLevel();
    }
    //判断是否为实体墙及其他墙体
    //使其他脚本能调用
    public bool IsSuperWall(Vector2 pos)
    {
        return mapController.IsSuperWall(pos);
    }
    public bool IsWall(Vector2 pos)
    {
        return mapController.IsWall(pos);
    }
    public void SetEnemyCounts()
    {
        enemyCount--;
        if (enemyCount < 0)
        {
            enemyCount = 0;
        }
    }

    public int GetEnemyCounts()
    {
        return enemyCount;
    }
    private void LevelController()
    {
        //每三关放大一次地图
        int x = 6 + 2 * (levelCount / 3);
        int y = 3 + 2 * (levelCount / 3);
        //设置地图上限
        if (x > 18)
        {
            x = 18;
        }
        if (y > 15)
        {
            y = 15;
        }
        enemyCount = 1 + (int)(levelCount * 1.5); //敌人数量
                                                  //设置敌人数量上限
        if (enemyCount > 10)
        {
            enemyCount = 40;
        }
        mapController = GetComponent<MapController>();
        mapController.InitMap(x, y, x * y, enemyCount); //初始化地图
                                                        //判断玩家是否第一次生成
        if (null == player)
        {
            player = GameObject.Instantiate(playerPre);
            playerController = player.GetComponent<PlayerController>();
            playerController.Init(3, 1, 2.0f); //初始化玩家
        }
        levelCount++; //关卡递增
        player.transform.position = mapController.GetPlayerPos();
    }
    public bool LoadNextLevel()
    {
        if (enemyCount == 0)
        {
            LevelController();
            return true;
        }
        else
        {
            return false;
        }
    }
}