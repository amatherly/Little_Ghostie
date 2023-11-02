using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindController : MonoBehaviour
{
    
    private float activeDuration = 2f;
    private float inactiveDuration = 1.5f;
    [SerializeField] private GameObject winds;

    void Start()
    {
        StartCoroutine(CycleWind());
    }
    
    private IEnumerator CycleWind()
    {
        while (true)
        {
            winds.SetActive(true);
            yield return new WaitForSeconds(activeDuration);
            winds.SetActive(false);
            yield return new WaitForSeconds(inactiveDuration);
        }
    }
}
