using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private Weapon _weapon;

    private Transform _transform;

    public Transform CameraTransform => _cameraTransform;

    private void Awake()
    {
        _weapon.Initialize(GetComponent<CharacterController>());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            _weapon.Shoot(_cameraTransform.position, _cameraTransform.forward);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.rigidbody != null)
            hit.rigidbody.WakeUp();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        CharacterController controller = GetComponent<CharacterController>();

        Gizmos.DrawWireCube(transform.position, Vector3.right + Vector3.forward + Vector3.up * controller.height);
    }
}
