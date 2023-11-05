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
    private Transform[] bulletSpawn;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public float fireRate = 2f;
    private float nextFireTime;

    //logic
    [SerializeField]
    private float detectionRadius;
    public LayerMask playerLayer;
    private bool readyToShoot;
    bool playerHasSpawned;

    [SerializeField]
    private int points;

    [SerializeField]
    private bool isMegaTank;

    //Drop item
    [Header("Drop item")]
    [SerializeField]
    private GameObject[] dropsPrefabs;
    [SerializeField]
    private int[] dropChance;

    Rigidbody rigidBody;
    // VFX
    [SerializeField]
    private ParticleSystem particleSystem;

    // SFX
    private AudioSource audioSource;


    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody>();
        // particleSystem = GetComponent<ParticleSystem>();
        nextFireTime = Time.time;
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        if (!isMegaTank)
        {
            int difficulty = GameObject.Find("GameController").GetComponent<GameController>().getDifficultyLevel();
            if (difficulty > 5)
            {
                int increaseBonus = difficulty - 5;
                HP += increaseBonus;
            }

        }
        else
        {
            int increaseBonus  = GameObject.Find("GameController").GetComponent<GameController>().getMegaTankPowerIncrease();
            HP += increaseBonus * 10;

            if(fireRate > 0.25f)
            {
                fireRate -= increaseBonus * 0.25f;
            }
        }
    }

    private void Update()
    {
        if(!playerHasSpawned)
        {
            target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
            if(target != null)
            {
                playerHasSpawned = true;
            }
        }
        if(HP <= 0)
        {
            GameObject.Find("GameController").GetComponent<GameController>().AddPointsToScore(points);
            if (isMegaTank)
            {
                GameObject.Find("GameController").GetComponent<GameController>().setMegaTankHasSpawned(false);
                dropItem();
            }
            dropItem();
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

    private void dropItem()
    {
        int item = Random.Range(0, dropsPrefabs.Length);
        int drop = Random.Range(0, 101);
        if(drop <= dropChance[item])
        {
            Vector3 position = new Vector3(transform.position.x, -1, transform.position.z);
            Instantiate(dropsPrefabs[item], position, Quaternion.identity);
        }
    }
    public void TakeDamage(int damage)
    {
        StartCoroutine(playSoundAndTakeDamage(damage));
        // HP -= damage;
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

        if(bulletSpawn.Length <= 1)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn[0].position, Quaternion.identity);
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            bulletRigidbody.velocity = direction * bulletSpeed;
        }
        else
        {
            Debug.Log("Instantiate mega bullet");
            for(int i = 0; i < bulletSpawn.Length; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawn[i].position, Quaternion.identity);
                Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
                bulletRigidbody.velocity = direction * bulletSpeed;
            }
        }

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
    public void playDamageSound()
    {
        // StartCoroutine(playSound());
        audioSource.Play();
    }
    IEnumerator playSoundAndTakeDamage(int damage)
    {
        particleSystem.Play();
        audioSource.Play();
        yield return new WaitForSeconds(.2f);
        HP -= damage;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
