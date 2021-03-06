using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControllerMy : MonoBehaviour
{

    public int X, Y;//X行 Y列
    public GameObject superWallPre;//实体墙预制体
    public GameObject wallPre;//可销毁的墙预制体
    public GameObject doorPre;//门的预制体
    public GameObject propPre;//道具预制体
    public GameObject playerPre;//
    private Vector2 playerPos;
    private List<Vector2> nullPointsList = new List<Vector2>();
    //private List<Vector2> wallPointsList = new List<Vector2>();

    // Use this for initialization


    void Awake()
    {
        InitMap(5, 3, 30);
    }
    //Awake和start函数一样，但前者比后者先调用

    // Update is called once per frame
    void Update()
    {

    }
    //初始化地图

    public void InitMap(int x, int y, int wallCount)
    {
        X = x;
        Y = y;
        CreateSuperWall();
        FindNullPoint();
        CreateWall1(wallCount);
        CreateDoor();
        CreateProp();
        //Debug.Log(nullPointsList.Count);
    }

    //生成实体墙
    private void CreateSuperWall()
    {
        for (int i = -X; i < X; i += 2)
        {
            for (int j = -Y; j < Y; j += 2)
            {
                //实例化墙
                SpwanSuperWall(new Vector2(i, j));
            }
        }
        //生成最上下侧的实体墙
        for (int i = -(X + 2); i <= X; i++)
        {
            //上
            SpwanSuperWall(new Vector2(i, Y));
            //下
            SpwanSuperWall(new Vector2(i, -Y - 2));

        }

        //生成最左右侧的实体墙
        for (int i = -(Y + 1); i <= Y - 1; i++)
        {
            //左
            SpwanSuperWall(new Vector2(-(X + 2), i));
            //右
            SpwanSuperWall(new Vector2(X, i));

        }
    }

    //生成实体墙
    private void SpwanSuperWall(Vector2 pos)
    {
        GameObject superWall = GameObject.Instantiate(superWallPre, transform);
        superWall.transform.position = pos;
    }

    //查找地图中所有的空点
    private void FindNullPoint()
    {
        //-------------------------------------
        for (int i = -(X + 1); i < X; i += 2)
        {
            for (int j = -(Y + 1); j < Y; j++)
            {
                Debug.Log(i + " " + j);
                //SpwanWall(new Vector2(i, j));
                nullPointsList.Add(new Vector2(i, j));
            }
        }
        for (int i = -X; i < X; i += 2)
        {
            for (int j = -(Y + 1); j < Y; j += 2)
            {
                Debug.Log(i + " " + j);
                //SpwanWall(new Vector2(i, j));
                nullPointsList.Add(new Vector2(i, j));
            }
        }
        Vector2 playerPos = GetPlayerPos();

        nullPointsList.Remove(new Vector2(playerPos.x - 1, playerPos.y));
        nullPointsList.Remove(new Vector2(playerPos.x + 1, playerPos.y));
        nullPointsList.Remove(new Vector2(playerPos.x, playerPos.y - 1));
        nullPointsList.Remove(new Vector2(playerPos.x, playerPos.y + 1));
        //-------------------------------------
        //for (int x = -(X + 1); x <= X - 1; x++)
        //{
        //    if (-(X + 1) % 2 == x % 2)//奇数行
        //    {
        //        for (int y = -(Y + 1); y <= Y - 1; y++)
        //        {
        //            nullPointsList.Add(new Vector2(x, y));
        //            //Debug.Log(x+" "+y);
        //        }
        //    }
        //    else
        //    {
        //        for (int y = -(Y + 1); y <= Y - 1; y += 2)
        //        {
        //            nullPointsList.Add(new Vector2(x, y));
        //            //Debug.Log(x + " " + y);
        //        }
        //    }
        //}
    }
    //创建可销毁的墙
    private void CreateWall1(int wallCount)
    {
        if (wallCount >= nullPointsList.Count)//作用：避免数组越界
        {
            wallCount = (int)(nullPointsList.Count * 0.7f);
        }
        for (int i = 0; i < wallCount; i++)
        {
            int index = Random.Range(0, nullPointsList.Count);
            GameObject wall = GameObject.Instantiate(wallPre, transform);
            wall.transform.position = nullPointsList[index];
            nullPointsList.RemoveAt(index);//把当前生成可销毁的墙的空结点移给
        }
    }
    //创建门
    private void CreateDoor()
    {
        int index = Random.Range(0, nullPointsList.Count);//随机出门的位置
        GameObject door = GameObject.Instantiate(doorPre, transform);
        door.transform.position = nullPointsList[index];
        nullPointsList.RemoveAt(index);
    }

    //生成道具
    private void CreateProp()
    {
        int count = Random.Range(0, 2 + (int)(nullPointsList.Count * 0.05f));
        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, nullPointsList.Count);//随机出道具位置
            GameObject prop = GameObject.Instantiate(propPre, transform);
            prop.transform.position = nullPointsList[index];
            nullPointsList.RemoveAt(index);//把当前生成可销毁的墙的空结点移给
        }
    }

    public Vector2 GetPlayerPos()
    {
        int index = Random.Range(0, nullPointsList.Count);//随机出玩家位置
        playerPos = nullPointsList[index];
        nullPointsList.RemoveAt(index);//把当前生成可销毁的墙的空结点移给
        return playerPos;
    }

    //生成主角
    //private void CreatePlayer()
    //{
    //    int n = nullPointsList.Count;
    //    while (n>0)
    //    {
    //        int index = Random.Range(0, nullPointsList.Count);//随机出道具位置
    //        float px = nullPointsList[index].x;
    //        float py = nullPointsList[index].y;
    //        Vector2 dest = new Vector2(px - 1, py);
    //        Vector2 dest1 = new Vector2(px - 2, py);
    //        if (nullPointsList.Contains(dest) && nullPointsList.Contains(dest1))
    //        {
                
    //            break;
    //        }
    //        n--;
    //    }
    //}
}
