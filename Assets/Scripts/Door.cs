using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator anim;
    public RuntimeAnimatorController door;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
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
            gameObject.tag = Tag.Untagged;
            anim.runtimeAnimatorController = door;
            this.GetComponent<Collider2D>().isTrigger = true;
        }
        //判断门是否与玩家相碰
        //判断敌人是否都被消灭
        //全满足才可跳转下一关
        if (col.CompareTag(Tag.Player))
        {
            if (GameController.Instance.LoadNextLevel())
            {
                gameObject.tag = Tag.Wall;
                GetComponent<BoxCollider2D>().isTrigger = false;
            }
        }
    }
}
