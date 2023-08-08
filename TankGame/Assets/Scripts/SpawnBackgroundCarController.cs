using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBackgroundCarController : MonoBehaviour
{
    [SerializeField]
    private GameObject carPrefab;
    [SerializeField]
    private float spawnDelay;
    private float aux;
    [SerializeField]
    private Transform[] spawnpoints;

    private void Start()
    {
        aux = spawnDelay;
    }
    private void Update()
    {
        spawnDelay -= Time.deltaTime;
        if(spawnDelay <= 0)
        {
            spawnDelay = aux;
            SpawnCar();
        }
    }

    void SpawnCar()
    {
        int point = Random.Range(0, spawnpoints.Length);
        GameObject car = Instantiate(carPrefab, spawnpoints[point].position, Quaternion.identity);
        if (point == 1 || point == 2)
        {
            car.GetComponent<BackgroundCar>().setRotaionY(-90f);
            car.GetComponent<BackgroundCar>().setDirection(new Vector3(0f, 0f, -1f));
        }
        else
        {
            car.GetComponent<BackgroundCar>().setRotaionY(90f);
            car.GetComponent<BackgroundCar>().setDirection(new Vector3(0f, 0f, 1f));
        }
    }
}
