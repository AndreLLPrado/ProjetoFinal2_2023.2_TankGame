using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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

    private void Start()
    {
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
            //if(score >= highScore)
            //{
            //    highScore = score;
            //}

            //if(currentTime >= bestTime)
            //{
            //    bestTime = currentTime;
            //}

            // DifficulrtIncrease();
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

    public void setGameOver(bool gOnver)
    {
        gameOver = gOnver;
    }

    public bool getGameOver()
    {
        return gameOver;
    }
}
