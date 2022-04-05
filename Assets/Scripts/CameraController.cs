using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private float smoothTime = 0.3f;
    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;
    private Transform player;
    
    void Update()
    {
        player = PlayerManager.Instance.playerIsBig ? 
            PlayerManager.Instance.bigCharacter.transform : 
            PlayerManager.Instance.smallCharacter.transform;
        
        var playerViewportPos = Camera.main.WorldToViewportPoint(player.transform.position);
        if (playerViewportPos.x >= 0.5f)
        {
            TargetFollow();
        }
    }

    void TargetFollow()
    {
        var cameraOffsetY = cameraOffset.y != 0 ? cameraOffset.y : transform.position.y;
        targetPosition = new Vector3(player.position.x, cameraOffsetY, cameraOffset.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
