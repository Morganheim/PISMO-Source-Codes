using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFlower : MonoBehaviour
{
    public float rotationSpeedInAngle = 10f;
    private void Update()
    {
        if(rotationSpeedInAngle <= 45)
        {
            transform.Rotate(0 ,0 , rotationSpeedInAngle * Time.deltaTime);
        }
        else if (rotationSpeedInAngle > 45)
        {
            transform.Rotate(0, 0, 45f * Time.deltaTime);
        }
    }
}
