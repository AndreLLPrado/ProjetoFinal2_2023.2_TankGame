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
            SpawnEnemy();
        }
    }
    public void SpawnEnemy()
    {
        int position = Random.Range(0, spawnPoints.Length);
        Instantiate(Enemy, spawnPoints[position].position, Quaternion.identity);
        
    }
}
