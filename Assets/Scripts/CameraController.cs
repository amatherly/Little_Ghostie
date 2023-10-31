
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float smoothTime = 0.3f;
    public Vector3 offset;
    public Vector2 minPosition;
    public Vector2 maxPosition;
    
    void LateUpdate()
    {
        Vector3 desiredPos = transform.position + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, smoothTime);
        transform.position = smoothPos;
    }
}
