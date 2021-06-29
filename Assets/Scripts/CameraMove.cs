using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Transform player;
    private float smoothing = 3.0f;
    public void Init(Transform transform)
    {
        player = transform;
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
        }
    }

    //// Update is called once per frame
    //void FixedUpdate()
    //{
    //    Vector3 targetCamPos = player.position + offset;
    //    transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    //}
}
