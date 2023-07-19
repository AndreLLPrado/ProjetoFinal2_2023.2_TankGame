using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int HP;

    //mov
    public Transform target;
    public float speed = 1f;
    private Rigidbody enemyRigidbody;

    //bullet
    [SerializeField]
    private Transform bulletSpawn;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public float fireRate = 2f;
    private float nextFireTime;

    //logic
    [SerializeField]
    private float detectionRadius;
    public LayerMask playerLayer;
    private bool readyToShoot;


    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectsWithTag("Player")[0].transform;

        nextFireTime = Time.time;
    }

    private void Update()
    {
        if(HP <= 0)
        {
            GameObject.Find("GameController").GetComponent<GameController>().AddPointsToScore(100);
            Destroy(gameObject);
        }
        if (!GameObject.Find("GameController").GetComponent<GameController>().getGameOver()) 
        { 
            DetectPlayer();
        }
    }

    void FixedUpdate()
    {
        if (!GameObject.Find("GameController").GetComponent<GameController>().getGameOver()) 
        {
            if (target != null)
            {
                RotateTowardsTarget();

                if(!readyToShoot)
                {
                    Vector3 direction = (target.position - transform.position).normalized;
                    enemyRigidbody.velocity = direction * speed;
                }

                if (Time.time >= nextFireTime)
                {
                    if(readyToShoot)
                        Shoot();

                    nextFireTime = Time.time + fireRate; // Atualiza o próximo momento disponível para disparar
                }
            }
        }
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
    }

    void RotateTowardsTarget()
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemyRigidbody.MoveRotation(targetRotation);
        }
    }

    void Shoot()
    {
        // Debug.Log("Enemy shoot!");
        Vector3 direction = (target.position - transform.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = direction * bulletSpeed;
    }

    void DetectPlayer()
    {
        Vector3 direction = target.position - transform.position;
        float distance = direction.magnitude;

        if (distance <= detectionRadius)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, detectionRadius, playerLayer))
            {
                // Debug.Log("Player Detected!");
                readyToShoot = true;
            }
            else
            {
                readyToShoot = false;
            }
        }
        else
        {
            readyToShoot = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
