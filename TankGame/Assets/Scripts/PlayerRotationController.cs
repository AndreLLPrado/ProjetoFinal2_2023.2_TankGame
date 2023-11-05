using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotationController : MonoBehaviour
{
    private Vector3 targetRotation;
    void Start()
    {
        
    }

    void Update()
    {
        if (!GameObject.Find("GameController").GetComponent<GameController>().getGameOver() && Time.timeScale > 0)
            RotationPlayer();
    }

    private void RotationPlayer()
    {
        float horizontalAxis = Input.GetAxisRaw("Horizontal");
        float verticalAxis = Input.GetAxisRaw("Vertical");

        if (horizontalAxis != 0f || verticalAxis != 0f) {
            float rotation = Mathf.Atan2(horizontalAxis, verticalAxis) * Mathf.Rad2Deg;
            targetRotation = new Vector3(0f, rotation, 0f);
        }
        transform.eulerAngles = targetRotation;
    }
}
