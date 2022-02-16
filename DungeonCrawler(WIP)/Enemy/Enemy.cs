using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float moveTimer;
    [SerializeField] private float moveTimerReset;
    [SerializeField] private float attackTimer;
    [SerializeField] private float attackTimerReset;
    [SerializeField] private float hitChance;
    [SerializeField] private float damage;

    public Transform meleeAttackPoint;
    public float meleeAttackRange = 0.5f;
    public LayerMask playerLayer;

    private const float leftHand = -90f;
    private const float rightHand = +90f;
    private Quaternion rotateFromDirection;
    private Quaternion rotateTowardsDirection;
    private float rotationTime = 0.0f;
    [SerializeField] private float rotationSpeed = 5f;

    public NavMeshAgent agent;
    public Animator anim;
    public EnemyData data;

    private void Start()
    {
        health = data.hp;
        moveTimerReset = data.movementSpeed;
        moveTimer = moveTimerReset;
        attackTimerReset = data.attackTimer;
        attackTimer = attackTimerReset;
        hitChance = data.hitChance;
        damage = data.damage;
        LevelLayouts.ChangeLayoutToImpassable(SceneManager.GetActiveScene().buildIndex, Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z), "Enemy");
        anim = GetComponent<Animator>();
        rotateTowardsDirection = transform.rotation;
        rotateFromDirection = transform.rotation;
    }

    private void Update()
    {
        moveTimer -= Time.deltaTime;
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0)
        {
            //detect player
            Collider[] players = Physics.OverlapSphere(meleeAttackPoint.position, meleeAttackRange, playerLayer);

            //damage player
            foreach (Collider player in players)
            {
                int chanceToHit = Random.Range(0, 101);
                if (chanceToHit + player.GetComponentInChildren<PlayerStats>().dodge + (hitChance * 100) >= 100)
                {
                    Debug.Log("Miss");
                }
                else
                {
                    Attack(player);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            TurnLeft();
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            TurnRight();
        }
        if (IsRotating())
        {
            AnimateRotation();
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        CheckIfDead();

        //play take hit animation
        anim.SetTrigger("Hit");
    }

    private void Attack(Collider player)
    {
        //play attack animation
        anim.SetTrigger("Attack");

        //deal damage to player
        player.GetComponent<Combat>().TakeDamage(damage);

        Debug.Log($"{player.GetComponentInChildren<PlayerStats>().playerName} was hit for {damage} damage!");
    }

    private void MoveOneStep()
    {
        //move one step

        //play move animation
        anim.SetTrigger("Walk", true);

        //reset moveTimer
    }

    private void TurnRight()
    {
        //rotate 90 degrees
        TurnEulerDegrees(rightHand);

        //play turn right animation
        anim.SetTrigger("Turn R");

        //reset moveTimer

    }

    private void TurnLeft()
    {
        //rotate -90 degrees
        TurnEulerDegrees(leftHand);


        //play turn left animation
        anim.SetTrigger("Turn L");

        //reset moveTimer

    }

    private bool IsRotating()
    {
        return transform.rotation != rotateTowardsDirection;
    }

    private void AnimateRotation()
    {
        rotationTime += Time.deltaTime;
        transform.rotation = Quaternion.Slerp(rotateFromDirection, rotateTowardsDirection, rotationTime * rotationSpeed);
        CompensateRotationRoundingErrors();
    }

    private void CompensateRotationRoundingErrors()
    {
        if (transform.rotation == rotateTowardsDirection)
        {
            transform.rotation = rotateTowardsDirection;
        }
    }

    private void TurnEulerDegrees(in float eulerDirectionDelta)
    {
        rotateFromDirection = transform.rotation;
        rotationTime = 0.0f;
        rotateTowardsDirection *= Quaternion.Euler(0.0f, eulerDirectionDelta, 0.0f);
    }

    private void CheckIfDead()
    {
        if (health <= 0)
        {
            //play death animation
            anim.SetTrigger("Death", true);

            //give experience to player

            //call CheckLeveUp method from PlayerStats script

            Debug.Log("Killed enemy: " + this.gameObject.name);

            //on animation end
            LevelLayouts.ChangeLayoutToPassable(SceneManager.GetActiveScene().buildIndex, Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
            this.gameObject.SetActive(false);
        }
    }
}
