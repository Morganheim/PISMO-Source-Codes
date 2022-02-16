using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerMethods : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;
    public int dualityMeter;

    public PostProcessVolume volume;
    private Vignette vignette;
    private Grain grain;

    public bool gameOver;

    private void Start()
    {
        volume.profile.TryGetSettings(out vignette);
        volume.profile.TryGetSettings(out grain);
        vignette.intensity.value = 0f;
        grain.intensity.value = 0f;

        gameOver = false;

        currentHealth = maxHealth;

        dualityMeter = PlayerPrefs.GetInt("DualityMeter");
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        CheckGameOver();
    }

    public void CheckGameOver()
    {
        if (currentHealth <= 0)
        {
            gameOver = true;
        }
    }

    public void HungerIndulge()
    {
        dualityMeter--;
        vignette.intensity.value = 0.5f;
        grain.intensity.value = 0;
        PlayerPrefs.SetInt("DualityMeter", dualityMeter);
        if (dualityMeter < 0)
        {
            DualityNegativeStatChangeAdd();
        }
        else if (dualityMeter > 0)
        {
            DualityPositiveStatChangeSubtract();
        }
        else
        {
            DualityZero();
        }
    }

    public void HungerResist()
    {
        dualityMeter++;
        PlayerPrefs.SetInt("DualityMeter", dualityMeter);
        grain.intensity.value = 0f;
        if (dualityMeter < 0)
        {
            DualityNegativeStatChangeSubtract();
        }
        else if (dualityMeter > 0)
        {
            DualityPositiveStatChangeAdd();
        }
        else
        {
            DualityZero();
        }
    }

    private void DualityPositiveStatChangeAdd()
    {
        //spell trosi manje hp
        var spellCost = GetComponent<PlayerCombat>().spellCost;
        PlayerPrefs.SetFloat("SpellCost", spellCost * 0.5f);

        //spell trosi vise dmg
        var spellDamage = 15f;
        if (PlayerPrefs.GetFloat("SpellDamage") == 0)
        {
            spellDamage = 15f;
            PlayerPrefs.SetFloat("SpellDamage", spellDamage);
        }
        else
        {
            spellDamage = PlayerPrefs.GetFloat("SpellDamage");
            PlayerPrefs.SetFloat("SpellDamage", spellDamage + (spellDamage * 0.33f));
        }

        //melee manje heala
        var meleeHealRate = GetComponent<PlayerCombat>().meleeHealRate;
        PlayerPrefs.SetFloat("MeleeHealRate", meleeHealRate - (meleeHealRate * 0.2f));

        //melee trosi manje dmg
        var meleeDamage = GetComponent<PlayerCombat>().meleeDamage;
        PlayerPrefs.SetFloat("MeleeDamage", meleeDamage - (meleeDamage * 0.25f));
    }

    private void DualityPositiveStatChangeSubtract()
    {
        //spell trosi manje hp
        var spellCost = GetComponent<PlayerCombat>().spellCost;
        PlayerPrefs.SetFloat("SpellCost", spellCost / 0.5f);

        //spell trosi vise dmg
        var spellDamage = 15f;
        if (PlayerPrefs.GetFloat("SpellDamage") == 0)
        {
            spellDamage = 15f;
            PlayerPrefs.SetFloat("SpellDamage", spellDamage);
        }
        else
        {
            spellDamage = PlayerPrefs.GetFloat("SpellDamage");
            PlayerPrefs.SetFloat("SpellDamage", spellDamage / 1.25f);
        }

        //melee manje heala
        var meleeHealRate = GetComponent<PlayerCombat>().meleeHealRate;
        PlayerPrefs.SetFloat("MeleeHealRate", meleeHealRate / 0.80f);

        //melee trosi manje dmg
        var meleeDamage = GetComponent<PlayerCombat>().meleeDamage;
        PlayerPrefs.SetFloat("MeleeDamage", meleeDamage / 0.75f);
    }

    private void DualityNegativeStatChangeAdd()
    {
        //melee vise heala
        var meleeHealRate = GetComponent<PlayerCombat>().meleeHealRate;
        PlayerPrefs.SetFloat("MeleeHealRate", meleeHealRate + (meleeHealRate * 0.5f));

        //melee vise trosi dmg
        var meleeDamage = GetComponent<PlayerCombat>().meleeDamage;
        PlayerPrefs.SetFloat("MeleeDamage", meleeDamage + (meleeDamage * 0.5f));

        //spell manje trosi dmg
        var spellDamage = 15f;
        if (PlayerPrefs.GetFloat("SpellDamage") == 0)
        {
            spellDamage = 15f;
            PlayerPrefs.SetFloat("SpellDamage", spellDamage);
        }
        else
        {
            spellDamage = PlayerPrefs.GetFloat("SpellDamage");
            PlayerPrefs.SetFloat("SpellDamage", spellDamage - (spellDamage * 0.2f));
        }

        //spell vise trosi hp
        var spellCost = GetComponent<PlayerCombat>().spellCost;
        PlayerPrefs.SetFloat("SpellCost", spellCost + (spellCost * 0.4f));

        //movement speed veci
        var movementSpeed = GetComponent<PlayerMovement>().speedNormal;
        PlayerPrefs.SetFloat("MovementSpeed", movementSpeed + (movementSpeed * 0.25f));

        //dashcooldown manji
        var dashCooldown = GetComponent<PlayerMovement>().dashCooldownReset;
        PlayerPrefs.SetFloat("DashCooldown", dashCooldown - 0.5f);
    }

    private void DualityNegativeStatChangeSubtract()
    {
        //melee vise heala
        var meleeHealRate = GetComponent<PlayerCombat>().meleeHealRate;
        PlayerPrefs.SetFloat("MeleeHealRate", meleeHealRate / 1.5f);

        //melee vise trosi dmg
        var meleeDamage = GetComponent<PlayerCombat>().meleeDamage;
        PlayerPrefs.SetFloat("MeleeDamage", meleeDamage / 1.5f);

        //spell manje trosi dmg
        var spellDamage = 15f;
        if (PlayerPrefs.GetFloat("SpellDamage") == 0)
        {
            spellDamage = 15f;
            PlayerPrefs.SetFloat("SpellDamage", spellDamage);
        }
        else
        {
            spellDamage = PlayerPrefs.GetFloat("SpellDamage");
            PlayerPrefs.SetFloat("SpellDamage", spellDamage / 0.8f);
        }

        //spell vise trosi hp
        var spellCost = GetComponent<PlayerCombat>().spellCost;
        PlayerPrefs.SetFloat("SpellCost", spellCost / 1.4f);

        //movement speed veci
        var movementSpeed = GetComponent<PlayerMovement>().speedNormal;
        PlayerPrefs.SetFloat("MovementSpeed", movementSpeed / 1.25f);

        //dashcooldown manji
        var dashCooldown = GetComponent<PlayerMovement>().dashCooldownReset;
        PlayerPrefs.SetFloat("DashCooldown", dashCooldown + 0.5f);
    }

    private void DualityZero()
    {
        //defult values
        var spellCost = 10f;
        PlayerPrefs.SetFloat("SpellCost", spellCost);
        var spellDamage = 15f;
        PlayerPrefs.SetFloat("SpellDamage", spellDamage);
        var meleeHealRate = 5f;
        PlayerPrefs.SetFloat("MeleeHealRate", meleeHealRate);
        var meleeDamage = 20f;
        PlayerPrefs.SetFloat("MeleeDamage", meleeDamage);
        var movementSpeed = 12f;
        PlayerPrefs.SetFloat("MovementSpeed", movementSpeed);
        var dashCooldown = 4f;
        PlayerPrefs.SetFloat("DashCooldown", dashCooldown);
    }

}
