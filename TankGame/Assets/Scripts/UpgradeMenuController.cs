using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
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
                    bestTime = float.Parse(reader.ReadLine());
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
                    fireRate = float.Parse(reader.ReadLine());
                }
                else if (line == "damage")
                {
                    damage = int.Parse(reader.ReadLine());
                }
            }
        }
    }

    public int getCash()
    {
        return cash;
    }
}
