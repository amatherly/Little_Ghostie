using UnityEngine;

public class Pickup : MonoBehaviour
{
    public float speed = 2f;
    public float pickupRange = 1f;
    public AudioClip pickupSound;

    private Transform playerTransform;
    private PlayerController player;
    public AudioSource audioSource;
    


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerTransform = player.transform;
    }


    private void Update()
    {
        // Check if the player is in range
        if (Vector2.Distance(transform.position, playerTransform.position) <= pickupRange)
        {
            Debug.Log("Player is in range to float item");
            transform.position = Vector2.Lerp(transform.position, playerTransform.position, Time.deltaTime * speed);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<GameManager>().DisplayPrompt();
            AudioSource.PlayClipAtPoint(pickupSound, this.gameObject.transform.position);
            player.HasKey = true;
            Destroy(gameObject);
        }
    }
}