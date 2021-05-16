using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;

    void Start()
    {
               
    }

    void LateUpdate()
    {
        transform.position = new Vector3()
        {
            x = target.position.x,
            y = target.position.y,
            z = transform.position.z
        };
    }
}
