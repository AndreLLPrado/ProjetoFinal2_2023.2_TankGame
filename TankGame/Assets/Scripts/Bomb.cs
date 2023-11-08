using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    private GameObject explosionPrefab;

    private AudioSource sfx;

    [SerializeField]
    private Light light;

    [SerializeField]
    private int damage;

    [SerializeField]
    private float duration;

    [SerializeField]
    private float minIntensity, maxIntensity, blimpVelocity;
    private bool blimp;

    private BombTrigger bombTrigger;

    private void Start()
    {
        blimp = false;
        sfx = GetComponent<AudioSource>();
        bombTrigger = GetComponentInChildren<BombTrigger>();
    }

    private void Update()
    {
        duration -= Time.deltaTime;

        if(!blimp)
        {
            light.intensity -= blimpVelocity * Time.deltaTime;
            if (light.intensity < minIntensity)
            {
                blimp = true;
            }
        }
        else
        {
            light.intensity += blimpVelocity * Time.deltaTime;
            if (light.intensity > maxIntensity)
            {
                blimp = false;
            }
        }

        if(duration <= 0)
        {
            explode();
        }
    }

    private void explode()
    {
        foreach (GameObject obj in bombTrigger.getInAreaList().ToArray()) 
        {
            if(obj.tag == "Player")
            {
                obj.GetComponent<PlayerStatus>().TakeDamage(damage);
            }
            if (obj.tag == "Enemy")
            {
                obj.GetComponent<Enemy>().TakeDamage(damage);
            }
        }

        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "PlayerBullet")
        {
            explode();
        }
    }
}
