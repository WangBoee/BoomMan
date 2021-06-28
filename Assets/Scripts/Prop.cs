using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Prop : MonoBehaviour
{
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
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(Tag.BombEffect))
        {
            //开启道具触发器
            this.GetComponent<BoxCollider2D>().isTrigger = true;
            //TODO
        }
    }
}
