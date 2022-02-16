using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combat : MonoBehaviour
{
    public PlayerStats stats;
    public PlayerAbilities abilities;

    [SerializeField] private bool gameOver; //bool for determining if game is over

    public Button attackButton;
    public Button spellButton;
    public Text hitMissText;

    public float attackTimer;
    private float attackTimerReset;
    [SerializeField] private bool meleeAttack;
    [SerializeField] private bool rangedAttack;
    private bool spellCast;

    private float hitChance;
    private float damageMelee;
    private float rangedDamage;
    private float spellDamage;

    public Transform meleeAttackPoint;
    public float meleeAttackRange = 0.5f;
    public LayerMask enemyLayer;

    private void Start()
    {
        stats = GetComponentInChildren<PlayerStats>();
        abilities = GetComponentInChildren<PlayerAbilities>();
        LoadStatValues();
        meleeAttack = false;
        attackTimer = 0;
        hitMissText.enabled = false;
    }

    private void Update()
    {
        if (!gameOver)
        {
            attackTimer -= Time.deltaTime;

            if (attackTimer > 0 && attackButton.interactable && spellButton.interactable)
            {
                attackButton.interactable = false;
                spellButton.interactable = false;
            }
            else if (attackTimer <= 0 && !attackButton.interactable && !spellButton.interactable)
            {
                attackButton.interactable = true;
                spellButton.interactable = true;
            }

            if (meleeAttack)
            {
                //detect enemy
                Collider[] hitEnemies = Physics.OverlapSphere(meleeAttackPoint.position, meleeAttackRange, enemyLayer);

                //damage enemy
                foreach (Collider enemy in hitEnemies)
                {
                    float chanceToHit = Random.Range(0f, 1.5f);
                    if (chanceToHit + hitChance < 1)
                    {
                        Debug.Log("Miss");
                        hitMissText.text = "Miss!";
                        hitMissText.enabled = true;
                        Invoke("DeactivateHitMissText", 2f);
                    }
                    else
                    {
                        MeleeAttack(enemy);
                    }
                }

                meleeAttack = false;
                CheckAndUpdateAttackTimerReset();
                attackTimer = attackTimerReset * abilities.QuickStrike();
            }

            if (rangedAttack)
            {
                float aimDamage = abilities.Aim();
                float headShotDamage = abilities.HeadShot();
                rangedDamage = (stats.rangedDamage + aimDamage) * headShotDamage;

                //fire missile


                //subtract stamina if abilities were used
                CalculateStaminaCostOfRangedAttack(aimDamage, headShotDamage);

                rangedAttack = false;
                CheckAndUpdateAttackTimerReset();
                attackTimer = attackTimerReset * abilities.QuickStrike();
            }

            if (spellCast)
            {
                //detect what spell is cast


                //cast detected spell


                //subtract stamina for cast spell


                spellCast = false;
                CheckAndUpdateAttackTimerReset();
                attackTimer = attackTimerReset * abilities.QuickStrike();
            }
        }
    }

    private void CheckAndUpdateAttackTimerReset()
    {
        //check for weapon's attack timer value or if spell, check detected spell's attack timer value
    }

    private void MeleeAttack(Collider enemy)
    {
        float slashDamage = abilities.Slash();
        float bruteDamage = abilities.Brute();
        damageMelee = (stats.meleeDamage + slashDamage) * bruteDamage;

        bool flurryBool = abilities.Flurry();
        if (flurryBool)
        {
            damageMelee *= 2;
        }

        //subtract stamina if abilities were used
        CalculateStaminaCostOfMeleeAttack(slashDamage, flurryBool);

        //deal damage to enemy
        enemy.GetComponent<Enemy>().TakeDamage(damageMelee);

        Debug.Log($"{enemy.name} was hit for {damageMelee} damage!");
        hitMissText.text = $"{damageMelee}";
        hitMissText.enabled = true;
        Invoke("DeactivateHitMissText", 2f);
    }

    public void AttackOnButtonClick()
    {
        meleeAttack = true;
    }

    private void DeactivateHitMissText()
    {
        hitMissText.enabled = false;
    }

    public void TakeDamage(float damage)
    {
        stats.health -= damage;
        stats.UpdateHealthBar();

        if (stats.health <= 0)
        {
            gameOver = true;
            stats.gameOverPanel.SetActive(true);
            this.transform.parent.gameObject.SetActive(false);
        }
    }

    private void CalculateStaminaCostOfMeleeAttack(float slashDamage, bool flurryBool)
    {
        float staminaCostOfAttack = 0;
        if (slashDamage != 0)
        {
            staminaCostOfAttack += abilities.SlashStaminaCost();
        }

        if (flurryBool)
        {
            staminaCostOfAttack += abilities.FlurryStaminaCost();
        }

        staminaCostOfAttack *= (1 - stats.staminaCost);

        staminaCostOfAttack *= abilities.Willpower();
        staminaCostOfAttack *= abilities.Nirvana();

        stats.stamina -= staminaCostOfAttack;

        stats.UpdateStaminaBar();
    }

    private void CalculateStaminaCostOfRangedAttack(float aimDamage, float headShotDamage)
    {
        float staminaCostOfAttack = 0;
        if (aimDamage != 0)
        {
            staminaCostOfAttack += abilities.AimStaminaCost();
        }
        if (headShotDamage != 1)
        {
            staminaCostOfAttack += abilities.HeadShotStaminaCost();
        }

        staminaCostOfAttack *= (1 - stats.staminaCost);

        staminaCostOfAttack *= abilities.Willpower();
        staminaCostOfAttack *= abilities.Nirvana();

        stats.stamina -= staminaCostOfAttack;

        stats.UpdateStaminaBar();
    }

    public void LoadStatValues()
    {
        attackTimerReset = stats.attackTimer;
        damageMelee = stats.meleeDamage;
        rangedDamage = stats.rangedDamage;
        spellDamage = stats.spellDamage;
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
