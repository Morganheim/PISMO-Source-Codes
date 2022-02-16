using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSmoother : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.8f;
    public Vector3 offset;

    private void Update()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        //za kvazi 3d feel
        //transform.LookAt(target);
    }
}
