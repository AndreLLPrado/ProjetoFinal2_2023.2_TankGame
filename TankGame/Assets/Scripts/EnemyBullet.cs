using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float destructionDelay;
    [SerializeField]
    private int damage;

    private bool hasCollided = false;

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
        if (hasCollided)
            return;
        // Debug.Log("Enemy bullet colide with: " + collision.gameObject.name + " tag: " + collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {
            if(collision.gameObject.GetComponent<PlayerStatus>() != null)
            {
                // Debug.Log("Enemy Hit Damage: " + damage.ToString());
                collision.gameObject.GetComponent<PlayerStatus>().TakeDamage(damage);
            }
        }
        hasCollided = true;
        Destroy(gameObject);
    }
}
