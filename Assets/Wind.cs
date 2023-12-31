using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    private int damage = 2;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") )
        {
            PlayerController player = other.GetComponent<PlayerController>();
            player.TakeDamage(damage);
        }
    }
}