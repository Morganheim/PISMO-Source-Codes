using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.tag == "kocka")
        {
            collision.gameObject.SetActive(false);
            Debug.Log("kolizija s kockom");
        }
    }
}
