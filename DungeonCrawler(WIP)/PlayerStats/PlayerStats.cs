using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    private int saveFile; //save file is used to determine what save file is currently being used

    [Header("Player Name Info")]
    public string playerName; //name of the player character
    public Text[] playerNameTexts; //all the text components displaying the player character's name

    [Header("Portrait Info")]
    public Sprite portrait; //chosen portrait
    public Sprite[] availablePortraits; //array of all portraits from the portrait directory, from which the chosen portrait is taken
    public Image portraitUI; //the image on the game UI taht displays the character portrait

    [Header("Stats")] //all character stats
    public float health; //current health of the player character
    public float maxHealth; //max health value

    public float stamina; //current stamina value of the player character
    public float maxStamina; //max stamina value

    public float hitChance; //chance to hit the enemy when attacking, the equipped weapon adds its hitChance value to this base value
    public float attackTimer; //this value is taken from the equipped weapon, used to determine the attack cooldown time
    public float staminaCost; //this is the percent amount that is subtracted from the total stamina cost value of any used ability or spell

    public float dodge; //a modifier that influences the chance to not get hit by an enemy attack
    public float protection; //a modifier loaded from equipped armour, reduces damage taken from an enemy attack

    public float resistancePoison; //reduces damage from type poison
    public float resistanceFire; //reduces damage from type fire
    public float resistanceMental; //reduces damage from type mental

    public float carryWeightCapacity; //player character carry capacity, i.e. how much weight (from items) can the player carry in their inventory, if the weight of the items carried exceeds this value, the player character cannot move
    public float carryWeight; //player's current carried weight

    public float meleeDamage; // modifier to which the damage value of the equipped weapon is added to determine the damage dealt by the equipped melee weapon
    public float rangedDamage; // modifier to which the damage value of the equipped weapon is added to determine the damage dealt by the equipped ranged weapon
    public float spellDamage; // modifier to which the damage of the cast spell is added to determine the damage dealt by the spell

    public int level; //the current level of the player character
    public int experience; //total gained experience points, when an enemy is defeated the experience gain value of that enemy is added to this variable, used to determine when the player levels up
    public int skillPoints; //used to by new levels in skills, these points are gaind upon lebvel up

    [Header("Skills")]
    public int[] skillsLevel = new int[6]; //0 = athletics, 1 = melee, 2 = mentality, 3 = ranged, 4 = sorcery, 5 = strength

    [Header("Unlocked Player Abilities Lists")]
    public List<string> unlockedAbilities = new List<string>();

    [Header("Locked Player Abilities Lists")]
    public string[] lockedAbilities =
    {
        // ATHLETICS (0, 1)
        "Quick Feet", "Quick Strike",
        // MELEE (2, 3)
        "Slash", "Flurry",
        // MENTALITY (4, 5)
        "Willpower", "Nirvana",
        // RANGED (6, 7)
        "Aim", "Head Shot",
        // SORCERY (8, 9)
        "Heal", "Fireball",
        // STRENGTH (10, 11)
        "Mule", "Brute"
    };

    [Header("Scripts")]
    public Levelling levelling; //Levelling script, needed to update character sheet UI upon levelUp
    public PlayerAbilities abilities; //Abilities script, needed to add ability values to stats

    [Header("UI Elements")]
    public Slider healthBarSlider; //slider representing the health on the game UI
    public Slider staminaBarSlider; //slider representing stamina on the game UI
    public GameObject gameOverPanel; //UI panel representing that the player character died

    private void Awake()
    {
        saveFile = PlayerPrefs.GetInt("ActiveSaveFile");
        LoadStatValues();
    }

    private void Start()
    {
        portraitUI.sprite = portrait;
        PrintPlayerNameOnText();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            AddExperience(1000);
        }
    }

    private void PrintPlayerNameOnText()
    {
        for (int i = 0; i < playerNameTexts.Length; i++)
        {
            playerNameTexts[i].text = playerName;
        }
    }

    public void SaveStatValues()
    {
        //Health
        PlayerPrefs.SetFloat("Health" + saveFile, health);
        PlayerPrefs.SetFloat("MaxHealth" + saveFile, maxHealth);

        //Stamina
        PlayerPrefs.SetFloat("Stamina" + saveFile, stamina);
        PlayerPrefs.SetFloat("MaxStamina" + saveFile, maxStamina);

        //Combat Attacking
        PlayerPrefs.SetFloat("HitChance" + saveFile, hitChance);
        PlayerPrefs.SetFloat("AttackTimer" + saveFile, attackTimer);
        PlayerPrefs.SetFloat("StaminaCost" + saveFile, staminaCost);

        //Combat Defense
        PlayerPrefs.SetFloat("Dodge" + saveFile, dodge);
        PlayerPrefs.SetFloat("Protection" + saveFile, protection);

        //Resistances
        PlayerPrefs.SetFloat("PoisonResistance" + saveFile, resistancePoison);
        PlayerPrefs.SetFloat("FireResistance" + saveFile, resistanceFire);
        PlayerPrefs.SetFloat("MentalResistance" + saveFile, resistanceMental);

        //Carry Weight Capacity
        PlayerPrefs.SetFloat("CarryWeight" + saveFile, carryWeight);
        PlayerPrefs.SetFloat("CarryWeightCapacity" + saveFile, carryWeightCapacity);

        //Experience Points
        PlayerPrefs.SetInt("Experience" + saveFile, experience);

        //Level
        PlayerPrefs.SetInt("Level" + saveFile, level);

        //Skill Points
        PlayerPrefs.SetInt("SkillPoints", skillPoints);

        //Skills
        PlayerPrefs.SetInt("AthleticsLevel" + saveFile, skillsLevel[0]);
        PlayerPrefs.SetInt("MeleeLevel" + saveFile, skillsLevel[1]);
        PlayerPrefs.SetInt("MentalityLevel" + saveFile, skillsLevel[2]);
        PlayerPrefs.SetInt("RangedLevel" + saveFile, skillsLevel[3]);
        PlayerPrefs.SetInt("SorceryLevel" + saveFile, skillsLevel[4]);
        PlayerPrefs.SetInt("StrengthLevel" + saveFile, skillsLevel[5]);

        //Abilities
        foreach(string ability in unlockedAbilities)
        {
            PlayerPrefs.SetString(ability + saveFile, ability);
        }
    }

    private void LoadStatValues()
    {
        playerName = PlayerPrefs.GetString("PlayerName" + saveFile);
        portrait = availablePortraits[PlayerPrefs.GetInt("PortraitIndex" + saveFile)];
        health = PlayerPrefs.GetFloat("Health" + saveFile);
        maxHealth = PlayerPrefs.GetFloat("MaxHealth" + saveFile);
        stamina = PlayerPrefs.GetFloat("Stamina" + saveFile);
        maxStamina = PlayerPrefs.GetFloat("MaxStamina" + saveFile);
        hitChance = PlayerPrefs.GetFloat("HitChance" + saveFile);
        attackTimer = PlayerPrefs.GetFloat("AttackTimer" + saveFile);
        staminaCost = PlayerPrefs.GetFloat("StaminaCost" + saveFile);
        dodge = PlayerPrefs.GetFloat("Dodge" + saveFile);
        protection = PlayerPrefs.GetFloat("Protection" + saveFile);
        resistancePoison = PlayerPrefs.GetFloat("PoisonResistance" + saveFile);
        resistanceFire = PlayerPrefs.GetFloat("FireResistance" + saveFile);
        resistanceMental = PlayerPrefs.GetFloat("MentalResistance" + saveFile);
        carryWeight = PlayerPrefs.GetFloat("CarryWeight" + saveFile);
        carryWeightCapacity = PlayerPrefs.GetFloat("CarryWeightCapacity" + saveFile);
        experience = PlayerPrefs.GetInt("Experience" + saveFile);
        level = PlayerPrefs.GetInt("Level" + saveFile);
        skillPoints = PlayerPrefs.GetInt("SkillPoints");
        skillsLevel[0] = PlayerPrefs.GetInt("AthleticsLevel" + saveFile);
        skillsLevel[1] = PlayerPrefs.GetInt("MeleeLevel" + saveFile);
        skillsLevel[2] = PlayerPrefs.GetInt("MentalityLevel" + saveFile);
        skillsLevel[3] = PlayerPrefs.GetInt("RangedLevel" + saveFile);
        skillsLevel[4] = PlayerPrefs.GetInt("SorceryLevel" + saveFile);
        skillsLevel[5] = PlayerPrefs.GetInt("StrengthLevel" + saveFile);

        LoadAbilities();
    }

    private void LoadAbilities()
    {
        for (int i = 0; i < lockedAbilities.Length; i++)
        {
            if (PlayerPrefs.HasKey(lockedAbilities[i] + saveFile))
            {
                unlockedAbilities.Add(lockedAbilities[i]);
            }
        }
    }

    public void AddExperience(int amount)
    {
        experience += amount;
        CheckIfLevelUp();
        levelling.UpdateCharacterSheetUIAndPrintNewStatValues();
    }

    private void LevelUp()
    {
        //add level
        level++;

        //add skill points
        if (level != 5)
        {
            skillPoints += 2;
        }
        else if (level == 5)
        {
            skillPoints += 3;
        }

        //add stats
        if (level == 2)
        {
            maxHealth += 5;
            maxStamina += 10;
            dodge += 1;
        }
        else if (level == 3)
        {
            maxHealth += 10;
            maxStamina += 15;
            dodge += 2;
        }
        else if (level == 4)
        {
            maxHealth += 5;
            maxStamina += 10;
            dodge += 1;
        }
        else if (level == 5)
        {
            maxHealth += 10;
            maxStamina += 15;
            dodge += 2;
        }

        //play particle effect for levelup


        //save values
        SaveStatValues();

        //update values on character sheet UI
        levelling.UpdateCharacterSheetUIAndPrintNewStatValues();
    }

    public void CheckIfLevelUp()
    {
        if (experience >= 1000 && level == 1)
        {
            //levelled up from 1 to 2
            LevelUp();

            //play level up particle effect

        }
        else if (experience >= 2000 && level == 2)
        {
            //levelled up from 2 to 3
            LevelUp();
            
            //play level up particle effect

        }
        else if (experience >= 4000 && level == 3)
        {
            //levelled up from 3 to 4
            LevelUp();
            
            //play level up particle effect

        }
        else if (experience >= 8000 && level == 4)
        {
            //levelled up from 4 to 5
            LevelUp();

            //play level up particle effect

        }
    }

    public void AddSkillLevelStatChange(int skillIndex, int skillLevel)
    {
        float dodgeIncrease;
        float hitChanceIncrease;
        float staminaCostIncrease;
        float rangedDamageIncrease;
        float spellDamageIncrease;
        float meleeDamageIncrease;

        if (skillLevel == 5) //values for skill level 5
        {
            dodgeIncrease = 3;
            hitChanceIncrease = 0.3f;
            staminaCostIncrease = 0.1f;
            rangedDamageIncrease = 5;
            spellDamageIncrease = 5;
            meleeDamageIncrease = 5;
        }
        else if (skillLevel == 3) //values for skill level 3
        {
            dodgeIncrease = 1;
            hitChanceIncrease = 0.25f; //only difference
            staminaCostIncrease = 0.05f;
            rangedDamageIncrease = 2;
            spellDamageIncrease = 2;
            meleeDamageIncrease = 2;
        }
        else //values for all other skill levels
        {
            dodgeIncrease = 1;
            hitChanceIncrease = 0.15f;
            staminaCostIncrease = 0.05f;
            rangedDamageIncrease = 2;
            spellDamageIncrease = 2;
            meleeDamageIncrease = 2;
        }

        switch (skillIndex)
        {
            case 0: dodge += dodgeIncrease; break; //ATHLETICS
            case 1: hitChance += hitChanceIncrease; break; //MELEE
            case 2: staminaCost += staminaCostIncrease; break; //MENTALITY
            case 3: rangedDamage += rangedDamageIncrease; break; //RANGED
            case 4: spellDamage += spellDamageIncrease; break; //SORCERY
            case 5: meleeDamage += meleeDamageIncrease; break; //STRENGTH
        }
    }

    public void RemoveSkillLevelStatChange(int skillIndex, int skillLevel)
    {
        float dodgeIncrease;
        float hitChanceIncrease;
        float staminaCostIncrease;
        float rangedDamageIncrease;
        float spellDamageIncrease;
        float meleeDamageIncrease;

        if (skillLevel == 4) //values for skill level going from 5 to 4
        {
            dodgeIncrease = 3;
            hitChanceIncrease = 0.3f;
            staminaCostIncrease = 0.1f;
            rangedDamageIncrease = 5;
            spellDamageIncrease = 5;
            meleeDamageIncrease = 5;
        }
        else if(skillLevel == 2) //values for skill levels going from 3 to 2
        {
            dodgeIncrease = 1;
            hitChanceIncrease = 0.25f; //only difference
            staminaCostIncrease = 0.05f;
            rangedDamageIncrease = 2;
            spellDamageIncrease = 2;
            meleeDamageIncrease = 2;
        }
        else //values for all other skill level changes
        {
            dodgeIncrease = 1;
            hitChanceIncrease = 0.15f;
            staminaCostIncrease = 0.05f;
            rangedDamageIncrease = 2;
            spellDamageIncrease = 2;
            meleeDamageIncrease = 2;
        }

        switch (skillIndex)
        {
            case 0: dodge -= dodgeIncrease; break; //ATHLETICS
            case 1: hitChance -= hitChanceIncrease; break; //MELEE
            case 2: staminaCost -= staminaCostIncrease; break; //MENTALITY
            case 3: rangedDamage -= rangedDamageIncrease; break; //RANGED
            case 4: spellDamage -= spellDamageIncrease; break; //SORCERY
            case 5: meleeDamage -= meleeDamageIncrease; break; //STRENGTH
        }
    }

    public void AddAbilityOnSkillChange(int skillIndex, int skillLevel)
    {
        if (skillLevel == 2)
        {
            if (skillIndex == 0) //ATHLETICS
            {
                if (!unlockedAbilities.Contains(lockedAbilities[0]))
                {
                    //add Quick Feet to unlocked ability List
                    unlockedAbilities.Add(lockedAbilities[0]);
                    //add dodge bonus from quick feet
                    dodge += abilities.QuickFeet();
                }
            }
            else if (skillIndex == 1) //MELEE
            {
                if (!unlockedAbilities.Contains(lockedAbilities[2]))
                {
                    //add Slash to unlocked ability List
                    unlockedAbilities.Add(lockedAbilities[2]);
                }
            }
            else if (skillIndex == 2) //MENTALITY
            {
                if (!unlockedAbilities.Contains(lockedAbilities[4]))
                {
                    //add Willpower to unlocked ability List
                    unlockedAbilities.Add(lockedAbilities[4]);
                }
            }
            else if (skillIndex == 3) //RANGED
            {
                if (!unlockedAbilities.Contains(lockedAbilities[6]))
                {
                    //add Aim to unlocked ability List
                    unlockedAbilities.Add(lockedAbilities[6]);
                }
            }
            else if (skillIndex == 4) //SORCERY
            {
                if (!unlockedAbilities.Contains(lockedAbilities[8]))
                {
                    //add Heal to unlocked ability List
                    unlockedAbilities.Add(lockedAbilities[8]);
                }
            }
            else if (skillIndex == 5) //STRENGTH
            {
                if (!unlockedAbilities.Contains(lockedAbilities[10]))
                {
                    //add Mule to unlocked ability List
                    unlockedAbilities.Add(lockedAbilities[10]);
                    //add carry weight bonus from mule
                    carryWeightCapacity += abilities.Mule();
                }
            }
        }
        else if (skillLevel == 4)
        {
            if (skillIndex == 0) //ATHLETICS
            {
                if (!unlockedAbilities.Contains(lockedAbilities[1]))
                {
                    unlockedAbilities.Add(lockedAbilities[1]);
                }
            }
            else if (skillIndex == 1) //MELEE
            {
                if (!unlockedAbilities.Contains(lockedAbilities[3]))
                {
                    unlockedAbilities.Add(lockedAbilities[3]);
                }
            }
            else if (skillIndex == 2) //MENTALITY
            {
                if (!unlockedAbilities.Contains(lockedAbilities[5]))
                {
                    unlockedAbilities.Add(lockedAbilities[5]);
                }
            }
            else if (skillIndex == 3) //RANGED
            {
                if (!unlockedAbilities.Contains(lockedAbilities[7]))
                {
                    unlockedAbilities.Add(lockedAbilities[7]);
                }
            }
            else if (skillIndex == 4) //SORCERY
            {
                if (!unlockedAbilities.Contains(lockedAbilities[9]))
                {
                    unlockedAbilities.Add(lockedAbilities[9]);
                }
            }
            else if (skillIndex == 5) //STRENGTH
            {
                if (!unlockedAbilities.Contains(lockedAbilities[11]))
                {
                    unlockedAbilities.Add(lockedAbilities[11]);
                }
            }
        }
    }

    public void RemoveAbilityOnSkillChange(int skillIndex, int skillLevel)
    {
        if (skillLevel == 1)
        {
            if (skillIndex == 0)
            {
                if (unlockedAbilities.Contains(lockedAbilities[0]))
                {
                    dodge -= abilities.QuickFeet();
                    unlockedAbilities.Remove(lockedAbilities[0]);
                }
            }
            else if (skillIndex == 1)
            {
                if (unlockedAbilities.Contains(lockedAbilities[2]))
                {
                    unlockedAbilities.Remove(lockedAbilities[2]);
                }
            }
            else if (skillIndex == 2)
            {
                if (unlockedAbilities.Contains(lockedAbilities[4]))
                {
                    unlockedAbilities.Remove(lockedAbilities[4]);
                }
            }
            else if (skillIndex == 3)
            {
                if (unlockedAbilities.Contains(lockedAbilities[6]))
                {
                    unlockedAbilities.Remove(lockedAbilities[6]);
                }
            }
            else if (skillIndex == 4)
            {
                if (unlockedAbilities.Contains(lockedAbilities[8]))
                {
                    unlockedAbilities.Remove(lockedAbilities[8]);
                }
            }
            else if (skillIndex == 5)
            {
                if (unlockedAbilities.Contains(lockedAbilities[10]))
                {
                    carryWeightCapacity -= abilities.Mule();
                    unlockedAbilities.Remove(lockedAbilities[10]);
                }
            }
        }
        else if (skillLevel == 3)
        {
            if (skillIndex == 0)
            {
                if (unlockedAbilities.Contains(lockedAbilities[1]))
                {
                    unlockedAbilities.Remove(lockedAbilities[1]);
                }
            }
            else if (skillIndex == 1)
            {
                if (unlockedAbilities.Contains(lockedAbilities[3]))
                {
                    unlockedAbilities.Remove(lockedAbilities[3]);
                }
            }
            else if (skillIndex == 2)
            {
                if (unlockedAbilities.Contains(lockedAbilities[5]))
                {
                    unlockedAbilities.Remove(lockedAbilities[5]);
                }
            }
            else if (skillIndex == 3)
            {
                if (unlockedAbilities.Contains(lockedAbilities[7]))
                {
                    unlockedAbilities.Remove(lockedAbilities[7]);
                }
            }
            else if (skillIndex == 4)
            {
                if (unlockedAbilities.Contains(lockedAbilities[9]))
                {
                    unlockedAbilities.Remove(lockedAbilities[9]);
                }
            }
            else if (skillIndex == 5)
            {
                if (unlockedAbilities.Contains(lockedAbilities[11]))
                {
                    unlockedAbilities.Remove(lockedAbilities[11]);
                }
            }
        }
    }

    public void UpdateHealthBar()
    {
        CheckIfValueIsBelowZero(health);
        healthBarSlider.value = health;
    }

    public void UpdateStaminaBar()
    {
        CheckIfValueIsBelowZero(stamina);
        staminaBarSlider.value = stamina;
    }

    private void CheckIfValueIsBelowZero(float value)
    {
        if (value <= 0)
        {
            value = 0;
        }
    }

    public void CheckIfValueIsMax(float value, float maxValue)
    {
        if (value >= maxValue)
        {
            value = maxValue;
        }
    }
}
