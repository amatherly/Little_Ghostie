
using System;
using Unity.VisualScripting;
using UnityEngine;

public class WaterDrop : MonoBehaviour
{

    private float lifeTime = 0.7f;


    public void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().TakeDamage(1);
            Destroy(gameObject);
        }
    }
    

    
}
