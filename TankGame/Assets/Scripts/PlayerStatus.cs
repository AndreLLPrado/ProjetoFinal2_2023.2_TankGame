using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField]
    private int HP;

    private void Update()
    {
        if(HP < 1)
        {
            GameObject.Find("GameController").GetComponent<GameController>().setGameOver(true);
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
