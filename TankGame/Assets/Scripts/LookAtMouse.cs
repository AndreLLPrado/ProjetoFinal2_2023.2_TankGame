using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    void Update()
    {
        // Obter a posição do mouse no mundo
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.transform.position.y - transform.position.y;
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Calcular a direção horizontal do objeto em relação à posição do mouse
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0f;

        // Rotacionar o objeto na direção horizontal do mouse
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Euler(0f, targetRotation.eulerAngles.y, 0f);
        }
    }
}
