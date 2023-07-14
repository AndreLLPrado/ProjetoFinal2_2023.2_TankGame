using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public Camera mainCamera;

    public float fireRate;  // Intervalo entre disparos em segundos

    private float nextFireTime;  // Próximo momento disponível para disparar

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (!GameObject.Find("GameController").GetComponent<GameController>().getGameOver()) 
        { 
            if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
            {
                nextFireTime = Time.time + fireRate;

                Vector3 mousePosition = Input.mousePosition;
                Ray ray = mainCamera.ScreenPointToRay(mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    Vector3 direction = hit.point - transform.position;

                    GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                    Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
                    bulletRigidbody.velocity = direction.normalized * bulletSpeed;
                }
            }
        }
    }
}
