using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabaSkripta : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Animator animator;
    public float direction;
    Collider2D collidder;
    Transform spawner;
    public GameObject[] veggies;

    public GameObject headTurnedRight;
    public GameObject headtTurnedLeft;

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
            BabaTurnHead();
        }
    }

    private void Update()
    {
        transform.position += new Vector3(direction, 0, 0) * Time.deltaTime;
    }

    private void BabaTurnHead()
    {
        if (headTurnedRight.activeSelf)
        {
            headTurnedRight.SetActive(false);
            headtTurnedLeft.SetActive(true);
        }
        else
        {
            headTurnedRight.SetActive(true);
            headtTurnedLeft.SetActive(false);
        }
    }

    public IEnumerator ShootFood()
    {
        while(true)
        {
            animator.SetBool("isThrowing", false);
            yield return new WaitForSeconds(3);
            int i = Random.Range(0, veggies.Length);
            animator.SetBool("isThrowing", true);
            Instantiate(veggies[i], spawner.position, Quaternion.identity);
        }
    }
}
