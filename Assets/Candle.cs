using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Candle : MonoBehaviour
{
    public float ActiveDuration
    {
        get => activeDuration;
        set => activeDuration = value;
    }

    [SerializeField] private Image currSprite;
    [SerializeField] private float activeDuration = 2f;
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private Light2D candle;
    [SerializeField] private float damage = 1f;


    void Start()
    {
        StartCoroutine(UpdateSprite());
        
    }

    private void Update()
    {
        if (candle.pointLightInnerRadius <= 0 || candle.pointLightOuterRadius <= 0)
        {
            FindObjectOfType<GameManager>().GameOver();
        }
    }

    private IEnumerator UpdateSprite()
    {
        while (true)
        {
            candle.pointLightOuterRadius -= damage;
            candle.pointLightInnerRadius -= damage;
            UpdateHealthbar();
            yield return new WaitForSeconds(activeDuration);
        }
    }
    

    public void UpdateHealthbar()
    {
        currSprite.sprite = sprites[(int)candle.pointLightInnerRadius + 1];
    }

}
