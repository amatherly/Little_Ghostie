
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public bool HasKey
    {
        get => hasKey;
        set => hasKey = value;
    }

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float amplitude = 0.5f;
    [SerializeField] private float speed = 1f; 
    [SerializeField] private Light2D candle;
    [SerializeField] private float damage = 1f; 
    [SerializeField] private int health = 7;


    [FormerlySerializedAs("moveSound")] [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip sizzleSound;
    private Rigidbody2D rb;
    private Sprite currSprite;

    private float verticalDelta = 0f;
    private bool hasKey = false;

    private void Awake()
    {
        currSprite = GetComponent<Sprite>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
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

        if (moveDirection.magnitude > 0 && !audioSource.isPlaying)
        {
            audioSource.UnPause();
        }
        else
        {
            audioSource.Pause();
        }
        
        Debug.Log("Player has key: " + hasKey);
    }

    public void TakeDamage(int points)
    {
        health -= points;
        UpdateSprite(health);
    }
    
    private void UpdateSprite(int candleLevel)
    {
        if (candleLevel > 0)
        {
            candle.pointLightOuterRadius-= damage;
            candle.pointLightInnerRadius-= damage;
            FindObjectOfType<Candle>().UpdateHealthbar();
            audioSource.PlayOneShot(sizzleSound);
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

    private IEnumerator FloatUpAndDown()
    {
        while (true)
        {
            verticalDelta = Mathf.Sin(Time.time * speed) * amplitude;
            yield return null;
        }
    }
    
    
}