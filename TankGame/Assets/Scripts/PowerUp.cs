using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [Header("Config")]
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float duration;
    [SerializeField]
    private Light light;

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
        if (collision.gameObject.tag == "Player")
        {
            GameObject.Find("Canvas").GetComponent<SFXPlayer>().RapairToolSFXPlay();
            if (collision.gameObject.GetComponentInChildren<Shooting>() != null)
            {
                collision.gameObject.GetComponentInChildren<Shooting>().setHasPowerUp(true);
            }

            Destroy(gameObject);
        }
    }
}
