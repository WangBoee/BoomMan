using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public Sprite wallSprite;   //获取墙体图片
    public int X, Y;//X行 Y列
    public GameObject doorPre;//门的预制体
    private GameObject door;
    private List<Vector2> nullPointsList = new List<Vector2>();
    private List<Vector2> superWallList = new List<Vector2>();
    private List<Vector2> wallList = new List<Vector2>();

    //Awake和start函数一样，但前者比后者先调用


    // Update is called once per frame
    void Update()
    {

    }
    //初始化地图

    public void InitMap(int x, int y, int wallCount, int enemyCount)
    {
        X = x;
        Y = y;
        foreach (Transform item in transform)
        {
            Destroy(item.gameObject);
        }
        Debug.Log("init map");
        //每次生成地图清空之前地图数据
        nullPointsList.Clear();
        wallList.Clear();
        superWallList.Clear();
        //生成地图
        CreateSuperWall();
        FindNullPoint();
        CreateWall1(wallCount);
        CreateDoor();
        CreateProp();
        CreateEnemy(enemyCount);
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
        superWallList.Add(pos);
        ObjPool.Instace.GetObj(ObjectType.SuperWall, pos);
    }

    //查找地图中所有的空点
    private void FindNullPoint()
    {
        for (int x = -(X + 1); x <= X - 1; x++)
        {
            if (-(X + 1) % 2 == x % 2)//奇数行
            {
                for (int y = -(Y + 1); y <= Y - 1; y++)
                {
                    nullPointsList.Add(new Vector2(x, y));
                    //Debug.Log(x+" "+y);
                }
            }
            else
            {
                for (int y = -(Y + 1); y <= Y - 1; y += 2)
                {
                    nullPointsList.Add(new Vector2(x, y));
                    //Debug.Log(x + " " + y);
                }
            }
        }
        //剔除左上角的点
        nullPointsList.Remove(new Vector2(-(X + 1), Y - 1));
        nullPointsList.Remove(new Vector2(-X, Y - 1));
        nullPointsList.Remove(new Vector2(-(X + 1), Y - 2));
        nullPointsList.Remove(new Vector2(-(X + 1), -Y - 1));
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
            ObjPool.Instace.GetObj(ObjectType.Wall, nullPointsList[index]);
            wallList.Add(nullPointsList[index]);
            nullPointsList.RemoveAt(index);//把当前生成可销毁的墙的空结点移给
        }
    }
    //创建门
    private void CreateDoor()
    {
        if (null == door)
        {
            door = GameObject.Instantiate(doorPre);
        }
        door.GetComponent<SpriteRenderer>().sprite = wallSprite;
        int index = Random.Range(0, nullPointsList.Count);//随机出门的位置
        door.transform.position = nullPointsList[index];
        wallList.Add((Vector2)door.transform.position);
        nullPointsList.RemoveAt(index);
    }

    //生成道具
    private void CreateProp()
    {
        int count = Random.Range(0, 2 + (int)(nullPointsList.Count * 0.05f));
        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, nullPointsList.Count);//随机出道具位置
            ObjPool.Instace.GetObj(ObjectType.Prop, nullPointsList[index]);
            nullPointsList.RemoveAt(index);//把当前生成可销毁的墙的空结点移给
        }
    }

    //获取主角坐标
    public Vector2 GetPlayerPos()
    {
        return new Vector2(-(X + 1), Y - 1);
    }

    public Vector2 GetEnemyPos()
    {
        return new Vector2(-(X + 1), -Y - 1);
    }
    //生成敌人
    private void CreateEnemy(int count)
    {
        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, nullPointsList.Count);
            ObjPool.Instace.GetObj(ObjectType.Enemy, nullPointsList[index]);
            nullPointsList.RemoveAt(index);
        }
    }
    public bool IsSuperWall(Vector2 pos)
    {
        return superWallList.Contains(pos);
    }
    public bool IsWall(Vector2 pos)
    {
        return wallList.Contains(pos);
    }
}
