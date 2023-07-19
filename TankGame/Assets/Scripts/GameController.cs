using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Time
    [SerializeField]
    private float timer; // time to difficulty increase
    private float aux;
    [SerializeField]
    private float bestTime;
    [SerializeField]
    private float currentTime;
    [SerializeField]
    private float spawnEnemyTime;
    private float aux2;

    // Score
    [SerializeField]
    private int score;
    [SerializeField]
    private int highScore;

    //cash
    [SerializeField]
    private int cash;

    //Logic
    [SerializeField]
    private bool gameOver;
    bool save;

    private void Start()
    {
        save = false;
        aux = timer;
        aux2 = spawnEnemyTime;

        // saveGame();
        loadGame();
    }

    private void Update()
    {
        if(!gameOver)
        {
            currentTime += Time.deltaTime;
            spawnEnemyTime -= Time.deltaTime;
            if(spawnEnemyTime < 0)
            {
                GetComponent<SpawnPicker>().SpawnEnemy();
                spawnEnemyTime = aux2;
            }

            // DifficulrtIncrease();
        }
        else
        {
            if (score >= highScore)
            {
                highScore = score;
            }

            if (currentTime >= bestTime)
            {
                bestTime = currentTime;
            }

            if(!save)
            {
                saveGame();
                save = true;
            }
        }
    }

    private void DifficulrtIncrease()
    {
        timer -= Time.deltaTime;
        if( timer <= 0)
        {
            //Difficulty increase

            timer = aux;
        }
    }

    public void AddPointsToScore(int pts)
    {
        score += pts;
    }

    private void saveGame()
    {
        string filePath = Application.dataPath + "/save.txt"; // Caminho do arquivo a ser criado/aberto

        // Escreve no arquivo
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("score");
            writer.WriteLine(highScore.ToString());
            writer.WriteLine("bestTime");
            writer.WriteLine(bestTime.ToString());
            writer.WriteLine("cash");
            writer.WriteLine("99999999");
        }

        Debug.Log("Arquivo criado/alterado e salvo em: " + filePath);
    }

    private void loadGame()
    {
        // Lê o arquivo
        string filePath = Application.dataPath + "/save.txt";
        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                Debug.Log(line);
                if(line == "score")
                {
                    highScore = int.Parse(reader.ReadLine());
                }
                else if(line == "bestTime")
                {
                    bestTime = float.Parse(reader.ReadLine());
                }
                else if(line == "cash")
                {
                    cash = int.Parse(reader.ReadLine());
                }
            }
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("game");
    }

    public void setGameOver(bool gOnver)
    {
        gameOver = gOnver;
    }

    public bool getGameOver()
    {
        return gameOver;
    }

    public float[] getPlayerTimeInfo()
    {
        float [] times = new float[2];
        times[0] = currentTime;
        times[1] = bestTime;
        return times;
    }

    public int[] getPlayerScoreInfo()
    {
        int [] scoreBoard = new int[2];
        scoreBoard[0] = score;
        scoreBoard[1] = highScore;
        return scoreBoard;
    }
}
