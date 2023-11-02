
using System.Collections;
using UnityEngine;

public class GhostBuster : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float followSpeed = 2.5f;
    [SerializeField] private float patrolSpeed = 1f;
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float shootForce;
    [SerializeField] private Transform gun;
    [SerializeField] private Transform forwardIndicator;
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private GameObject  waterDropPrefab;
    [SerializeField] private float  viewConeAngle = 45f;
    [SerializeField] private float  soundMultiplier = 5f;
    [SerializeField] private float  soundInterval = 0.1f;
    
    
    private PlayerController player;
    private AudioSource shootingSound;
    
    private int currentPoint = 0;
    private enum State { Patrol, Follow }
    private State currentState;
    private float timeSinceLastShot = 0f;
    private float shootingInterval = 1f;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        shootingSound = GetComponent<AudioSource>();
        currentState = State.Patrol;
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        
        switch (currentState)
        {
            case State.Patrol:
                Patrol();
                moveSpeed = patrolSpeed;
                Vector3 toPlayer = player.transform.position - transform.position;
                Vector3 forwardDirection = forwardIndicator.position - transform.position;

                float angleToPlayer = Vector3.Angle(forwardDirection, toPlayer);

                Vector3 leftRayDirection = Quaternion.Euler(0, 0, -viewConeAngle) * forwardDirection;
                Vector3 rightRayDirection = Quaternion.Euler(0, 0, viewConeAngle) * forwardDirection;
                Debug.DrawRay(transform.position, leftRayDirection, Color.red);
                Debug.DrawRay(transform.position, rightRayDirection, Color.red);

                if (Vector3.Distance(transform.position, player.transform.position) < detectionRange && Mathf.Abs(angleToPlayer) < viewConeAngle)
                {
                    currentState = State.Follow;
                }
                break;

            case State.Follow:
                Debug.Log("Following player");
                moveSpeed = followSpeed;
                FollowPlayer();
                if (Vector3.Distance(transform.position, player.transform.position) >= detectionRange)
                {
                    currentState = State.Patrol;
                }
                break;
        }
    }

    private void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        Transform target = patrolPoints[currentPoint];
        MoveTowards(target.position);

        if (Vector3.Distance(transform.position, target.position) < 1f)
        {
            currentPoint = (currentPoint + 1) % patrolPoints.Length;
        }
    }

    private void FollowPlayer()
    {
        MoveTowards(player.transform.position);
        if (timeSinceLastShot >= shootingInterval)
        {
            Debug.Log("Shooting player");
            Shoot();
            timeSinceLastShot = 0f;
        }
    }

    private void MoveTowards(Vector3 target)
    {
        Vector3 direction = target - transform.position;
        
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        
        if (direction.x < 0f)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (direction.x > 0f)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
    
    private void Shoot()
    {
        Debug.Log("pew pew");
        GameObject waterDrop = Instantiate(waterDropPrefab, gun.position, gun.rotation);
        ParticleSystem waterTrail = waterDrop.GetComponentInChildren<ParticleSystem>();

        if(waterTrail != null)
        {
            waterTrail.Play();
        }

        Vector2 directionToPlayer = (player.transform.position - gun.position).normalized;
        waterDrop.GetComponent<Rigidbody2D>().AddForce(directionToPlayer * shootForce, ForceMode2D.Impulse);
        PlayShootingSounds();
    }
    
    private IEnumerator PlayShootingSoundsCoroutine()
    {
        for (int i = 0; i < soundMultiplier; i++)
        {
            shootingSound.pitch = Random.Range(0.5f, 1.5f);
            shootingSound.PlayOneShot(shootingSound.clip);
            yield return new WaitForSeconds(soundInterval); 
        }
    }

    private void PlayShootingSounds()
    {
        StartCoroutine(PlayShootingSoundsCoroutine());
    }
}