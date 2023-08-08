using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCar : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private Vector3 direction;
    [SerializeField]
    private float selftDestructTime;

    void Update()
    {
        transform.transform.position += (direction * speed) * Time.deltaTime;
        Invoke("selfDestrouct", selftDestructTime);
    }

    public void setSpeed(float s)
    {
        speed = s;
    }

    public void setDirection(Vector3 dir)
    {
        direction = dir;
    }

    public void setRotaionY(float angle)
    {
        transform.eulerAngles = new Vector3(0f, angle, 0f);
    }

    void selfDestrouct()
    {
        Destroy(gameObject);
    }
}
