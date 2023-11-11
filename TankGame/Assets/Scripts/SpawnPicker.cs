using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPicker : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawnPoints;

    [SerializeField]
    private GameObject[] Enemies;
    [SerializeField]
    private float seekerChance;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
           // SpawnEnemy();
        }
    }
    public void SpawnEnemy()
    {
        int difficulty = GameObject.Find("GameController").GetComponent<GameController>().getDifficultyLevel();

        if(difficulty > 1)
        {
            int spawn = 0;
            if(difficulty > 5)
            {
                difficulty = 5;
            }

            

            for (int i = 0; i < difficulty; i++)
            {
                if (difficulty > 2)
                {
                    float test = Random.Range(0, 100);

                    if (test <= seekerChance)
                    {
                        spawn = 1;
                    }
                    else
                    {
                        spawn = 0;
                    }
                }
                int position = Random.Range(0, spawnPoints.Length);
                Instantiate(Enemies[spawn], spawnPoints[position].position, Quaternion.identity);
            }
        }
        else
        {
            int position = Random.Range(0, spawnPoints.Length);
            Instantiate(Enemies[0], spawnPoints[position].position, Quaternion.identity);
        }
    }
}
