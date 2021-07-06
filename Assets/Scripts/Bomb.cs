using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using EZCameraShake;
public class Bomb : MonoBehaviour
{
    private int bombRange; //爆炸范围
    public GameObject bombEffect; //爆炸特效
    private Action onFinAction;
    public void Init(int bRange, float time, Action action)
    {
        this.bombRange = bRange;
        StartCoroutine("DelayBoom", time);
        onFinAction = action;
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    //private void OnTriggerEnter2D(Collider2D col)
    //{
    //    if (CompareTag(Tag.Player))
    //    {
    //        GetComponent<CircleCollider2D>().isTrigger = true;
    //    }
    //}
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag(Tag.Player))
        {
            GetComponent<CircleCollider2D>().isTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag(Tag.Player))
        {
            GetComponent<CircleCollider2D>().isTrigger = false;
        }
    }
    //延时爆炸
    IEnumerator DelayBoom(float time)
    {
        //暂停time秒
        yield return new WaitForSeconds(time);
        if (onFinAction != null)
        {
            onFinAction();
        }
        CameraShaker.Instance.ShakeOnce(4f, 4f, 0.1f, 1.0f);
        AudioController.Instance.PlayBoom(); //播放音效
        //延时结束，删除炸弹物体，生成爆炸特效
        //Destroy(Instantiate(bombEffect, transform.position, Quaternion.identity), 0.5f);
        ObjPool.Instance.GetObj(ObjectType.BombEffect, transform.position);
        //炸弹特效回收改由帧事件处理
        Boom(Vector2.left); //向左延伸爆炸效果
        Boom(Vector2.right); //向右
        Boom(Vector2.up); //向上
        Boom(Vector2.down); //向下
        //Destroy(gameObject);	//销毁炸弹
        gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
        ObjPool.Instance.AddObj(ObjectType.Bomb, gameObject); //回收炸弹
    }
    //生成爆炸特效，爆炸点周围四个方向
    private void Boom(Vector2 dir)
    {
        for (int i = 1; i <= bombRange; i++)
        {
            //当前位置加上某个方向偏移量
            Vector2 pos = (Vector2)transform.position + dir * i;
            //判断生成pos是否存在实体墙（不可销毁）
            if (GameController.Instance.IsSuperWall(pos))
            {
                break;
            }
            if (GameController.Instance.IsWall(pos))
            {
                //实例化炸弹特效并设置位置
                ObjPool.Instance.GetObj(ObjectType.BombEffect, pos);
                GameController.Instance.DelMapWallList(pos);
                break;
            }
            //实例化炸弹特效并设置位置
            ObjPool.Instance.GetObj(ObjectType.BombEffect, pos);
        }
    }
}
//waterhydroxyl到此一游