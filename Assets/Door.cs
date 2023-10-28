
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject inside;
    [SerializeField] private GameObject outside;
    [SerializeField] private PlayerController player;
    [SerializeField] private Texture2D cursor;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] doorSounds;
    private bool isPlayerInRange = false;


    

    private void Update()
    {
        if (isPlayerInRange)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (player.HasKey)
                {
                    Debug.Log("Door Opened");
                    audioSource.PlayOneShot(doorSounds[0]);
                    inside.SetActive(true);
                    outside.SetActive(false);
                    player.HasKey = false;
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                }
                else
                {
                    Debug.Log("You don't have the key");
                    audioSource.PlayOneShot(doorSounds[1]);
                }
            }
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            isPlayerInRange = false;
        }
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
            isPlayerInRange = true;
        }
    }
    
    
}
