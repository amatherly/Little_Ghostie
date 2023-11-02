
using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public bool IsBoostActive => isBoostActive;

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

    [Header("Invisibility Settings")]
    [SerializeField] private float invisibilityDuration = 3f;
    [SerializeField] private float fadeDuration = .3f;
    
    [Header("Boost Meter")]
    [SerializeField] private float boostMeterMax = 100f;
    [SerializeField] private float boostMeterIncreaseRate = 1f;
    [SerializeField] private float boostMeterDecreaseRate = 10f;
    [SerializeField] private float boostMeter = 100f;
    [SerializeField] private bool isBoostActive = false;
    
    [Header("Boost Meter UI")]
    [SerializeField] private Image boostMeterImage;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(FloatUpAndDown());
        
    }

    private void FixedUpdate()
    {
        HandleBoostMeter();
        UpdateBoostMeterUI();
        
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

        if (Input.GetKeyDown(KeyCode.Space) && boostMeter >= 97f && !isBoostActive)
        {
            Debug.Log("BooostActivated");
            ActivateInvisibility();
        }
        
        Debug.Log("Player has key: " + hasKey);
    }
    
    private void HandleBoostMeter()
    {
        if (!isBoostActive)
        {
            if (boostMeter < boostMeterMax)
            {
                boostMeter += boostMeterIncreaseRate * Time.deltaTime;
            }
        }
    }
    
    public void ActivateInvisibility()
    {
        isBoostActive = true;
        StartCoroutine(BecomeInvisible());
        boostMeter = 0f;
    }
    
    private IEnumerator BecomeInvisible()
    {
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            SetSpriteAlpha(Mathf.Lerp(1f, 0.1f, normalizedTime)); 
            yield return null;
        }
        SetSpriteAlpha(0.1f); 
        
        yield return new WaitForSeconds(invisibilityDuration);
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            SetSpriteAlpha(Mathf.Lerp(0.1f, 1f, normalizedTime));
            yield return null;
        }
        SetSpriteAlpha(1f); 
        isBoostActive = false;
    }
    
    private void SetSpriteAlpha(float alpha)
    {
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
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
            FindObjectOfType<GameManager>().GameOver();
            Debug.Log("Player died");
        }
    }
    

    private IEnumerator FloatUpAndDown()
    {
        while (true)
        {
            verticalDelta = Mathf.Sin(Time.time * speed) * amplitude;
            yield return null;
        }
    }
    
    private void UpdateBoostMeterUI()
    {
        boostMeterImage.fillAmount = boostMeter / boostMeterMax;
    }
}