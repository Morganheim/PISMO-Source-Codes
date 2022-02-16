using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellCombinations : MonoBehaviour
{
    public bool[,] spellCombinations = new bool[3, 3];

    public Button[] spellCombinationButtons;

    public GameObject fireballPrefab;

    public PlayerStats stats;
    public PlayerAbilities abilities;
    public Combat combat;

    private void Start()
    {
        stats = GetComponentInChildren<PlayerStats>();
        abilities = GetComponentInChildren<PlayerAbilities>();
        combat = GetComponent<Combat>();
    }

    public void CastSpell()
    {
        if (spellCombinations[0, 0] && !spellCombinations[0, 1] && spellCombinations[0, 2] && !spellCombinations[1, 0] && spellCombinations[1, 1] && !spellCombinations[1, 2] && spellCombinations[2, 0] && !spellCombinations[2, 1] && spellCombinations[2, 2])
        {
            if (stats.unlockedAbilities.Contains("Heal") && stats.stamina >= abilities.HealStaminaCost())
            {
                // cast heal
                Heal();

                Debug.Log("Cast heal");

                //reset attack timer to the value corresponding the heal spell
                combat.attackTimer = abilities.HealAttackTimer();
            }
        }
        else if (!spellCombinations[0, 0] && spellCombinations[0, 1] && !spellCombinations[0, 2] && spellCombinations[1, 0] && !spellCombinations[1, 1] && spellCombinations[1, 2] && !spellCombinations[2, 0] && !spellCombinations[2, 1] && !spellCombinations[2, 2])
        {
            if (stats.unlockedAbilities.Contains("Fireball") && stats.stamina >= abilities.FireballStaminaCost())
            {
                // cast fireball
                Ray ray = this.GetComponent<Camera>().ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                //Fireball(ray);

                Debug.Log("Cast fireball");

                //reset attack timer to the value corresponding the fireball spell
                combat.attackTimer = abilities.FireballAttackTimer();
            }
            Fireball();
        }
        else
        {
            // NO SPELL CAST
            Debug.Log("NO SPELL WAS CASTED");
        }
    }

    public void Heal()
    {
        float healAmount = stats.maxHealth * 0.2f;
        stats.health += healAmount;
        stats.CheckIfValueIsMax(stats.health, stats.maxHealth);
        stats.UpdateHealthBar();

        //play heal particle effect


        //subtract stamina cost
        CalculateStaminaCostOfSpell(true, false);
    }

    public void Fireball()
    {
        //shoot fireball
        GameObject fireball = Instantiate(fireballPrefab, combat.meleeAttackPoint.transform.position, combat.meleeAttackPoint.transform.rotation);
        //fireball.velocity = fireball.GetComponent<Fireball>().speed * ray.direction;

        //subtract stamina cost
        CalculateStaminaCostOfSpell(false, true);
    }

    private void CalculateStaminaCostOfSpell(bool isHeal, bool isFireball)
    {
        float staminaCostOfAttack = 0;
        if (isHeal)
        {
            staminaCostOfAttack += abilities.HealStaminaCost();
        }

        if (isFireball)
        {
            staminaCostOfAttack += abilities.FireballStaminaCost();
        }

        staminaCostOfAttack *= (1 - stats.staminaCost);

        staminaCostOfAttack *= abilities.Willpower();
        staminaCostOfAttack *= abilities.Nirvana();

        stats.stamina -= staminaCostOfAttack;

        stats.UpdateStaminaBar();
    }

    private void ResetAttackTimerAfterSpellCast()
    {
        
    }

    public void InputSpellCombination(int index)
    {
        int x = index / 10;
        int y = index % 10;
        spellCombinations[x, y] = !spellCombinations[x, y];
        DebugSpellCombo();
    }

    public void ResetInput()
    {
        var rMax = spellCombinations.GetUpperBound(0);
        var cMax = spellCombinations.GetUpperBound(1);

        for (int i = 0; i <= rMax; i++)
        {
            for (int j = 0; j <= cMax; j++)
            {
                spellCombinations[i, j] = false;
            }
        }

        for (int i = 0; i < spellCombinationButtons.Length; i++)
        {
            spellCombinationButtons[i].GetComponent<Image>().sprite = spellCombinationButtons[i].GetComponent<ButtonSymbolChanger>().chernobog[0];
        }

        DebugSpellCombo();
    }

    private void DebugSpellCombo()
    {
        //debuger
        int rMax = spellCombinations.GetUpperBound(0);
        int cMax = spellCombinations.GetUpperBound(1);
        string msg = "";

        for (int i = 0; i <= rMax; i++)
        {
            for (int j = 0; j <= cMax; j++)
            {
                if (spellCombinations[i, j])
                {
                    msg += "true    ";
                }
                else
                {
                    msg += "false    ";
                }
            }
            //msg += "\n";
        }

        Debug.Log(msg);
    }
}
