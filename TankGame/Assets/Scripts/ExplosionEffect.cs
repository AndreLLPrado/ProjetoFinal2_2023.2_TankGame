using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    private AudioSource sfx;
    void Start()
    {
        sfx = GetComponent<AudioSource>();
        StartCoroutine("duaration");
    }
    IEnumerator duaration()
    {
        sfx.Play();
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
