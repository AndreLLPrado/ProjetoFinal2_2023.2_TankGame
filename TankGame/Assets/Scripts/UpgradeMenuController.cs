using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

public class UpgradeMenuController : MonoBehaviour
{
    [SerializeField]
    private int cash;
    [SerializeField]
    private int HP;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float fireRate;
    [SerializeField]
    private int damage;

    //for debug
    [SerializeField]
    private int highScore;
    [SerializeField]
    private float bestTime;

    [SerializeField]
    private int[] skillLevel;

    [SerializeField]
    private int[] skillCost;

    private void Start()
    {
        skillLevel = new int[4];
        skillCost = new int[4];

        skillCost[0] = 10; // Speed
        skillCost[1] = 10; // HP
        skillCost[2] = 10; // Fire Rate
        skillCost[3] = 10; // damage

        loadGame();
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
            skillCost[0] = cost * 10;
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
            skillCost[1] = cost * 10;
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
            skillCost[2] = cost * 10;
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
            skillCost[3] = cost * 10;
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
