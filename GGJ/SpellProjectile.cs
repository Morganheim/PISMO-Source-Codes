using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class SpellProjectile : MonoBehaviour
{
    Rigidbody rb;
    public float spellDamage;
    public float speed;
    [SerializeField]
    private bool isChargedAttack;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        CheckForModifiers();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (isChargedAttack)
            {
                other.gameObject.GetComponent<Enemy>().TakeDamage(spellDamage * 3);
            }
            else
            {
                other.gameObject.GetComponent<Enemy>().TakeDamage(spellDamage);
            }

            Destroy(this.gameObject);
        }
        else if (other.gameObject.tag != "Enemy")
        {
            Destroy(this.gameObject);
        }
    }

    private void CheckForModifiers()
    {
        if (PlayerPrefs.GetFloat("SpellDamage") == 0)
        {
            spellDamage = 15f;
            PlayerPrefs.SetFloat("SpellDamage", spellDamage);
        }
        else
        {
            spellDamage = PlayerPrefs.GetFloat("SpellDamage");
        }
    }
}
