using UnityEngine;
using System.Collections;

public class FloatObject : MonoBehaviour
{
    [SerializeField] private float amplitude = 0.5f;  
    [SerializeField] private float speed = 3.75f;   
    private Vector3 startPosition;
    private float verticalDelta = 0f;
    

    void Start()
    {
        startPosition = transform.position; // Save the initial position
        StartCoroutine(FloatUpAndDown());
    }

    private IEnumerator FloatUpAndDown()
    {
        while (true)
        {
            verticalDelta = Mathf.Sin(Time.time * speed) * amplitude;
            transform.position = startPosition + new Vector3(0f, verticalDelta, 0f); // Apply the vertical delta

            yield return null;
        }
    }
}