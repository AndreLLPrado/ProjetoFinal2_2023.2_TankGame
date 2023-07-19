using System.Collections;
using System.Collections.Generic;
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
    [SerializeField]
    private Text timeText;

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text inGameScoreText;
    [SerializeField]
    private Text inGameTimeText;

    [SerializeField]
    private GameObject lifeBarObject;
    [SerializeField]
    private Image lifeBar;
    [SerializeField]
    private int maxPlayerHP;

    void Start()
    {
        gameOverPanel.SetActive(false);
        maxPlayerHP = GameObject.Find("Player").GetComponent<PlayerStatus>().getPlayerHP();
    }

    void Update()
    {
        LifeBar();

        if (GameObject.Find("GameController").GetComponent<GameController>().getGameOver())
        {
            lifeBarObject.SetActive(false);
            inGameTimeText.text = "";
            inGameScoreText.text = "";

            getPlayerVals();
            gameOverPanel.SetActive(true);

            timeText.text = "Time survived: " + currentTime.ToString("F2") + "\nBest Time: " + bestTime.ToString("F2");
            scoreText.text = "Score: " + actualScore.ToString() + "\nBest score: " + bestScore.ToString();

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
        int acutalHP = GameObject.Find("Player").GetComponent<PlayerStatus>().getPlayerHP();

        float HP = (float)acutalHP / (float)maxPlayerHP * 1f;
        lifeBar.fillAmount = HP;
    }
}
