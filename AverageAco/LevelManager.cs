using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    int level1Passed;
    int level2Passed;
    int level3Passed;
    int level4Passed;

    int checkPointSave;

    [SerializeField]
    private GameObject checkPoint;

    public GameObject[] nextLevelButtons;

    public GameObject[] questionmarks;

    public GameObject secretLevelButtonOnLevelSelectPanel;

    public GameObject secretLevelQuestionmark;

    [SerializeField]
    private GameObject player;
    [SerializeField] GameObject cam;

    private void Start()
    {
        CheckCompletedLevelsForAllPlaythroughs();
    }

    public void CheckCompletedLevelsForAllPlaythroughs()
    {
        for (int i = 0; i < 3; i++)
        {
            if (PlayerPrefs.GetInt($"Level{i+1}Passed") == 1)
            {
                nextLevelButtons[i].SetActive(true);
                questionmarks[i].SetActive(false);
            }
            else
            {
                nextLevelButtons[i].SetActive(false);
                questionmarks[i].SetActive(true);
            }
        }

        CheckIfSecretLevelIsAvailable();

        if (PlayerPrefs.GetInt("SecretLevelUnlocked") == 1)
        {
            secretLevelButtonOnLevelSelectPanel.SetActive(true);
            secretLevelQuestionmark.SetActive(false);
        }
        else
        {
            secretLevelButtonOnLevelSelectPanel.SetActive(false);
            secretLevelQuestionmark.SetActive(true);
        }
    }

    public void LevelPassed()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            level1Passed = 1;
            PlayerPrefs.SetInt("Level1Passed", level1Passed);
            PlayerPrefs.SetInt("SinglePlayLevel1Passed", level1Passed);
            Debug.Log($"Level {SceneManager.GetActiveScene().buildIndex} passed");
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            level2Passed = 1;
            PlayerPrefs.SetInt("Level2Passed", level2Passed);
            PlayerPrefs.SetInt("SinglePlayLevel2Passed", level2Passed);
            Debug.Log($"Level {SceneManager.GetActiveScene().buildIndex} passed");
        }
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            level3Passed = 1;
            PlayerPrefs.SetInt("Level3Passed", level3Passed);
            PlayerPrefs.SetInt("SinglePlayLevel3Passed", level3Passed);
            Debug.Log($"Level {SceneManager.GetActiveScene().buildIndex} passed");
        }
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            level4Passed = 1;
            PlayerPrefs.SetInt("Level4Passed", level4Passed);
            PlayerPrefs.SetInt("SinglePlayLevel4Passed", level4Passed);
            Debug.Log($"Level {SceneManager.GetActiveScene().buildIndex} passed");
        }

        CheckCompletedLevelsForAllPlaythroughs();
    }

    private void CheckIfSecretLevelIsAvailable()
    {
        if (PlayerPrefs.GetInt("LevelCounter") >= 4)
        {
            int collectedCoins = 0;

            int maxCoinsSum = 0;

            int[] maxCoins = GetComponent<CoinManager>().maxCoins;

            for (int i = 0; i < maxCoins.Length; i++)
            {
                collectedCoins += PlayerPrefs.GetInt($"Level{i + 1}Coins");
                maxCoinsSum += maxCoins[i];
            }

            if (collectedCoins >= maxCoinsSum)
            {
                PlayerPrefs.SetInt("SecretLevelUnlocked", 1);
            }
        }
    }

    public void LoadNextLevel(int level)
    {
        Debug.Log(level);
        checkPointSave = 0;
        PlayerPrefs.SetInt("CheckPointSave", checkPointSave);
        SceneManager.LoadScene(level);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        LoadPlayerPositionAtCheckPoint();
    }

    public void SavePositionAtCheckpoint()
    {
        checkPointSave = 1;
        PlayerPrefs.SetInt("CheckPointSave", checkPointSave);

        PlayerPrefs.SetFloat("Position_X", checkPoint.transform.position.x);
        PlayerPrefs.SetFloat("Position_Y", checkPoint.transform.position.y);

        Debug.Log("spremi poziciju checkpointa");
    }

    public void LoadPlayerPositionAtCheckPoint()
    {
        if (PlayerPrefs.GetInt("CheckPointSave") != 0)
        {
            player.transform.position = new Vector2(PlayerPrefs.GetFloat("Position_X"), PlayerPrefs.GetFloat("Position_Y"));
            cam.transform.position = new Vector2(PlayerPrefs.GetFloat("Position_X"), PlayerPrefs.GetFloat("Position_Y"));
        }
    }
}
