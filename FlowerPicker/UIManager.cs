using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    Scoreeeborde scbd;

    [Header("UI objects")]
    public Text timerText;
    public float score;

    [Header("Scoreboard Textovi")]
    public Text name1, name2, name3, name4, name5, name6, name7, name8, name9, name10;

    public Text score1, score2, score3, score4, score5, score6, score7, score8, score9, score10;

    [Header("Pcele")]
    public GameObject bee1;
    public GameObject bee2;

    public GameObject[] all;

    public Text playerTxt;
    public InputField playerInput;

    [Header("Time data - do not change")]
    public float elapsedTime;

    [Header("End game panel objects")]
    public GameObject endGamePanel;
    public Text endGameScoreText;
    public Text endGameVictoryText;
    public Image backgroundImage;

    public bool isGameOver = false;
    DestroyerMoveToMouse dmtm;

    MusicManager mm;

    private void Awake()
    {
        isGameOver = false;
    }

    private void Start()
    {
        scbd = FindObjectOfType<Scoreeeborde>();

        Time.timeScale = 0;

        dmtm = FindObjectOfType<DestroyerMoveToMouse>();
        mm = FindObjectOfType<MusicManager>();

        bee1.SetActive(false);
        bee2.SetActive(false);

        //LeaderBoardShow();

        score = 0;
        for (int i = 0; i < all.Length; i++)
        {
            all[i].SetActive(false);
            Time.timeScale = 0;
        }

        playerInput.text = PlayerPrefs.GetString("ActiveName");
        playerTxt.text = PlayerPrefs.GetString("ActiveName");
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        timerText.text = ((int)elapsedTime / 60).ToString("00:") + ((int)elapsedTime % 60).ToString("00");

        if (isGameOver == true)
        {
            Time.timeScale = 0;
            dmtm.canIClick = false;
        }
    }

    public void SetUpName()
    {
        if (playerInput.text != "")
        {
            PlayerPrefs.SetString("ActiveName", playerInput.text);
            playerTxt.text = PlayerPrefs.GetString("ActiveName");
            //PlayerPrefs.SetString("playerOne" + playerTxt.text, playerTxt.text);   
        }

        else
        {
            PlayerPrefs.SetString("ActiveName", "Player");
            playerTxt.text = PlayerPrefs.GetString("ActiveName");
        }
    }

    public void ShowEndGame()
    {
        score = Mathf.CeilToInt((30 / elapsedTime) * 10000);

        scbd.activeScore = score;
        scbd.activeName = playerTxt.text;

        scbd.SaveNewScore();
        scbd.SetArrays();
        scbd.SetUpUI();

        bee1.SetActive(false);
        bee2.SetActive(false);
        mm.mainAudioSource.Stop();
        mm.VictoryFanfare();

        //SaveScore(score, playerTxt.text, (((int)score / 60).ToString("00:") + ((int)score % 60).ToString("00")));
        endGamePanel.SetActive(true);
        endGamePanel.GetComponent<Image>().sprite = backgroundImage.sprite;
        endGameScoreText.text = "Your score is: " + score;
        endGameScoreText.color = Color.white;
        //endGameVictoryText.color = Color.white;
        endGameVictoryText.color = Color.cyan;
        endGameVictoryText.color = new Color(0.5f, 0.5f, 0.5f);
        //endGameVictoryText.color = new Color ()
    }

    public void PlayAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quitting game");
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }

    public void StartGame()
    {
        for (int i = 0; i < all.Length; i++)
        {
            all[i].SetActive(true);
        }
        bee1.SetActive(true);
        bee2.SetActive(true);
        elapsedTime = 0;
        Time.timeScale = 1;
    }
}