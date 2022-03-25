using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private float smoothTime = 0.3f;
    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;
    
    void Update()
    {
        targetPosition =
            new Vector3(player.position.x, cameraOffset.y != 0 ? cameraOffset.y : transform.position.y, cameraOffset.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
