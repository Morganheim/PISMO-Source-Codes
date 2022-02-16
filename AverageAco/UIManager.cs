using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Texts")]
    public Text level1EndText;
    public Text level2EndText;
    public Text level3EndText;
    public Text level4EndText;
    public Text secretLevelEndText;
    public Text gameEndText;

    [Header("Button Texts")]
    public Text level1EndButtonText;
    public Text level2EndButtonText;
    public Text level3EndButtonText;
    public Text level4EndButtonText;
    public Text resetLevelButtonText;

    public int levelCounter;

    public GameObject gameEndPanel;

    public GameObject secretLevelButtonOnGameEndPanel;

    private void Start()
    {
        levelCounter = PlayerPrefs.GetInt("LevelCounter");
    }

    public void LevelEnd(int coins, int maxCoins)
    {
        LevelEndTextUpdate(coins, maxCoins);

        LevelEndButtonTextUpdate();

        AddCompletedLevelToLevelCounter();

        CheckIfAllLevelsAreCompleted();

    }

    public void LevelEndTextUpdate(int coins, int maxCoins)
    {
        int level = SceneManager.GetActiveScene().buildIndex;

        if (level == 1)
        {
            level1EndText.text = $"You completed level {level} and collected {coins} coins out of {maxCoins} coins to pay for the electricity bill!";
        }
        else if (level == 2)
        {
            level2EndText.text = $"You completed level {level} and collected {coins} coins out of {maxCoins} coins to pay for the insurance bill!";
        }
        else if (level == 3)
        {
            level3EndText.text = $"You completed level {level} and collected {coins} coins out of {maxCoins} coins to pay for the gas bill!";
        }
        else if (level == 4)
        {
            level4EndText.text = $"You completed level {level} and collected {coins} coins out of {maxCoins} coins to pay for rent!";
        }
        else if (level == 5)
        {
            secretLevelEndText.text = $"You completed the secret level like a Balkan boss. Now Aco can go on vacation." +
                $"\nSee you there!";
        }
    }

    private void LevelEndButtonTextUpdate()
    {
        if (PlayerPrefs.GetInt("LevelCounter") >= 4)
        {
            level1EndButtonText.text = "Pay the Bills";
            level2EndButtonText.text = "Pay the Bills";
            level3EndButtonText.text = "Pay the Bills";
            level4EndButtonText.text = "Pay the Bills";
        }
        else
        {
            level1EndButtonText.text = "Next Level";
            level2EndButtonText.text = "Next Level";
            level3EndButtonText.text = "Next Level";
            level4EndButtonText.text = "Next Level";
        }
    }

    public void AddCompletedLevelToLevelCounter()
    {
        levelCounter = 0;
        for (int i = 0; i < 4; i++)
        {
            if(PlayerPrefs.GetInt($"SinglePlayLevel{i + 1}Passed") == 1)
            {
                levelCounter++;
                PlayerPrefs.SetInt("LevelCounter", levelCounter);
            }

        }

        if (PlayerPrefs.GetInt($"SinglePlayLevel{SceneManager.GetActiveScene().buildIndex}Passed") == 0)
        {
            levelCounter = PlayerPrefs.GetInt("LevelCounter");
            levelCounter++;
            PlayerPrefs.SetInt("LevelCounter", levelCounter);
        }
    }

    public void ResetLevelCounter()
    {
        levelCounter = 0;
        PlayerPrefs.SetInt("LevelCounter", levelCounter);
    }

    public void CheckIfAllLevelsAreCompleted()
    {
        if (PlayerPrefs.GetInt("LevelCounter") >= 4)
        {
            GameEnd(PlayerPrefs.GetInt("AllCoins"), 171, GetComponent<CoinManager>().billCoins[0], GetComponent<CoinManager>().billCoins[1], GetComponent<CoinManager>().billCoins[2], GetComponent<CoinManager>().billCoins[3], 128);
        }
    }

    public void GameEnd(int coins, int maxCoins, int bill1Coins, int bill2Coins, int bill3Coins, int bill4Coins, int goodEndingCoins)
    {
        gameEndPanel.SetActive(true);

        if(coins >= goodEndingCoins && coins <= maxCoins)
        {
            gameEndText.text = $"You completed all the levels and collected {coins} coins out of {maxCoins} coins." +
            $"\nAco needed {bill1Coins} coins to pay the electricity bill, {bill2Coins} coins to pay the insurance bill, {bill3Coins} to pay the gas bill and {bill4Coins} coins to pay the rent." +
            $"\nPrincess Yagoda is delighted. She happily stays with Aco and the two of them bone, pork and shag all night long! " +
            $"\nOh, happy day!";
        }
        else if (coins < goodEndingCoins)
        {
            gameEndText.text = $"You completed all the levels and collected {coins} coins out of {maxCoins} coins." +
            $"\nAco needed {bill1Coins} coins to pay the electricity bill, {bill2Coins} coins to pay the insurance bill, {bill3Coins} to pay the gas bill and {bill4Coins} coins to pay the rent." +
            $"\nHe failed." +
            $"\nPrincess Yagoda is furious. She says she should have listened to her mother and married Louis G. She leaves Aco in his cold, dark apartment feeling more alone than ever. " +
            $"\nWhat have you done...";
        }
        else if (coins > maxCoins)
        {
            gameEndText.text = $"You completed all the levels and collected {coins} coins out of {maxCoins} coins." +
                $"\nLike a true Balkan man, Aco managed to snag some coins he wasn't supposed to, but honestly, who cares? Everybody steals in the Balkans. It's a point of pride." +
                $"\nThe bills are no longer a problem, and Aco was even thinking of trading his Yugo 55 for a black Golf 2." +
                $"\nPrincess Yagoda is overjoyed. Not only does she stay with Aco but she brings her girlfriends over as well." +
                $"\nYou proved Aco is far from average and helped him to live like a king." +
                $"\nAt least for a little while.";
        }

        if (PlayerPrefs.GetInt("SecretLevelUnlocked") == 1)
        {
            secretLevelButtonOnGameEndPanel.SetActive(true);
        }
    }

    public void AcoDied()
    {
        int level = SceneManager.GetActiveScene().buildIndex;
        resetLevelButtonText.text = $"Play Level {level} Again";
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void ClearPlayerPrefsOnQuitGame()
    {
        for (int i = 0; i < 4; i++)
        {
            PlayerPrefs.SetInt($"Level{i + 1}Coins", 0);
        }
        PlayerPrefs.SetInt("AllCoins", 0);

        PlayerPrefs.SetInt("CheckPointSave", 0);
        PlayerPrefs.SetFloat("Position_X", 0);
        PlayerPrefs.SetFloat("Position_Y", 0);

        for (int i = 0; i < 4; i++)
        {
            PlayerPrefs.SetInt($"SinglePlayLevel{i+1}Passed", 0);
        }

        ResetLevelCounter();
    }

    public void QuitGame()
    {
        Debug.Log("Game Exit approved. Have a nice day.");
        ClearPlayerPrefsOnQuitGame();
        Application.Quit();
    }
}
