using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;

    public Transform CameraTransform => _cameraTransform;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.rigidbody != null)
            hit.rigidbody.velocity = Vector3.up * 25;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        CharacterController controller = GetComponent<CharacterController>();

        Gizmos.DrawWireCube(transform.position, Vector3.right + Vector3.forward + Vector3.up * controller.height);
    }
}
