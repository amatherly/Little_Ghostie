using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostParty : MonoBehaviour
{
    public GameObject ghostCollider;


    void Start()
    {
    }


    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
 
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.HasKey = true;
            }

            // Destroy the object with this script attached.
            Destroy(gameObject);
        }
    }
}
