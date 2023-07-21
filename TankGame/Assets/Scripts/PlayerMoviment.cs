using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{
    private Rigidbody rb;

    public float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        speed = GameObject.Find("GameController").GetComponent<GameController>().getPlayerSpeed();
    }

    void Update()
    {
        if (!GameObject.Find("GameController").GetComponent<GameController>().getGameOver())
        {
            MovePlayerByPhysics();
        }
    }

    private void MovePlayerByPhysics()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical) * speed;

        rb.velocity = movement;
    }
}
