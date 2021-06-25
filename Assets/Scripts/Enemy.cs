using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed = 0.0375f;
    private int dirID = 0; //方向, 0:上 1:下 2:左 3:右
    private Vector2 dirVec; //生成的方向
    private float rayDistance = 0.7f; //射线检测距离
    private Rigidbody2D rig;
    private Color pcolor; //图片原颜色
    private SpriteRenderer sr = null; //获取精灵渲染器
    private bool locked = false; //敌人是否被困住，默认假设未被困住

    void Awake()
    {
        dirID = Random.Range(0, 4);
        InitDir(dirID);
        rig = this.GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        pcolor = sr.color;
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("是否困住: " + locked);
        //先判断是否被困，困住则不动
        if (!locked)
        {
            rig.MovePosition((Vector2)transform.position + (speed * dirVec));
        }
        else
        {
            ChangeDir();
        }
    }
    //初始化方向
    private void InitDir(int dir)
    {
        dirID = dir;
        switch (dirID)
        {
            case 0: //上
                dirVec = Vector2.up;
                break;
            case 1: //下
                dirVec = Vector2.down;
                break;
            case 2: //左
                dirVec = Vector2.left;
                break;
            case 3: //右
                dirVec = Vector2.right;
                break;
            default:
                break;
        }
    }
    //碰撞条件
    private void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.LogError("碰撞"); //debug
        if (col.CompareTag(Tag.SuperWall) || col.CompareTag(Tag.Wall))
        {
            transform.position = new Vector2(Mathf.RoundToInt(transform.position.x),
                Mathf.RoundToInt(transform.position.y));
            ChangeDir();
        }
        //多个Enemy重叠时，设置半透明状态
        if (col.CompareTag(Tag.Enemy))
        {
            pcolor.a = 0.5f;
            sr.color = pcolor;
        }
        if (col.CompareTag(Tag.BombEffect))
        {
            GameController.Instance.SetEnemyCounts();
            Destroy(this.gameObject);
        }
    }
    //退出碰撞
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag(Tag.Enemy))
        {
            //碰撞结束时重置敌人颜色
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            pcolor.a = 1.0f;
            sr.color = pcolor;
        }
    }
    //private void OnTriggerStay2D(Collider2D col)
    //{
    //    Debug.Log("OnTriggerStay2D Run");
    //    Debug.Log("GameObject1 collided with " + col.name);
    //    this.ChangeDir();
    //}
    //------------------------
    //射线检测
    //改变方向
    private void ChangeDir()
    {
        //Debug.LogError("改变方向"); //debug
        List<int> dirList = new List<int>();    //存储可移动方向
        if (!Physics2D.Raycast(transform.position, Vector2.up, rayDistance, 1 << 8))
        {
            dirList.Add(0);	//上方未检测到物体，将此方向添加到列表
        }
        if (!Physics2D.Raycast(transform.position, Vector2.down, rayDistance, 1 << 8))
        {
            dirList.Add(1);
        }
        if (!Physics2D.Raycast(transform.position, Vector2.left, rayDistance, 1 << 8))
        {
            dirList.Add(2);
        }
        if (!Physics2D.Raycast(transform.position, Vector2.right, rayDistance, 1 << 8))
        {
            dirList.Add(3);
        }
        //Debug.Log("可移动方向数量: " + dirList.Count);
        if (dirList.Count > 0)
        {
            locked = false;
            int index = Random.Range(0, dirList.Count);
            InitDir(dirList[index]);
            //Debug.Log("移动方向: " + dirList[index]);
        }
        else
        {
            locked = true;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, rayDistance, 0));

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -rayDistance, 0));

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(-rayDistance, 0, 0));

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(rayDistance, 0, 0));
    }
}
