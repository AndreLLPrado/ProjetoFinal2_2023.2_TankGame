using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public Camera mainCamera;

    public float fireRate;  // Intervalo entre disparos em segundos

    private float nextFireTime;  // Pr�ximo momento dispon�vel para disparar

    float increseValue;

    [SerializeField]
    private GameObject fireRateBar;

    private void Start()
    {
        float increseValue = 0;
        mainCamera = Camera.main;
        fireRate = GameObject.Find("GameController").GetComponent<GameController>().getPlayerFireRate();
    }

    void Update()
    {
        float pause = Time.timeScale;
        if (!GameObject.Find("GameController").GetComponent<GameController>().getGameOver() && pause > 0) 
        { 
            if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
            {
                fireRateBar.transform.localScale = new Vector3(0.0f, 0.2f, 0.2f);
                increseValue = 0;

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
            else
            {
                // Calcula o valor de preenchimento da barra de acordo com a velocidade do fireRate
                float fillValue = Mathf.Clamp(1.2f - (nextFireTime - Time.time) / fireRate, 0.0f, 1.2f);
                fireRateBar.transform.localScale = new Vector3(fillValue, 0.2f, 0.2f);
            }
        }
    }
}
