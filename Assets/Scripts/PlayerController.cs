using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int HP = 0; //玩家生命值
    public GameObject bombPre; //普通炸弹预制体
    public int boomRange; //炸弹爆炸范围
    public int bombCount = 1;
    private Animator anim;//动画状态机
    public float speed = 0.06f; //玩家移动速度
    private bool isInjured = false; //标识玩家是否收到伤害
    private float boomTime = 0.0f; //炸弹爆炸时间
    private SpriteRenderer spriteRenderer;
    private Color color;
    private Rigidbody2D rig;
    private bool isDied = false;
    private List<GameObject> bombList = new List<GameObject>();

    // Use this for initialization
    void Awake()
    {
        anim = this.GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        color = spriteRenderer.color;
    }

    // Update is called once per frame
    //GetAxis线性变换（0-1渐变），GetAxisRaw是跳跃（0-1切换）。
    void Update()
    {
        if (!isDied)
        {
            Move();
            Bomb();
        }
    }
    //玩家初始化
    public void Init(int hp, int bRange, int bCount, float bTime)
    {
        this.HP = hp;
        boomRange = bRange;
        boomTime = bTime;
        bombCount = bCount;
    }
    public void SetBombCount(int count)
    {
        if (bombCount > 8)
        {
            bombCount = 8;
        }
        else
        {
            bombCount = count;
        }
    }
    private void Bomb()
    {
        if (Input.GetKeyDown(KeyCode.Space) && bombCount != 0)
        {
            AudioController.Instance.PlayFire(); //播放音效
            Vector2 pos = new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
            GameObject bomb = ObjPool.Instance.GetObj(ObjectType.Bomb, pos);
            bomb.GetComponent<CircleCollider2D>().isTrigger = true;
            bombCount--;
            if (bomb != null)
            {
                //GameObject bomb = GameObject.Instantiate(bombPre);
                //一般调用方法
                //bomb.GetComponent<Bomb>().Init(boomRange, boomTime, OnFinAction); //调用Bomb类中Init初始化炸弹特效
                //匿名函数，lambda 表达式
                bomb.GetComponent<Bomb>().Init(boomRange, boomTime, () =>
                {
                    bombCount++;
                    bombList.Remove(bomb);  //将炸弹从列表中移除
                });
            }
            else
            {
                Debug.LogError("bomb is null");
            }
            bombList.Add(bomb);
        }
    }
    //一般调用方法
    //void OnFinAction()
    //{
    //    bombCount++;
    //}

    public void AddSpeed(float val = 0.005f)
    {
        if (val > 0.12f)
        {
            speed = 0.12f;
        }
        speed += val;
    }
    private void Move()
    {
        float h = Input.GetAxis("Horizontal");//水平方向
        float v = Input.GetAxis("Vertical");//垂直方向
        anim.SetFloat("Horizontal", h);
        anim.SetFloat("Vertical", v);
        rig.MovePosition(transform.position + new Vector3(h, v) * speed);
    }
    //触发检测
    private void OnTriggerEnter2D(Collider2D col)
    {
        //若玩家正在收到伤害直接返回，此时玩家处于无敌状态
        if (isInjured)
        {
            return;
        }
        //若玩家碰到敌人，生命值减一
        if (col.CompareTag(Tag.Enemy) || col.CompareTag(Tag.BombEffect))
        {
            //游戏结束判定
            if (HP <= 1)
            {
                PlayerDieAnim();
                return;
            }
            HP--;
            StartCoroutine("Injured", 2f);
        }
    }
    //协程
    //闪烁玩家人物
    IEnumerator Injured(float time)
    {
        isInjured = true;
        for (int i = 0; i < time * 2; i++)
        {
            color.a = 0.0f;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(0.25f);
            color.a = 1.0f;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(0.25f);
        }
        isInjured = false;
    }
    //播放死亡动画
    public void PlayerDieAnim()
    {
        isDied = true;
        anim.SetTrigger("Die");
    }
    //死亡动画播放完成，再调用游戏结束函数
    public void DieAnimFinish()
    {
        GameController.Instance.GameOver();
    }
    //重置炸弹
    public void ReSet()
    {
        foreach (var item in bombList)
        {
            bombCount++;
            item.GetComponent<Bomb>().StopAllCoroutines();
            ObjPool.Instance.AddObj(ObjectType.Bomb, item);
        }
        bombList.Clear();
        StopCoroutine("Injured");
        color.a = 1;
        spriteRenderer.color = color;
        isInjured = false;
    }
}
