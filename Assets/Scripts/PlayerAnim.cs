using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    public float speed = 0.05f;
    private Vector2 dest;

    // Use this for initialization
    void Start()
    {
        dest = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        Vector2 t = Vector2.MoveTowards(transform.position, dest, speed);
        GetComponent<Rigidbody2D>().MovePosition(t);
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            dest = (Vector2)transform.position + Vector2.up;
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            dest = (Vector2)transform.position + Vector2.down;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            dest = (Vector2)transform.position + Vector2.left;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            dest = (Vector2)transform.position + Vector2.right;
        }
        //GetComponent<Animator>().SetFloat("Vetical", 0.0f);
        //GetComponent<Animator>().SetFloat("Horizontal", 0.0f);
    }
}
