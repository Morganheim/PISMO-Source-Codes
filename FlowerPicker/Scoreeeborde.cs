using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreeeborde : MonoBehaviour
{
    public float[] scores;
    public string[] playerNames;
    public Text[] scoreTexts;
    public Text[] playerTexts;

    public float activeScore;
    public string activeName;
    bool newScore = false;

    private void Start()
    {
        SetArrays();
    }

    public void SetArrays()
    {
        for (int i = 0; i < 10; i++)
        {
            scores[i] = PlayerPrefs.GetFloat("Score" + i);
            playerNames[i] = PlayerPrefs.GetString("Name" + i);
        }
        SetUpUI();
    }

    public void SetUpUI()
    {
        for (int i = 0; i < scores.Length; i++)
        {
            scoreTexts[i].text = scores[i].ToString();
            playerTexts[i].text = playerNames[i];
        }
    }

    public void SaveNewScore()
    {
        int spermljeniZapis = 0;

        if (activeScore > scores[9])
        {
            if (activeScore > scores[8])
            {
                if (activeScore > scores[7])
                {
                    if (activeScore > scores[6])
                    {
                        if (activeScore > scores[5])
                        {
                            if (activeScore > scores[4])
                            {
                                if (activeScore > scores[3])
                                {
                                    if (activeScore > scores[2])
                                    {
                                        if (activeScore > scores[1])
                                        {
                                            if (activeScore > scores[0])
                                            {
                                                spermljeniZapis = 0;
                                            }
                                            else
                                            {
                                                spermljeniZapis = 1;
                                            }
                                        }
                                        else
                                        {
                                            spermljeniZapis = 2;
                                        }
                                    }
                                    else
                                    {
                                        spermljeniZapis = 3;
                                    }
                                }
                                else
                                {
                                    spermljeniZapis = 4;
                                }
                            }
                            else
                            {
                                spermljeniZapis = 5;
                            }
                        }
                        else
                        {
                            spermljeniZapis = 6;
                        }
                    }
                    else
                    {
                        spermljeniZapis = 7;
                    }
                }
                else
                {
                    spermljeniZapis = 8;
                }
            }
            else
            {
                spermljeniZapis = 9;
            }
            newScore = true;
        }
        else if (activeScore < scores[9])
        {
            newScore = false;
        }

        if (newScore == true)
        {
            for (int i = scores.Length - 1; i >= 0; i--)
            {
                if (i == spermljeniZapis)
                {
                    PlayerPrefs.SetFloat("Score" + i, activeScore);
                    PlayerPrefs.SetString("Name" + i, activeName);
                }
                else if (i > spermljeniZapis)
                {
                    PlayerPrefs.SetFloat("Score" + i, PlayerPrefs.GetFloat("Score" + (i - 1)));
                    PlayerPrefs.SetString("Name" + i, PlayerPrefs.GetString("Name" + (i - 1)));
                }
            }
            newScore = false;
        }
    }
}
