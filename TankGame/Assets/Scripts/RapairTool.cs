using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapairTool : MonoBehaviour
{
    [Header("Config")]
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float duration;
    [SerializeField]
    private Light light;


    [Header("Cure force")]
    [SerializeField]
    private int minCure;

    [SerializeField]
    private int maxCure;
    private void Start()
    {
        light.intensity = duration;
    }

    private void Update()
    {
        transform.eulerAngles += new Vector3(0f, rotationSpeed * Time.deltaTime, 0f);
        duration -= Time.deltaTime;
        light.intensity -= Time.deltaTime;
        if (duration <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameObject.Find("Canvas").GetComponent<SFXPlayer>().RapairToolSFXPlay();
            int cure = Random.Range(minCure, maxCure);
            if (collision.gameObject.GetComponent<PlayerStatus>() != null) 
            {
                collision.gameObject.GetComponent<PlayerStatus>().cure(cure);
            }
            Destroy(gameObject);
        }
    }
}
