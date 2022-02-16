using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PcelaMovement : MonoBehaviour
{
    public bool move = false;
    Vector3 tempPosition;
    float timer;
    public float timerReset = 5f;
    DestroyerMoveToMouse dmtm;

    public float speed = 35.0f; //speed of shake
    public float amount = 0.2f; //distance of shake

    public GameObject spinPoint;

    private void Start()
    {
        dmtm = FindObjectOfType<DestroyerMoveToMouse>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (move && timer > 0)
        {
            transform.position = tempPosition;
            BeeShake();
        }
        else
        {
            move = false;
            tempPosition = transform.position;
            BeeMovement();
            
        }
    }
    public void OnClickMovement()
    {
        move = true;
        timer = timerReset;
        tempPosition = transform.position;
        Debug.Log("Dotaknuo si pcelu");
    }

    void BeeMovement()
    {
        dmtm.canIClick = true;
        transform.position = tempPosition;
        transform.RotateAround(spinPoint.transform.position, Vector3.forward, -45 * Time.deltaTime);
        transform.Rotate(Vector3.back, -45 * Time.deltaTime);
    }

    void BeeShake()
    {
        dmtm.canIClick = false;
        var x = Mathf.Sin(Time.time * speed) * amount;
        Debug.Log("x = " + x);
        transform.position += new Vector3(x + tempPosition.x, x + tempPosition.y, 0) * Time.deltaTime;
    }
}
