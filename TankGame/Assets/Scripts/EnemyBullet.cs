using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float destructionDelay;
    [SerializeField]
    private int damage;

    void Start()
    {
        Invoke("DestroyObject", destructionDelay);
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Debug.Log("Enemy bullet colide with: " + collision.gameObject.name + " tag: " + collision.gameObject.tag);
        if(collision.gameObject.tag == "Player")
        {
            if(collision.gameObject.GetComponent<PlayerStatus>() != null)
            {
                collision.gameObject.GetComponent<PlayerStatus>().TakeDamage(1);
            }
        }
        Destroy(gameObject);
    }
}
