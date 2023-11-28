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
    [SerializeField]
    private RectTransform lifeBarBgObj;
    [SerializeField]
    private RectTransform lifeBarObj;
    [SerializeField]
    private RectTransform lifeBarMolduraObj;

    [Header("Mega Tank Timer")]
    [SerializeField]
    private Image megaTankTimer;
    [SerializeField]
    private GameObject megaTankTimerPanel;
    [SerializeField]
    private Text megaTankText;

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
        MegaTankTimerControl();

        if (GameObject.Find("GameController").GetComponent<GameController>().getGameOver())
        {
            panelScoreObj.SetActive(false);
            timerPanelObj.SetActive(false);
            lifeBarObject.SetActive(false);
            megaTankTimerPanel.SetActive(false);
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
        // moldura 1, barra 1.72 - vida máxima
        // moldura 0.5, barra 0.86 - vida min.

        //bar progression size
        float normalizedValue = Mathf.Clamp01((maxPlayerHP - 5f) / (15f - 5f));
        float scale = Mathf.Lerp(0.5f, 1f, normalizedValue);
        float scaleBar = Mathf.Lerp(0.86f, 1.72f, normalizedValue);

        float pos = Mathf.Lerp(-100f, 0f, normalizedValue);
        float posBar = Mathf.Lerp(-100f, 0.38f, normalizedValue);

        lifeBarMolduraObj.localScale = new Vector3(scale, 1f, 1f);
        lifeBarBgObj.localScale = new Vector3(scale, 1f, 1f);
        lifeBarObj.localScale = new Vector3(scaleBar, 1f, 1f);

        lifeBarMolduraObj.transform.localPosition = new Vector3(pos, 1f, 1f);
        lifeBarBgObj.transform.localPosition = new Vector3(pos, 1f, 1f);
        lifeBarObj.transform.localPosition = new Vector3(posBar, 1f, 1f);

        //bar controll
        int acutalHP = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerStatus>().getPlayerHP();
        float HP = (float)acutalHP / (float)maxPlayerHP;

        lifeBar.fillAmount = HP;
    }

    private void MegaTankTimerControl()
    {
        if(GameObject.Find("GameController").GetComponent<GameController>().getMegaTankHasSpawned())
        {
            megaTankTimerPanel.SetActive(false);
        }
        else
        {
            megaTankTimerPanel.SetActive(true);
            float time = GameObject.Find("GameController").GetComponent<GameController>().getMegaTankTimerToSpawn();
            float fill = time / 60;
            megaTankTimer.fillAmount = fill;
        }
    }
}
