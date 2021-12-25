using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowRotation : MonoBehaviour
{
    public float speed;
    private void FixedUpdate()
    {
        float angleZ = speed * Time.deltaTime;
        transform.Rotate(0, 0, angleZ);
        //transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y,
        //    transform.rotation.z * speed * Time.deltaTime, transform.rotation.w);
    }
}