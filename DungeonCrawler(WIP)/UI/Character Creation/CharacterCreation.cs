using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterCreation : MonoBehaviour
{
    [Header("Save File Info")]
    [SerializeField] private int saveFile; //needed for saving and loading and setting player prefs keys
    public Text chooseSave1Text; //text displaying character name on save file if it is not empty
    public Text chooseSave2Text; //text displaying character name on save file if it is not empty
    public Text chooseSave3Text; //text displaying character name on save file if it is not empty

    [Header("Name Input Field")]
    public InputField nameInput; //input field for character name
    private string playerName; //used to store character name from input field so it can be saved for that saveFile

    [Header("Portrait Info")]
    public Sprite[] availablePortraits; //array of available portraits to be put in by hand from the portraits directory
    private int chosenPortraitIndex; //used to store the index of the chosen portrait so it can be saved for that saveFile
    public GameObject[] portraitBorders; //array of UI borders for portraits, needed for visualizing selected portrait during character creation

    [Header("General Stats")]
    public Text statsValuesText; //text with hp, stamina, dodge etc., that will update as skill levels are added or removed during character creation

    [Header("Skills Levels Info")]
    public Text[] skillsLevelText; //array of texts displaying the skill LEVEL (number)
    [SerializeField] private int[] skillsLevel = new int[6]; //array of skill levels (these are displayed in the above one), each int in the array corresponds to a certain skill in this order: athletics, melee, mentality, ranged, sorcery, strength

    [Header("Skill Point Pool Info")]
    public Text skillPointPoolText; //text displaying available skill points
    [SerializeField] private int skillPointPool; //skill points

    [Header("Skill Buttons")]
    public Button[] addSkillLevelButtons; //buttons that add a skill level (array corresponding to the same order as the skillsLevel variable
    public Button[] removeSkillLevelButtons; //buttons that remove a skill level (as above)

    [Header("Play Game Button")]
    public Button playGameButton; //button that ends character creation and calls a method for saving the chosen values

    [Header("Player Stats")] //player stats that can be influenced by character creation and their base values
    [SerializeField] private float maxHealth = 10; //max value for health
    [SerializeField] private float maxStamina = 10; //max value for stamina
    [SerializeField] private float hitChance = 0; //modifier added to random number to determine if an attack will result in a hit or miss
    [SerializeField] private float dodge = 2; //modifier added to enemy's chance to hit to etermine if enemy attack will result in hit or miss
    [SerializeField] private float staminaCost = 0; //modifier that reduces stamina cost of abilities or spells
    [SerializeField] private float meleeDamage = 0; //damage of melee weapon attack
    [SerializeField] private float rangedDamage = 0; //damage of ranged weapon attack
    [SerializeField] private float spellDamage = 0; //damage of spell cast
    [SerializeField] private float carryWeightCapacity = 15; //max cumulative value of item's weight, if exceeded the player cannot move

    [Header("Ability Lists")]
    public List<string> unlockedAbilities = new List<string>(); //list of abilities unlocked through skill levels

    [SerializeField]
    private string[] lockedAbilities = //array containing all abilities
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

    private void Start()
    {
        skillPointPool = 3; //setting the base value of skill points for character creation
        playGameButton.interactable = false; //the playGameButton can only be interactable if character ceration is complete (name and portrati chosen and skill points distributed)

        PrintCharacterStats();
        CheckIfAddRemoveSkillButtonsShouldBeInteractable();
        PrintSkillPointPoolOnText();

        for (int i = 0; i < 6; i++)
        {
            PrintSkillLevelOnText(i);
        }

        PrintCharacterNameOnLoadButtons();
    }

    //setting the saveFile variable value
    public void ChooseSaveFileBeforeCharacterCreation(int saveFileIndex)
    {
        saveFile = saveFileIndex;
        PlayerPrefs.SetInt("ActiveSaveFile", saveFile);
    }

    //setting the player character name on load button text for appropriate saveFile
    public void PrintCharacterNameOnLoadButtons()
    {
        if (PlayerPrefs.HasKey("PlayerName1"))
        {
            chooseSave1Text.text = $"Save 1 {PlayerPrefs.GetString("PlayerName1")}";
        }
        else
        {
            chooseSave1Text.text = "Save 1";
        }
        if (PlayerPrefs.HasKey("PlayerName2"))
        {
            chooseSave2Text.text = $"Save 2 {PlayerPrefs.GetString("PlayerName2")}";
        }
        else
        {
            chooseSave2Text.text = "Save 2";
        }
        if (PlayerPrefs.HasKey("PlayerName3"))
        {
            chooseSave3Text.text = $"Save 3 {PlayerPrefs.GetString("PlayerName3")}";
        }
        else
        {
            chooseSave3Text.text = "Save 3";
        }
    }

    //display current amount of skill points still available on UI
    private void PrintSkillPointPoolOnText()
    {
        skillPointPoolText.text = "Skill Points: " + skillPointPool.ToString();
    }

    //display skill level value on UI durring character creation
    private void PrintSkillLevelOnText(int skillIndex)
    {
        skillsLevelText[skillIndex].text = skillsLevel[skillIndex].ToString();
    }

    //display appropriate values on UI
    private void PrintCharacterStats()
    {
        string statsText = $"Health: {maxHealth}\n" +
            $"\nStamina: {maxStamina}\n" +
            $"\nDodge: {dodge}\n" +
            $"\nHit Chance: {hitChance * 100}%\n" +
            $"\nStamina Cost: -{staminaCost * 100}%\n" +
            $"\nDamage: {meleeDamage}(M) {rangedDamage}(R) {spellDamage}(S)\n" +
            $"\nMax Carry Weight: {carryWeightCapacity}\n" +
            $"\nAbilities: {CheckWhatAbilityIsUnlocked()}";

        statsValuesText.text = statsText;
    }

    //for adding appropriate stat changes depending on what skill is being increased for different skill levels
    private void AddSkillLevelStatChange(int skillIndex, int skillLevel)
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

        if (hitChance >= 0 && hitChance < 0.1f)
        {
            hitChance = 0; //fixed a bug with value not reseting to 0
        }
        if (staminaCost >= 0 && staminaCost < 0.05f)
        {
            staminaCost = 0; //fixed a bug with value not reseting to 0
        }
        else if (staminaCost >= 0.05f && staminaCost < 0.1f)
        {
            staminaCost = 0.05f;
        }
    }

    //for subtracting appropriate stat changes depending on what skill is being decreased for different skill levels
    private void RemoveSkillLevelStatChange(int skillIndex, int skillLevel)
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
        else if (skillLevel == 2) //values for skill levels going from 3 to 2
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

        if (hitChance >= 0 && hitChance < 0.1f)
        {
            hitChance = 0; //fixed a bug with value not reseting to 0
        }
        if (staminaCost >= 0 && staminaCost < 0.05f)
        {
            staminaCost = 0; //fixed a bug with value not reseting to 0
        }
        else if (staminaCost >= 0.05f && staminaCost < 0.1f)
        {
            staminaCost = 0.05f;
        }
    }

    //for adding appropriate ability for each skill on a certain level of said skill
    private void AddAbilityOnSkillChange(int skillIndex, int skillLevel)
    {
        if (skillLevel == 2)
        {
            switch (skillIndex)
            {
                case 0:
                    unlockedAbilities.Add(lockedAbilities[0]);
                    dodge += 2; break;
                case 1:
                    unlockedAbilities.Add(lockedAbilities[2]); break;
                case 2:
                    unlockedAbilities.Add(lockedAbilities[4]); break;
                case 3:
                    unlockedAbilities.Add(lockedAbilities[6]); break;
                case 4:
                    unlockedAbilities.Add(lockedAbilities[8]); break;
                case 5:
                    unlockedAbilities.Add(lockedAbilities[10]);
                    carryWeightCapacity += 15; break;
            }
        }

        else if (skillLevel == 4)
        {
            switch (skillIndex)
            {
                case 0:
                    unlockedAbilities.Add(lockedAbilities[1]); break;
                case 1:
                    unlockedAbilities.Add(lockedAbilities[3]); break;
                case 2:
                    unlockedAbilities.Add(lockedAbilities[5]); break;
                case 3:
                    unlockedAbilities.Add(lockedAbilities[7]); break;
                case 4:
                    unlockedAbilities.Add(lockedAbilities[9]); break;
                case 5:
                    unlockedAbilities.Add(lockedAbilities[11]); break;
            }
        }
    }

    //for removing appropriate ability for each skill on a certain level of said skill
    private void RemoveAbilityOnSkillChange(int skillIndex, int skillLevel)
    {
        if (skillLevel == 1)
        {
            switch (skillIndex)
            {
                case 0:
                    unlockedAbilities.Remove(lockedAbilities[0]);
                    dodge -= 2; break;
                case 1:
                    unlockedAbilities.Remove(lockedAbilities[2]); break;
                case 2:
                    unlockedAbilities.Remove(lockedAbilities[4]); break;
                case 3:
                    unlockedAbilities.Remove(lockedAbilities[6]); break;
                case 4:
                    unlockedAbilities.Remove(lockedAbilities[8]); break;
                case 5:
                    unlockedAbilities.Remove(lockedAbilities[10]);
                    carryWeightCapacity -= 15; break;
            }
        }

        else if (skillLevel == 3)
        {
            switch (skillIndex)
            {
                case 0:
                    unlockedAbilities.Remove(lockedAbilities[1]); break;
                case 1:
                    unlockedAbilities.Remove(lockedAbilities[3]); break;
                case 2:
                    unlockedAbilities.Remove(lockedAbilities[5]); break;
                case 3:
                    unlockedAbilities.Remove(lockedAbilities[7]); break;
                case 4:
                    unlockedAbilities.Remove(lockedAbilities[9]); break;
                case 5:
                    unlockedAbilities.Remove(lockedAbilities[11]); break;
            }
        }
    }

    //returns a string of all unlocked abilities, used for displaying unlocked abilities on UI
    private string CheckWhatAbilityIsUnlocked()
    {
        if (unlockedAbilities.Count != 0)
        {
            string printedAbilities = "";
            foreach (string ability in unlockedAbilities)
            {
                if (printedAbilities != "")
                {
                    printedAbilities += ", " + ability;
                }
                else
                {
                    printedAbilities += ability;
                }
            }
            return printedAbilities;
        }
        else
        {
            return null;
        }
    }

    //method called on button click, adds skill level for appropriate abillity, updates UI and checks button interactability
    public void AddSkillLevelOnButtonClick(int skillIndex)
    {
        if (skillPointPool > 0)
        {
            skillsLevel[skillIndex]++;
            PrintSkillLevelOnText(skillIndex);

            skillPointPool--;
            PrintSkillPointPoolOnText();
        }

        AddSkillLevelStatChange(skillIndex, skillsLevel[skillIndex]);

        AddAbilityOnSkillChange(skillIndex, skillsLevel[skillIndex]);

        PrintCharacterStats();

        CheckIfAddRemoveSkillButtonsShouldBeInteractable();

        CheckIfCharacterCreationIsComplete();
    }

    //method called on button click, subtracts skill level for appropriate ability, updates UI and checks button interactability
    public void RemoveSkillLevelOnButtonClick(int skillIndex)
    {
        skillsLevel[skillIndex]--;
        PrintSkillLevelOnText(skillIndex);

        skillPointPool++;
        PrintSkillPointPoolOnText();

        RemoveSkillLevelStatChange(skillIndex, skillsLevel[skillIndex]);

        RemoveAbilityOnSkillChange(skillIndex, skillsLevel[skillIndex]);

        PrintCharacterStats();

        CheckIfAddRemoveSkillButtonsShouldBeInteractable();

        CheckIfCharacterCreationIsComplete();
    }

    //method used for checking and enabling buttons for adding skill levels if there are skill points to be spent
    private void EnableAddSkillButtons()
    {
        if (skillPointPool > 0)
        {
            for (int i = 0; i < addSkillLevelButtons.Length; i++)
            {
                addSkillLevelButtons[i].interactable = true;
            }
        }
    }

    //method used for checking and disabling buttons for adding skill levels if there are no skill points to be spent
    private void DisableAddSkillButtons()
    {
        if (skillPointPool == 0)
        {
            for (int i = 0; i < addSkillLevelButtons.Length; i++)
            {
                addSkillLevelButtons[i].interactable = false;
            }
        }
    }

    //method used for enabling buttons for subtracting skill levels if there are skill points spent in said skill
    private void EnableRemoveSkillButtons()
    {
        for (int i = 0; i < skillsLevel.Length; i++)
        {
            if (skillsLevel[i] > 0)
            {
                removeSkillLevelButtons[i].interactable = true;
            }
        }
    }

    //method used for disabling buttons for subtracting skill levels if there are no skill points spent in said skill
    private void DisableRemoveSkillButtons()
    {
        for (int i = 0; i < skillsLevel.Length; i++)
        {
            if (skillsLevel[i] == 0)
            {
                removeSkillLevelButtons[i].interactable = false;
            }
        }
    }

    //checks and sets add and remove skill level buttons to interactable or not interactable depending on skill point pool
    private void CheckIfAddRemoveSkillButtonsShouldBeInteractable()
    {
        EnableAddSkillButtons();
        EnableRemoveSkillButtons();
        DisableAddSkillButtons();
        DisableRemoveSkillButtons();
    }

    //saving player input for character name in playerName variable and checking if character creation is complete, must be placed on the input field OnEndEdit
    public void InputCharacterName()
    {
        playerName = nameInput.text;

        CheckIfCharacterCreationIsComplete();
    }

    //saving the portrait index of chosen portrait in the chosenPortraitIndex variable and checking if character creation is complete, must be placed on portrait buttons
    public void ChooseCharacterPortrait(int portraitIndex)
    {
        chosenPortraitIndex = portraitIndex;

        CheckIfCharacterCreationIsComplete();
    }

    //activates appropriate border for chosen portrait, so that the player can see what portrait is chosen and deactivates the other portrait borders
    public void ActivateDeactivatePortraitBorder(int portraitIndex)
    {
        for (int i = 0; i < portraitBorders.Length; i++)
        {
            if (i != portraitIndex)
            {
                portraitBorders[i].SetActive(false);
            }
            else
            {
                portraitBorders[i].SetActive(true);
            }
        }
    }

    //sets the playGameButton to interactable if all conditions are met, otherwise sets it to not interactable
    private void CheckIfCharacterCreationIsComplete()
    {
        if (skillPointPool == 0 && nameInput.text != "" && chosenPortraitIndex <= 3 && chosenPortraitIndex >= 0)
        {
            playGameButton.interactable = true;
        }
        else
        {
            playGameButton.interactable = false;
        }
    }

    //saves all values from character creation in player prefs with the saveFile value added to the key, to allow for multiple saves
    private void SaveCharacterCreationValues()
    {
        PlayerPrefs.SetString("PlayerName" + saveFile, playerName);
        PlayerPrefs.SetInt("PortraitIndex" + saveFile, chosenPortraitIndex);
        PlayerPrefs.SetFloat("Health" + saveFile, 10f);
        PlayerPrefs.SetFloat("MaxHealth" + saveFile, maxHealth);
        PlayerPrefs.SetFloat("Stamina" + saveFile, 10f);
        PlayerPrefs.SetFloat("MaxStamina" + saveFile, maxStamina);
        PlayerPrefs.SetFloat("HitChance" + saveFile, hitChance);
        PlayerPrefs.SetFloat("AttackTimer" + saveFile, 0f);
        PlayerPrefs.SetFloat("StaminaCost" + saveFile, staminaCost);
        PlayerPrefs.SetFloat("Dodge" + saveFile, dodge);
        PlayerPrefs.SetFloat("Protection" + saveFile, 0f);
        PlayerPrefs.SetFloat("PoisonResistance" + saveFile, 0f);
        PlayerPrefs.SetFloat("FireResistance" + saveFile, 0f);
        PlayerPrefs.SetFloat("MentalResistance" + saveFile, 0f);
        PlayerPrefs.SetFloat("CarryWeight" + saveFile, 0);
        PlayerPrefs.SetFloat("CarryWeightCapacity" + saveFile, carryWeightCapacity);
        PlayerPrefs.SetInt("Experience" + saveFile, 0);
        PlayerPrefs.SetInt("Level" + saveFile, 1);
        PlayerPrefs.SetInt("SkillPoints" + saveFile, skillPointPool);
        PlayerPrefs.SetInt("AthleticsLevel" + saveFile, skillsLevel[0]);
        PlayerPrefs.SetInt("MeleeLevel" + saveFile, skillsLevel[1]);
        PlayerPrefs.SetInt("MentalityLevel" + saveFile, skillsLevel[2]);
        PlayerPrefs.SetInt("RangedLevel" + saveFile, skillsLevel[3]);
        PlayerPrefs.SetInt("SorceryLevel" + saveFile, skillsLevel[4]);
        PlayerPrefs.SetInt("StrengthLevel" + saveFile, skillsLevel[5]);

        foreach(string ability in unlockedAbilities)
        {
            PlayerPrefs.SetString(ability + saveFile, ability);
        }
    }

    //method that saves character creation stat values and loads the first level when character creation is complete, called on button click, 
    public void FinnishCharacterCreation()
    {
        SaveCharacterCreationValues();
        SceneManager.LoadScene(1);
        //SceneManager.LoadSceneAsync(1);
    }
}
