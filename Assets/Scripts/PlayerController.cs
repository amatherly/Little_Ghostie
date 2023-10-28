
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    public bool HasKey
    {
        get => hasKey;
        set => hasKey = value;
    }

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float amplitude = 0.2f; // The distance the object should float up and down
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private float speed = 1f; // The speed of the float
    [SerializeField] private Light2D candle; // The speed of the float



    private Rigidbody2D rb;
    private Sprite currSprite;
    private Vector3 startPosition;
    [CanBeNull] private AudioSource moveSound;
    private float verticalDelta = 0f;
    private int health = 7;
    private bool hasKey = false;

    private void Awake()
    {
        currSprite = GetComponent<Sprite>();
        rb = GetComponent<Rigidbody2D>();
        moveSound = GetComponent<AudioSource>();
        startPosition = transform.position;
    }

    private void Start()
    {
        StartCoroutine(FloatUpAndDown());
        
    }

    private void FixedUpdate()
    {
        // Player movement
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        Vector2 moveDirection = new Vector2(horizontalMovement, verticalMovement).normalized;
        Vector2 newVelocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed + verticalDelta);

        rb.velocity = newVelocity;

        if (moveDirection.magnitude > 0 && !moveSound.isPlaying)
        {
            moveSound.UnPause();
        }
        else
        {
            moveSound.Pause();
        }
        
        Debug.Log("Player has key: " + hasKey);
    }

    public void TakeDamage(int points)
    {
        health -= points;
        UpdateSprite(health);
    }

    private IEnumerator FloatUpAndDown()
    {
        while (true)
        {
            // Calculate the new vertical delta based on sine function
            verticalDelta = Mathf.Sin(Time.time * speed) * amplitude;

            yield return null;
        }
    }

    private void UpdateSprite(int candleLevel)
    {
        if (candleLevel > 0)
        {
            currSprite = sprites[candleLevel];
            candle.intensity -= .2f;
            Debug.Log("Sprite updated, candle level: " + candleLevel);
        }
        else
        {
            GameOver();
            Debug.Log("Player died");
        }
    }

    public void GameOver()
    {
        FindObjectOfType<UI>().GameOver();
        Time.timeScale = 0;
    }
    
}