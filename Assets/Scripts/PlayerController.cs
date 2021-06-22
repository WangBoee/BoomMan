using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator anim;//动画状态机
    public GameObject bombPre;
    private float speed = 0.1f;
    // Use this for initialization
    void Awake()
    {
        anim = this.GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
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
}
