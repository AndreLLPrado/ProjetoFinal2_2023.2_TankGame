using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    private AudioSource src;

    [SerializeField]
    private AudioClip reapirTool, playerDamage;

    private void Start()
    {
        src = GetComponent<AudioSource>();
    }

    public void RapairToolSFXPlay()
    {
        src.clip = reapirTool;
        src.Play();
    }

    public void PlayerTakeDamageSFXPlay() 
    {
        src.clip = playerDamage;
        src.time = .1f;
        src.Play();
    }
}
