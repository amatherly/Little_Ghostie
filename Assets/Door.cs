using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject inside;
    [SerializeField] private GameObject outside;
    [SerializeField] private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && player.HasKey)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Door Opened");
                inside.SetActive(true);
                outside.SetActive(false);
            }
        }
    }
}
