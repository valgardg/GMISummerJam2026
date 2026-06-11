using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.125f;

    public float leftBoundary = -10f;
    public float rightBoundary = 10f;

    public float yOffset = 2f;

    void LateUpdate()
    {
        Vector3 desiredPosition = new Vector3(
            Mathf.Clamp(player.position.x, leftBoundary, rightBoundary),
            player.position.y + yOffset,
            transform.position.z
        );
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
