using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform meleeAttackPoint;
    public float meleeAttackRange = 0.5f;
    public LayerMask enemyLayer;

    public Transform rangedAttackPoint;

    public Rigidbody regularProjectilePrefab;
    public Rigidbody chargedProjectilePrefab;
        
    Camera cam;
    Ray ray;
    RaycastHit raycastHit;

    public float meleeDamage;
    public float meleeHealRate;

    public float spellCost;

    public float chargeAttackTimer;
    private float chargeTimerReset;

    public float fireRate;
    private float fireRateReset;

    public float meleeRate;
    private float meleeRateReset;

    public GameObject shootImage;

    public Animator anim;
    private PlayerMethods pm;
    private bool gameOver;

    private void Start()
    {
        pm = GetComponent<PlayerMethods>();
        anim = GetComponent<Animator>();
        gameOver = pm.gameOver;
        cam = Camera.main;
        fireRateReset = fireRate;
        chargeTimerReset = chargeAttackTimer;
        meleeRateReset = meleeRate;
        CheckModifiersBasedOnDualityLevel();
        shootImage.SetActive(true);
    }

    public void CheckModifiersBasedOnDualityLevel()
    {
        if (PlayerPrefs.GetFloat("SpellCost") == 0 && PlayerPrefs.GetInt("DualityMeter") != 2)
        {
            spellCost = 10f;
            PlayerPrefs.SetFloat("SpellCost", spellCost);
        }
        else if(PlayerPrefs.GetFloat("SpellCost") != 0 && PlayerPrefs.GetInt("DualityMeter") != 2)
        {
            spellCost = PlayerPrefs.GetFloat("SpellCost");
        }
        else if (PlayerPrefs.GetInt("DualityMeter") == 2)
        {
            spellCost = 0;
            PlayerPrefs.SetFloat("DualityMeter", spellCost);
        }

        if (PlayerPrefs.GetFloat("MeleeHealRate") == 0)
        {
            meleeHealRate = 5f;
            PlayerPrefs.SetFloat("MeleeHealRate", meleeHealRate);
        }
        else
        {
            meleeHealRate = PlayerPrefs.GetFloat("MeleeHealRate");
        }

        if (PlayerPrefs.GetFloat("MeleeDamage") == 0)
        {
            meleeDamage = 20f;
            PlayerPrefs.SetFloat("MeleeDamage", meleeDamage);
        }
        else
        {
            meleeDamage = PlayerPrefs.GetFloat("MeleeDamage");
        }
    }

    private void Update()
    {
        if (!gameOver)
        {
            fireRate -= Time.deltaTime;
            meleeRate -= Time.deltaTime;

            if (fireRate <= 0)
            {
                shootImage.SetActive(true);
            }


            //melee attack
            if (Input.GetMouseButtonDown(0) && meleeRate < 0)
            {
                //animate attack
                anim.SetTrigger("Melee Attack");

                //detect enemy
                Collider[] hitEnemies = Physics.OverlapSphere(meleeAttackPoint.position, meleeAttackRange, enemyLayer);

                //damage enemy
                foreach (Collider enemy in hitEnemies)
                {
                    enemy.GetComponent<Enemy>().TakeDamage(meleeDamage);
                    HealPlayerOnMeleeDamageDealt();
                }
                meleeRate = meleeRateReset;
            }



            //charging attack
            if (Input.GetMouseButton(1) && fireRate < 0)
            {
                //animate charging
                anim.SetBool("Charging", true);

                chargeAttackTimer -= Time.deltaTime;
            }




            //ranged attack 1 (not charged)
            if (Input.GetMouseButtonUp(1) && chargeAttackTimer > 0)
            {
                //animate regular attack
                anim.SetBool("Charging", false);
                anim.SetTrigger("Spell Regular");

                //fire regular attack
                FireRegularProjectile(ray, raycastHit);
                fireRate = fireRateReset;
                chargeAttackTimer = chargeTimerReset;

                bool isCharged = false;
                DamagePlayerOnSpellCast(isCharged);
                shootImage.SetActive(false);
            }



            //ranged attack 2 (charged)
            else if (Input.GetMouseButtonUp(1) && chargeAttackTimer <= 0)
            {
                //animate charged attack
                anim.SetBool("Charging", false);
                anim.SetTrigger("Spell Charged");

                //fire charged attack
                FireChargedProjectile(ray, raycastHit);
                fireRate = fireRateReset;
                chargeAttackTimer = chargeTimerReset;

                bool isCharged = true;
                DamagePlayerOnSpellCast(isCharged);
                shootImage.SetActive(false);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!gameOver)
        {
            ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            ray.origin = rangedAttackPoint.transform.position;

            if (Physics.Raycast(ray, out raycastHit, 100))
            {
                Debug.DrawLine(rangedAttackPoint.transform.position, raycastHit.point, Color.red);
            }
        }
    }



    private void FireRegularProjectile(Ray ray, RaycastHit raycastHit)
    {
        Rigidbody projectile = Instantiate(regularProjectilePrefab, rangedAttackPoint.transform.position, rangedAttackPoint.transform.rotation);
        projectile.velocity = projectile.GetComponent<SpellProjectile>().speed * ray.direction;
    }

    private void FireChargedProjectile(Ray ray, RaycastHit raycastHit)
    {
        Rigidbody projectile = Instantiate(chargedProjectilePrefab, rangedAttackPoint.transform.position, rangedAttackPoint.transform.rotation);
        projectile.velocity = projectile.GetComponent<SpellProjectile>().speed * ray.direction;
    }

    private void HealPlayerOnMeleeDamageDealt()
    {
        pm.currentHealth += meleeHealRate;

        if (pm.currentHealth >= pm.maxHealth)
        {
            pm.currentHealth = pm.maxHealth;
        }
    }

    private void DamagePlayerOnSpellCast(bool isCharged)
    {
        if (!isCharged)
        {
            pm.currentHealth -= spellCost;
        }
        else
        {
            pm.currentHealth -= spellCost * 2;
        }

        pm.CheckGameOver();
    }



    private void OnDrawGizmosSelected()
    {
        if (meleeAttackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(meleeAttackPoint.position, meleeAttackRange);
    }
}
