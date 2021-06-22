using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Animator anim;//动画状态机
    private float speed = 0.1f;
    private Rigidbody2D rig;
	// Use this for initialization
	void Awake () {
        anim = this.GetComponent<Animator>();
        rig=GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
    //GetAxis线性变换（0-1渐变），GetAxisRaw是跳跃（0-1切换）。
	void Update () {
        Move();
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
