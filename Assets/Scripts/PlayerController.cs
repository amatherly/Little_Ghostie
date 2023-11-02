
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
    [SerializeField] private float amplitude = 0.5f; 
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private float speed = 1f; 
    [SerializeField] private Light2D candle;
    [SerializeField] private float damage = 0.2f; 



    private Rigidbody2D rb;
    private Sprite currSprite;
    [CanBeNull] private AudioSource moveSound;
    private float verticalDelta = 0f;
    [SerializeField] private int health = 7;
    private bool hasKey = false;

    private void Awake()
    {
        currSprite = GetComponent<Sprite>();
        rb = GetComponent<Rigidbody2D>();
        moveSound = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(FloatUpAndDown());
        
    }

    private void FixedUpdate()
    {
  
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
            verticalDelta = Mathf.Sin(Time.time * speed) * amplitude;
            yield return null;
        }
    }

    private void UpdateSprite(int candleLevel)
    {
        if (candleLevel > 0)
        {
            currSprite = sprites[candleLevel];
            candle.pointLightOuterRadius-= damage;
            candle.pointLightInnerRadius-= damage;
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