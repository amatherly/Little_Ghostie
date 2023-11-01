using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject keyPrefab;
    [SerializeField]public Transform[] spawnLocations;
 
    void Start()
    {
                 
            Instantiate(keyPrefab, spawnLocations[Random.Range(1, 4)].position, Quaternion.identity); 
        
    }

   
}
