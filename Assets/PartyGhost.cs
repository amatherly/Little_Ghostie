using UnityEngine;

public class PartyGhost : MonoBehaviour
{
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float waitTimeAtPoint = 2f;
    [SerializeField] private float stoppingDistance = 0.1f;

    private int currentPoint = 0;
    private bool isWaiting = false;
    private float waitTimer;
    private bool movingForward = true; // New field to track direction of patrol

    void Update()
    {
        Patrol();
    }

    private void Patrol()
    {
        if (patrolPoints.Length < 2) return; // Ensure there are at least two points to patrol between

        if (!isWaiting)
        {
            Transform target = patrolPoints[currentPoint];
            MoveTowards(target.position);

            if (Vector3.Distance(transform.position, target.position) < stoppingDistance)
            {
                isWaiting = true;
                waitTimer = waitTimeAtPoint;
            }
        }
        else
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0f)
            {
                isWaiting = false;
                UpdateCurrentPoint(); // Call the new method to update the current point
            }
        }
    }

    private void UpdateCurrentPoint()
    {
        if (movingForward)
        {
            if (currentPoint < patrolPoints.Length - 1)
            {
                currentPoint++;
            }
            else
            {
                movingForward = false;
                currentPoint--;
            }
        }
        else
        {
            if (currentPoint > 0)
            {
                currentPoint--;
            }
            else
            {
                movingForward = true;
                currentPoint++;
            }
        }
    }

    private void MoveTowards(Vector3 target)
    {
        Vector3 direction = target - transform.position;
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

        if (direction != Vector3.zero)
        {
            transform.localScale = new Vector3(Mathf.Sign(direction.x) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}
