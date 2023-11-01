using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostParty : MonoBehaviour
{
    public GameObject ghostCollider; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        private void OnTriggerEnter2D(Collider 2D other){
            //if player collides into door, take to other map 
            if (other.CompareTag("Player"))
        {
     
            player.HasKey = true;
            Destroy(gameObject);
        }
        
    }
}
