using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Globalization;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;

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

    //player status
    private int HP;
    [SerializeField] private float speed;
    [SerializeField] private float fireRate;
    private int damage;

    private void Start()
    {
        save = false;
        aux = timer;
        aux2 = spawnEnemyTime;

        // saveGame();
        loadGame();
        Instantiate(playerPrefab, new Vector3(0f, 1.21f, 0f), Quaternion.identity);
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
                cash += calculateCash();
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
            writer.WriteLine(cash.ToString());
            writer.WriteLine("HP");
            writer.WriteLine(HP.ToString());
            writer.WriteLine("speed");
            writer.WriteLine(speed.ToString());
            writer.WriteLine("fireRate");
            writer.WriteLine(fireRate.ToString());
            writer.WriteLine("damage");
            writer.WriteLine(damage.ToString());
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
                    string valueString = reader.ReadLine();
                    float.TryParse(valueString, NumberStyles.Float, CultureInfo.InvariantCulture, out bestTime);
                }
                else if(line == "cash")
                {
                    cash = int.Parse(reader.ReadLine());
                }
                else if (line == "HP")
                {
                    HP = int.Parse(reader.ReadLine());
                }
                else if( line == "speed")
                {
                    string valueString = reader.ReadLine();
                    float.TryParse(valueString, NumberStyles.Float, CultureInfo.InvariantCulture, out speed);
                }
                else if(line == "fireRate")
                {
                    string valueString = reader.ReadLine();
                    float.TryParse(valueString, NumberStyles.Float, CultureInfo.InvariantCulture, out fireRate);
                }
                else if (line == "damage")
                {
                    damage = int.Parse(reader.ReadLine());
                }
            }
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("game");
    }

    private int calculateCash()
    {
        int cash = score / 10;
        return cash;
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

    public float getPlayerSpeed()
    {
        return speed;
    }

    public int getPlayerHP()
    {
        return HP;
    }

    public float getPlayerFireRate()
    {
        return fireRate;
    }

    public int getPlayerDamage()
    {
        return damage;
    }
}
