using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEffect : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }
    //方法1 使用帧事件来处理动画执行完的动作
    //当动画播放完成执行
    private void AnimFinish()
    {
        Debug.Log("帧事件销毁爆炸特效");
        ObjPool.Instance.AddObj(ObjectType.BombEffect, gameObject);  //回收炸弹特效
    }

    //方法2 判断动画是否播放完成
    /*
    private Animator animator;
    void Awake()
    {
        animator = this.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime >= 1 && info.IsName("BombEffect")) ;
        {
            Destroy(this.gameObject);
        }
    }
    */
}
