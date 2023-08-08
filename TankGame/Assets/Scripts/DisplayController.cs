using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DisplayController : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverPanel;

    [SerializeField]
    private float bestTime;
    [SerializeField]
    private float currentTime;

    [SerializeField]
    private int actualScore;
    [SerializeField]
    private int bestScore;

    //UI
    [Header("Time")]
    [SerializeField]
    private Text timeText;
    [SerializeField]
    private GameObject newTimeRecord;
    [SerializeField]
    private GameObject timerPanelObj;

    [Header("Score")]
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private GameObject newHighScore;
    [SerializeField]
    private GameObject panelScoreObj;

    [Header("Cash")]
    [SerializeField]
    private Text cashWonText;

    [Header("In Game")]
    [SerializeField]
    private Text inGameScoreText;
    [SerializeField]
    private Text inGameTimeText;

    [Header("Life Bar")]
    [SerializeField]
    private GameObject lifeBarObject;
    [SerializeField]
    private Image lifeBar;
    [SerializeField]
    private int maxPlayerHP;
    bool playerHasSpawned;

    void Start()
    {
        gameOverPanel.SetActive(false);
        // maxPlayerHP = GameObject.Find("GameController").GetComponent<GameController>().getPlayerHP();
        string filePath = Application.dataPath + "/save.txt";
        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                Debug.Log(line);
                if (line == "HP")
                {
                    maxPlayerHP = int.Parse(reader.ReadLine());
                }
            }
        }
    }

    void Update()
    {
        LifeBar();

        if (GameObject.Find("GameController").GetComponent<GameController>().getGameOver())
        {
            panelScoreObj.SetActive(false);
            timerPanelObj.SetActive(false);
            lifeBarObject.SetActive(false);
            //inGameTimeText.text = "";
            //inGameScoreText.text = "";

            getPlayerVals();
            gameOverPanel.SetActive(true);

            timeText.text = "Time survived: " + currentTime.ToString("F2") + "\nBest Time: " + bestTime.ToString("F2");
            scoreText.text = "Score: " + actualScore.ToString() + "\nBest score: " + bestScore.ToString();

            cashWonText.text = "Cash: +$" + (actualScore / 10).ToString();

            if(actualScore >= bestScore)
            {
                newHighScore.SetActive(true);
            }
            else
            {
                newHighScore.SetActive(false);
            }

            if(currentTime >= bestTime)
            {
                newTimeRecord.SetActive(true);
            }
            else
            {
                newTimeRecord.SetActive(false);
            }

        }
        else
        {
            float timer = GameObject.Find("GameController").GetComponent<GameController>().getPlayerTimeInfo()[0];
            int score = GameObject.Find("GameController").GetComponent<GameController>().getPlayerScoreInfo()[0];

            inGameTimeText.text = timer.ToString("F2");
            inGameScoreText.text = "score: " + score.ToString();
        }
    }

    private void getPlayerVals()
    {
        currentTime = GameObject.Find("GameController").GetComponent<GameController>().getPlayerTimeInfo()[0];
        bestTime = GameObject.Find("GameController").GetComponent<GameController>().getPlayerTimeInfo()[1];

        actualScore = GameObject.Find("GameController").GetComponent<GameController>().getPlayerScoreInfo()[0];
        bestScore = GameObject.Find("GameController").GetComponent<GameController>().getPlayerScoreInfo()[1];
    }

    private void LifeBar()
    {
        int acutalHP = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerStatus>().getPlayerHP();
        float HP = (float)acutalHP / (float)maxPlayerHP;

        lifeBar.fillAmount = HP;
    }
}
