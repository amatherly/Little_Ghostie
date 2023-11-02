using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip hoverSound;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        hoverSound = audioSource.clip;
    }

    
    void Update()
    {
        
    }

    public void OnHover()
    {
        audioSource.PlayOneShot(hoverSound);
    }
    
}
