using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerEnemy : MonoBehaviour
{
    [SerializeField]
    private LayerMask playerLayer;
    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    private GameObject bombPrefab;
    private int currentHP;

    private void Update()
    {
        currentHP = gameObject.GetComponent<Enemy>().getHP();
        if(currentHP <= 0)
        {
            Instantiate(bombPrefab, transform.position, Quaternion.identity);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<PlayerStatus>() != null)
            {
                collision.gameObject.GetComponent<PlayerStatus>().TakeDamage(5);
            }
            if(explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
