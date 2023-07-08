using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    void Update()
    {
        // Obter a posi��o do mouse no mundo
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.transform.position.y - transform.position.y;
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Calcular a dire��o horizontal do objeto em rela��o � posi��o do mouse
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0f;

        // Rotacionar o objeto na dire��o horizontal do mouse
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Euler(0f, targetRotation.eulerAngles.y, 0f);
        }
    }
}
