using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    //触发器
    //判断可普通墙体是否被炸到
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(Tag.BombEffect))
        {
            Destroy(this.gameObject);
        }
    }
}
