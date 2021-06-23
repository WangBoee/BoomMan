using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int HP = 3; //玩家生命值
    public GameObject bombPre; //普通炸弹预制体
    private SpriteRenderer spriteRenderer;
    private Color color;
    private Rigidbody2D rig;
    private Animator anim;//动画状态机
    private float speed = 0.1f;
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
        Move();
        Bomb();
    }

    private void Bomb()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bomb = GameObject.Instantiate(bombPre, transform);
            bomb.transform.position = new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        }
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
        //若玩家碰到敌人，生命值减一
        if (col.CompareTag(Tag.Enemy))
        {
            HP--;
            StartCoroutine("Injured", 2f);
        }
    }
    //协程
    //闪烁玩家人物
    IEnumerator Injured(float time)
    {
        for (int i = 0; i < time * 2; i++)
        {
            color.a = 0.0f;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(0.25f);
            color.a = 1.0f;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(0.25f);

        }
    }
}
