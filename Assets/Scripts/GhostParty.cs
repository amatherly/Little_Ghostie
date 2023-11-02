using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostParty : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            player.CanMove = false;
            FindObjectOfType<GameManager>().Party();
        }
    }
}
