
using UnityEngine;

public class Wind : MonoBehaviour
{
    private int damage = 1;
    public Transform[] array;
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            player.TakeDamage(damage);
        }
    }
}
