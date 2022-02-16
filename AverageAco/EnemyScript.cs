using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyScript : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Animator animator;
    public float direction = 0.02f;
    Collider2D collidder;
    private void Start()
    {
        collidder = GetComponent<Collider2D>(); 
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "stop")
        {
            direction *= -1;
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }

    private void Update()
    {
        transform.position += new Vector3(direction, 0, 0) * Time.deltaTime;
    }

    //public void ChangeSprite()
    //{
    //    alive = false;
    //    animator.enabled = false;
    //    spriteRenderer.sprite = deadSprite;
    //    //collidder.enabled = false;
    //    rb.simulated = true;
    //}
}

