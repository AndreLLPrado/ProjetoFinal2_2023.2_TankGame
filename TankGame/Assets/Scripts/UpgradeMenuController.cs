using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

public class UpgradeMenuController : MonoBehaviour
{
    [SerializeField]
    private int cash = 0;
    [SerializeField]
    private int HP = 5;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float fireRate = 1.5f;
    [SerializeField]
    private int damage = 1;

    //for debug
    [SerializeField]
    private int highScore;
    [SerializeField]
    private float bestTime;

    [SerializeField]
    private int[] skillLevel;

    [SerializeField]
    private int[] skillCost;

    [SerializeField]
    private int[] skillCostProgression;

    private void Start()
    {
        skillLevel = new int[4];
        skillCost = new int[4];

        foreach(int level in skillLevel)
        {
            skillLevel[level] = 0;
        }

        skillCost[0] = skillCostProgression[0]; // Speed
        skillCost[1] = skillCostProgression[0]; // HP
        skillCost[2] = skillCostProgression[0]; // Fire Rate
        skillCost[3] = skillCostProgression[0]; // damage

        CheckAndCreateFile();
        loadGame();
    }

    void CheckAndCreateFile()
    {
        string filePath = Application.dataPath + "/save.txt"; // Caminho do arquivo a ser criado/aberto
        if (!File.Exists(filePath))
        {
            // Cria o arquivo se ele não existir
            using (StreamWriter writer = File.CreateText(filePath))
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
                    writer.WriteLine(skillLevel[0].ToString());
                }
                writer.WriteLine("skillCost");
                for (int i = 0; i < skillCost.Length; i++)
                {
                    writer.WriteLine(skillCost[0].ToString());
                }
            }

            Debug.Log("Arquivo criado em: " + filePath);
        }
        else
        {
            Debug.Log("Arquivo já existe em: " + filePath);
        }
    }
    public void loadGame()
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
                else if(line == "skillLevel")
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
            for(int i = 0; i < skillLevel.Length; i++)
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

    public void upgradeSpeed()
    {
        int cost = skillCost[0];
        if(cash >= cost && skillLevel[0] < 5)
        {
            speed += 2;
            cash -= cost;
            skillLevel[0]++;
            skillCost[0] = skillCostProgression[skillLevel[0]];
        }
    }

    public void upgradeHP()
    {
        int cost = skillCost[1];
        if (cash >= cost && skillLevel[1] < 5)
        {
            HP += 2;
            cash -= cost;
            skillLevel[1]++;
            skillCost[1] = skillCostProgression[skillLevel[1]];
        }
    }

    public void upgradeFR()
    {
        int cost = skillCost[2];
        if (cash >= cost && skillLevel[2] < 5)
        {
            fireRate -= 0.25f;
            cash -= cost;
            skillLevel[2]++;
            skillCost[2] = skillCostProgression[skillLevel[2]];
        }
    }

    public void upgradeDamage()
    {
        int cost = skillCost[3];
        if (cash >= cost && skillLevel[3] < 5)
        {
            damage++;
            cash -= cost;
            skillLevel[3]++;
            skillCost[3] = skillCostProgression[skillLevel[3]];
        }
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
}
