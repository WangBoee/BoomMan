using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	private float speed = 0.2f;
	private int dirID = 0; //方向, 0:上 1:下 2:左 3:右
	private Vector3 dirVec;	//生成的方向
	private Rigidbody2D rig;

	void Awake()
	{
		dirID = 0;
		InitDir(dirID);
		rig = this.GetComponent<Rigidbody2D>();
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		rig.MovePosition(transform.position+(speed*dirVec));
	}
	//初始化方向
	private void InitDir(int dir)
    {
		dirID = dir;
        switch (dirID)
        {
			case 0:	//上
				dirVec = Vector2.up;
				break;
			case 1:	//下
				dirVec = Vector2.down;
				break;
			case 2:	//左
				dirVec = Vector2.left;
				break;
			case 3:	//右
				dirVec =Vector2.right;
				break;
			default:
				break;
        }
    }
}
