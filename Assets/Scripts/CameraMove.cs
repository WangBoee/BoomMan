using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Transform player;
    private int horizontalX;
    private int vetcalY;
    private float smoothing = 3.0f;
    public void Init(Transform transform, int x, int y)
    {
        player = transform;
        horizontalX = x;
        vetcalY = y;
    }
    //每一帧调用，在Update调用完成后才调用
    void LateUpdate()
    {
        if (player != null)
        {
            float h = player.position.x;
            float v = player.position.y;
            float l = transform.position.z;
            transform.position = Vector3.Lerp(transform.position, new Vector3(h, v, l), smoothing * Time.deltaTime);

            float x = Mathf.Clamp(transform.position.x, -(horizontalX - 6.39f), horizontalX - 8.38f);
            float y = Mathf.Clamp(transform.position.y, -(vetcalY - 2.5f), vetcalY - 4.5f);
            //Debug.LogError(x + " " + y + "\n" + player.position.x + " " + player.position.y);
            transform.position = new Vector3(x, y, transform.position.z);
        }
    }

    //// Update is called once per frame
    //void FixedUpdate()
    //{
    //    Vector3 targetCamPos = player.position + offset;
    //    transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    //}
}
