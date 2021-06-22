using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	private float speed = 0.0125f;
	private int dirID = 0; //方向, 0:上 1:下 2:左 3:右
	private Vector3 dirVec; //生成的方向
	private float rayDistance=0.7f; //射线检测距离
	private Rigidbody2D rig;

	void Awake()
	{
		dirID = Random.Range(0,4);
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
	private void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log("OnTriggerEnter2D Run");
		Debug.Log("GameObject1 collided with " + col.name);
		this.ChangeDir();
    }
	private void OnTriggerExit2D(Collider2D col)
	{
		Debug.Log("OnTriggerExit2D Run");
		Debug.Log("GameObject1 collided with " + col.name);
		this.ChangeDir();
	}
	private void OnTriggerStay2D(Collider2D col)
	{
		Debug.Log("OnTriggerStay2D Run");
		Debug.Log("GameObject1 collided with " + col.name);
		this.ChangeDir();
	}
	//射线检测
	//改变方向
	private void ChangeDir()
    {
		List<int> dirList = new List<int>();    //存储可移动方向
		Debug.Log("Info: Player pos: "+transform.position);
		Debug.Log("Info: Ray pos: " + ((Vector2)transform.position + new Vector2(0, 1)));
		Debug.Log("Info: Dest pos:" + ((Vector2)transform.position + Vector2.up));
        if (!Physics2D.Raycast((Vector2)transform.position+new Vector2(0,1), (Vector2)transform.position + Vector2.up,rayDistance))
        {
			dirList.Add(0);	//上方未检测到物体，将此方向添加到列表
        }
        if (!Physics2D.Raycast((Vector2)transform.position + new Vector2(0, -1), (Vector2)transform.position+Vector2.down,rayDistance))
        {
			dirList.Add(1);

		}
        if (!Physics2D.Raycast((Vector2)transform.position + new Vector2(-1,0), (Vector2)transform.position + Vector2.left,rayDistance))
        {
			dirList.Add(2);
        }
        if (!Physics2D.Raycast((Vector2)transform.position + new Vector2(1,0 ), (Vector2)transform.position + Vector2.right,rayDistance))
        {
			dirList.Add(3);
        }
        if (dirList.Count>0)
		{
			int index = Random.Range(0, dirList.Count);
			InitDir(dirList[index]);
		}
    }
}
