using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject playerPre;//主角预制体
    public GameObject enemyPre;
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
        mapController.InitMap(8, 3, 20, 8);
        GameObject player = GameObject.Instantiate(playerPre);
        player.transform.position = mapController.GetPlayerPos();
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