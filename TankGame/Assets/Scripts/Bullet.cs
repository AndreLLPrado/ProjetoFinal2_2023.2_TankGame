using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float destructionDelay;

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
        // Debug.Log("bullet colide with: " + collision.gameObject.name);
        Destroy(gameObject);
    }
}
