using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
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
        Debug.Log("bullet colide with: " + collision.gameObject.name + " tag: " + collision.gameObject.tag);
        if(collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.GetComponent<Enemy>() != null)
            {
                collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
        Destroy(gameObject);
    }
}