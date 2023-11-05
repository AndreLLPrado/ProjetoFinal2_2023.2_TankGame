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
    [Header("Time controller")]
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

    //Mega Tank
    [Header("Mega Tank")]
    [SerializeField]
    private float spawnMegaTankTime;
    private float aux3;
    [SerializeField]
    private GameObject megaTankPrefab;
    [SerializeField]
    private bool megaTankHasSpawned;

    // Score
    [Header("Score controller")]
    [SerializeField]
    private int score;
    [SerializeField]
    private int highScore;

    [Header("Cash")]
    //cash
    [SerializeField]
    private int cash;

    //Logic
    [Header("Game Logic")]
    [SerializeField]
    private bool gameOver;
    bool save;
    [SerializeField]
    private int difficultyLevel = 0;
    [SerializeField]
    private bool maxDifficultyActivate;
    [SerializeField]
    private int bonusPoints;

    //player status
    [Header("Player Status")]
    private int HP;
    [SerializeField] private float speed;
    [SerializeField] private float fireRate;
    private int damage;

    [SerializeField]
    private int[] skillLevel;

    [SerializeField]
    private int[] skillCost;

    //others
    [Header("Others")]
    [SerializeField] private GameObject upgradeStore;
    private int MegaTankPowerIncrease;

    private void Start()
    {
        MegaTankPowerIncrease = 0;

        skillLevel = new int[4];
        skillCost = new int[4];

        save = false;
        aux = timer;
        aux2 = spawnEnemyTime;
        aux3 = spawnMegaTankTime;

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
                if(!megaTankHasSpawned)
                {
                    GetComponent<SpawnPicker>().SpawnEnemy();
                }
                spawnEnemyTime = aux2;
            }

            if(!maxDifficultyActivate && !megaTankHasSpawned)
            {
                DifficulrtIncrease();
            }

            SpawnMegaTank();
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
            difficultyLevel++;
            //if (difficultyLevel < 5)
            //{
            //    difficultyLevel++;
            //    score += bonusPoints;
            //}
            //else
            //{
            //    maxDifficultyActivate = true;
            //}

            timer = aux;
        }
    }

    private void SpawnMegaTank()
    {
        if(!megaTankHasSpawned)
        {
            spawnMegaTankTime -= Time.deltaTime;
            if( spawnMegaTankTime <= 0)
            {
                GameObject megaTank = Instantiate(megaTankPrefab, Vector3.zero, Quaternion.identity);
                megaTankHasSpawned = true;

                spawnMegaTankTime = aux3;
            }
        }
    }

    public void AddPointsToScore(int pts)
    {
        score += pts;
    }

    public void saveGame()
    {
        string filePath = Application.dataPath + "/save.txt"; // Caminho do arquivo a ser criado/aberto

        // Escreve no arquivo
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("score");
            writer.WriteLine(highScore.ToString());
            writer.WriteLine("bestTime");
            writer.WriteLine(bestTime.ToString(CultureInfo.InvariantCulture));
            writer.WriteLine("cash");
            writer.WriteLine(cash.ToString());
            writer.WriteLine("HP");
            writer.WriteLine(HP.ToString());
            writer.WriteLine("speed");
            writer.WriteLine(speed.ToString(CultureInfo.InvariantCulture));
            writer.WriteLine("fireRate");
            writer.WriteLine(fireRate.ToString(CultureInfo.InvariantCulture));
            writer.WriteLine("damage");
            writer.WriteLine(damage.ToString());
            writer.WriteLine("skillLevel");
            for (int i = 0; i < skillLevel.Length; i++)
            {
                writer.WriteLine(skillLevel[i].ToString());
            }
            writer.WriteLine("skillCost");
            for (int i = 0; i < skillCost.Length; i++)
            {
                writer.WriteLine(skillCost[i].ToString());
            }
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
                if (line == "score")
                {
                    highScore = int.Parse(reader.ReadLine());
                }
                else if (line == "bestTime")
                {
                    string valueString = reader.ReadLine();
                    float.TryParse(valueString, NumberStyles.Float, CultureInfo.InvariantCulture, out bestTime);
                    // bestTime = float.Parse(reader.ReadLine());
                }
                else if (line == "cash")
                {
                    cash = int.Parse(reader.ReadLine());
                }
                else if (line == "HP")
                {
                    HP = int.Parse(reader.ReadLine());
                }
                else if (line == "speed")
                {
                    speed = float.Parse(reader.ReadLine());
                }
                else if (line == "fireRate")
                {
                    string valueString = reader.ReadLine();
                    float.TryParse(valueString, NumberStyles.Float, CultureInfo.InvariantCulture, out fireRate);
                    // fireRate = float.Parse(reader.ReadLine());
                }
                else if (line == "damage")
                {
                    damage = int.Parse(reader.ReadLine());
                }
                else if (line == "skillLevel")
                {
                    for (int i = 0; i < skillLevel.Length; i++)
                    {
                        skillLevel[i] = int.Parse(reader.ReadLine());
                    }
                }
                else if (line == "skillCost")
                {
                    for (int i = 0; i < skillCost.Length; i++)
                    {
                        skillCost[i] = int.Parse(reader.ReadLine());
                    }
                }
            }
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("game");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("menu");
    }

    public void OpenCloseUpgradeStore()
    {
        bool active = upgradeStore.active;
        upgradeStore.SetActive(!active);
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

    public int getDifficultyLevel()
    {
        return difficultyLevel;
    }

    public void setMegaTankHasSpawned(bool s)
    {
        megaTankHasSpawned = s;
    }

    public bool getMegaTankHasSpawned()
    {
        return megaTankHasSpawned;
    }

    public int getCash()
    {
        return cash;
    }

    public int[] getSkillLevels()
    {
        return skillLevel;
    }

    public int[] getSkillCosts()
    {
        return skillCost;
    }

    public void setMegaTankPowerIncrease(int power)
    {
        MegaTankPowerIncrease += power;
    }

    public int getMegaTankPowerIncrease()
    {
        return MegaTankPowerIncrease;
    }

    public float getMegaTankTimerToSpawn()
    {
        return spawnMegaTankTime;
    }
}
