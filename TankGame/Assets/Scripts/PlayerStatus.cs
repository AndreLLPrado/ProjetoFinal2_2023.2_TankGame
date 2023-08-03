using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField]
    private int HP;
    private int maxHP;

    private void Start()
    {
        maxHP = HP = GameObject.Find("GameController").GetComponent<GameController>().getPlayerHP();
    }
    private void Update()
    {
        if(HP < 1)
        {
            GameObject.Find("GameController").GetComponent<GameController>().setGameOver(true);
        }
    }

    public void cure(int cure)
    {
        HP += cure;
        if(HP > maxHP)
        {
            HP = maxHP;
        }
    }
    public void TakeDamage(int damage)
    {
        HP -= damage;
    }

    public int getPlayerHP()
    {
        return HP;
    }
}
