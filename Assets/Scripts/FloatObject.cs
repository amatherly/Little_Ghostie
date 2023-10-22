using UnityEngine;
using System.Collections;

public class FloatObject : MonoBehaviour
{
    [SerializeField] private float amplitude = 0.5f;  // the distance the object should float up and down
    [SerializeField] private float speed = 1f;        // the speed of the float

    private Vector3 startPosition;

    void Start()
    {
        // Store the starting position of the object
        startPosition = transform.position;

        // Start the coroutine that moves the object up and down
        StartCoroutine(FloatUpAndDown());
    }

    IEnumerator FloatUpAndDown()
    {
        while (true)
        {
            // Calculate the new position based on sine function
            Vector3 newPosition = startPosition + Vector3.up * Mathf.Sin(Time.time * speed) * amplitude;

            // Move the object to the new position
            transform.position = newPosition;

            yield return null;
        }
    }
}