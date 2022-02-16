using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Fireball : MonoBehaviour
{
    public float speed;
    public float damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(this.gameObject, 2f);

            //play explosion particle effect

        }
    }
}
