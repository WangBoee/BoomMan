using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject playerPre;//主角预制体
    public GameObject enemyPre;
    private PlayerController playerController;
    private MapController mapController;
    public static GameController Instance;
    void Awake()
    {
        Instance = this;
    }
    // Use this for initialization
    void Start()
    {
        mapController = this.GetComponent<MapController>();
        playerController = this.GetComponent<PlayerController>();
        mapController.InitMap(8, 3, 20, 8);
        GameObject player = GameObject.Instantiate(playerPre);
        player.transform.position = mapController.GetPlayerPos();
        playerController.Init(3, 1, 2); //初始化玩家
        //GameObject enemy = GameObject.Instantiate(enemyPre);
        //enemy.transform.position = mapController.GetEnemyPos();
    }

    // Update is called once per frame
    void Update()
    {

    }
    //使其他脚本能调用
    public bool IsSuperWall(Vector2 pos)
    {
        return mapController.IsSuperWall(pos);
    }
}