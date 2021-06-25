﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Sprite doorSprite;   //用与获取门的图片
    private SpriteRenderer spriteRenderer;
    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //触发检测
    private void OnTriggerEnter2D(Collider2D col)
    {
        //判断是否与爆炸特效发生碰撞
        if (col.CompareTag(Tag.BombEffect))
        {
            spriteRenderer.sprite = doorSprite;
            this.GetComponent<Collider2D>().isTrigger = true;
        }
        //判断门是否与玩家相碰
        //判断敌人是否都被消灭
        //全满足才可跳转下一关
        if (col.CompareTag(Tag.Player))
        {
            //TODO
        }
    }
}
