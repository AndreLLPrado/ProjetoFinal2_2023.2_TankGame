using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPicker : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawnPoints;

    [SerializeField]
    private GameObject Enemy;

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
            if(difficulty > 5)
            {
                difficulty = 5;
            }


            for (int i = 0; i < difficulty; i++)
            {
                int position = Random.Range(0, spawnPoints.Length);
                Instantiate(Enemy, spawnPoints[position].position, Quaternion.identity);
            }
        }
        else
        {
            int position = Random.Range(0, spawnPoints.Length);
            Instantiate(Enemy, spawnPoints[position].position, Quaternion.identity);
        }
    }
}
