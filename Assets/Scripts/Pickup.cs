using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Item item;
    public float speed = 2f;
    public float pickupRange = 1f;
    public AudioClip pickupSound;

    private Transform playerTransform;
    public AudioSource audioSource;

    // public InventoryManager inventoryManager;


    private void Start()
    {
        // Set the starting position of the item to the position of the Pickup object
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }



    private void Update()
    {
        // Check if the player is in range
        if (Vector2.Distance(transform.position, playerTransform.position) <= pickupRange)
        {
            Debug.Log("Player is in range to float item");
            // Move the item towards the player
            transform.position = Vector2.Lerp(transform.position, playerTransform.position, Time.deltaTime * speed);
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(pickupSound, this.gameObject.transform.position);

            // if (inventoryManager.AddItem(item))
            // {
            //     Debug.Log("item added to inventory");
            //     Debug.Log("item destroyed");
            //     // Destroy the item
            //     Destroy(gameObject);
            // }
        }
    }
}

