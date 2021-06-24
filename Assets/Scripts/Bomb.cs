﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private int bombRange; //爆炸范围
    public GameObject bombEffect; //爆炸特效

    public void Init(int bRange, float time)
    {
        this.bombRange = bRange;
        StartCoroutine("DelayBoom", time);
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    //延时爆炸
    IEnumerator DelayBoom(float time)
    {
        //暂停time秒
        yield return new WaitForSeconds(time);
        //延时结束，删除炸弹物体，生成爆炸特效
        GameObject.Instantiate(bombEffect, transform.position, Quaternion.identity);
        Boom(Vector2.left); //向左延伸爆炸效果
        Boom(Vector2.right); //向右
        Boom(Vector2.up); //向上
        Boom(Vector2.down); //向下
        Destroy(gameObject);	//销毁炸弹
    }
    //生成爆炸特效，爆炸点周围四个方向
    private void Boom(Vector2 dir)
    {
        for (int i = 1; i <= bombRange; i++)
        {
            //当前位置加上某个方向偏移量
            Vector2 pos = (Vector2)transform.position + dir * i;
            //实例化炸弹特效并设置位置
            GameObject bombEff = GameObject.Instantiate(bombEffect);
            bombEff.transform.position = pos;
        }
    }
}
