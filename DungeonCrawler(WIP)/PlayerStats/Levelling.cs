using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Levelling : MonoBehaviour
{
    public PlayerStats stats;

    public Button[] addSkillLevelButtons;
    public Button[] removeSkillLevelButtons;

    public Text[] skillLevelValueTexts;

    private int skillPointPool;
    public Text skillPointPoolText;

    private int[] currentSkillLevels = new int[6];
    private int[] levelledSkillLevels = new int[6];

    public Button commitSkillPointsButton;

    public Text hpText, staminaText, carryWeightText, hitChanceText, expText, dodgeText, protectionText, staminaCostText, damageMeleeText, damageRangedText, damageSpellText, currentLevelOnInventoryText, curentLevelOnCSText;

    private void Start()
    {
        skillPointPool = stats.skillPoints;

        for (int i = 0; i < levelledSkillLevels.Length; i++)
        {
            currentSkillLevels[i] = stats.skillsLevel[i];
            levelledSkillLevels[i] = stats.skillsLevel[i];
        }

        UpdateCharacterSheetUIAndPrintNewStatValues();
    }

    public void UpdateCharacterSheetUIAndPrintNewStatValues()
    {
        CheckEnablingSkillButtons();
        PrintAllCharacterSheetValuesOnUI();
    }

    private void PrintCharacterStatsOnText()
    {
        hpText.text = $"Health: {stats.health}/{stats.maxHealth}";
        staminaText.text = $"Stamina: {stats.stamina}/{stats.maxStamina}";
        damageMeleeText.text = $"Damage M: {stats.meleeDamage}";
        damageRangedText.text = $"Damage R: {stats.rangedDamage}";
        damageSpellText.text = $"Damage S: {stats.spellDamage}";
        dodgeText.text = $"Dodge: {stats.dodge}";
        protectionText.text = $"Protection: {stats.protection}";
        hitChanceText.text = $"Hit Chance: {stats.hitChance}";
        staminaCostText.text = $"Stamina Cost: -{stats.staminaCost}%";
        carryWeightText.text = $"Encumbrance: {stats.carryWeight}kg / {stats.carryWeightCapacity}kg";
        expText.text = $"Exp: {stats.experience}";
        currentLevelOnInventoryText.text = $"Level: {stats.level}";
        curentLevelOnCSText.text = $"Level: {stats.level}";
    }

    private void PrintSkillPointPoolOnText()
    {
        skillPointPoolText.text = "Skill Points: " + skillPointPool.ToString();
    }

    private void PrintSkillLevelOnText(int skillIndex)
    {
        skillLevelValueTexts[skillIndex].text = levelledSkillLevels[skillIndex].ToString();
    }

    private void PrintAllCharacterSheetValuesOnUI()
    {
        for (int i = 0; i < 6; i++)
        {
            PrintSkillLevelOnText(i);
        }
        PrintSkillPointPoolOnText();
        PrintCharacterStatsOnText();
    }

    private void EnableOrDisableAddSkillButtons()
    {
        if (skillPointPool <= 0)
        {
            skillPointPool = 0;

            for (int i = 0; i < addSkillLevelButtons.Length; i++)
            {
                addSkillLevelButtons[i].interactable = false;
            }
        }
        else
        {
            for (int i = 0; i < addSkillLevelButtons.Length; i++)
            {
                addSkillLevelButtons[i].interactable = true;
            }
        }

        for (int i = 0; i < levelledSkillLevels.Length; i++)
        {
            if (levelledSkillLevels[i] == 5)
            {
                addSkillLevelButtons[i].interactable = false;
            }
        }
    }

    private void EnableRemoveSkillButtons()
    {
        for (int i = 0; i < levelledSkillLevels.Length; i++)
        {
            if (levelledSkillLevels[i] > currentSkillLevels[i])
            {
                removeSkillLevelButtons[i].interactable = true;
            }
        }
    }

    private void DisableRemoveSkillButtons()
    {
        for (int i = 0; i < levelledSkillLevels.Length; i++)
        {
            if (levelledSkillLevels[i] == currentSkillLevels[i])
            {
                removeSkillLevelButtons[i].interactable = false;
            }
        }
    }

    private void CheckEnablingSkillButtons()
    {
        EnableOrDisableAddSkillButtons();
        EnableRemoveSkillButtons();
        DisableRemoveSkillButtons();
    }

    public void AddSkillLevelOnButtonClick(int skillIndex)
    {
        //if (skillPointPool > 0)
        //{
        //    levelledSkillLevels[skillIndex]++;

        //    skillPointPool--;
        //}
        levelledSkillLevels[skillIndex]++;

        skillPointPool--;

        stats.AddSkillLevelStatChange(skillIndex, levelledSkillLevels[skillIndex]);

        stats.AddAbilityOnSkillChange(skillIndex, levelledSkillLevels[skillIndex]);

        UpdateCharacterSheetUIAndPrintNewStatValues();
    }

    public void RemoveSkillLevelOnButtonClick(int skillIndex)
    {
        levelledSkillLevels[skillIndex]--;

        skillPointPool++;

        stats.RemoveSkillLevelStatChange(skillIndex, levelledSkillLevels[skillIndex]);

        stats.RemoveAbilityOnSkillChange(skillIndex, levelledSkillLevels[skillIndex]);

        UpdateCharacterSheetUIAndPrintNewStatValues();
    }

    private void UpdatePlayerStatsFromLevellingScriptValues()
    {
        for (int i = 0; i < currentSkillLevels.Length; i++)
        {
            stats.skillsLevel[i] = currentSkillLevels[i];
        }
    }

    public void CommitSkillPoints()
    {
        for (int i = 0; i < levelledSkillLevels.Length; i++)
        {
            switch (i)
            {
                case 0: currentSkillLevels[i] = levelledSkillLevels[i]; break; //ATHLETICS
                case 1: currentSkillLevels[i] = levelledSkillLevels[i]; break; //MELEE
                case 2: currentSkillLevels[i] = levelledSkillLevels[i]; break; //MENTALITY
                case 3: currentSkillLevels[i] = levelledSkillLevels[i]; break; //RANGED
                case 4: currentSkillLevels[i] = levelledSkillLevels[i]; break; //SORCERY
                case 5: currentSkillLevels[i] = levelledSkillLevels[i]; break; //STRENGTH
            }
        }

        stats.skillPoints = skillPointPool;

        UpdatePlayerStatsFromLevellingScriptValues();

        UpdateCharacterSheetUIAndPrintNewStatValues();

        stats.SaveStatValues();
    }
}
