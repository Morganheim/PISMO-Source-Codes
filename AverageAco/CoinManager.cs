using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CoinManager : MonoBehaviour
{
    int allCoins;

    public int lvlCoins = 0;

    public int[] maxCoins = { 30, 49, 44, 48 };
    public int[] billCoins = { 22, 37, 33, 36 };

    public Text scoreText;
    public Text pauseScoreText;

    private void Start()
    {
        lvlCoins = 0;

        CoinsTextUpdate();
    }

    public void AddCoinsToAllCoinsCounter()
    {
        allCoins = 0;
        for (int i = 0; i < lvlCoins; i++)
        {
            allCoins += PlayerPrefs.GetInt($"Level{i + 1}Coins");
            PlayerPrefs.SetInt("AllCoins", allCoins);
        }
    }

    public void CoinCollect()
    {
        lvlCoins++;
        int level = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt($"Level{level}Coins", lvlCoins);

        CoinsTextUpdate();
    }

    private void CoinsTextUpdate()
    {
        scoreText.text = "Coins: " + lvlCoins.ToString();
        pauseScoreText.text = "Coins: " + lvlCoins.ToString();
    }
}
