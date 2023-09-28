using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;

    [SerializeField] private float _speed;
    [SerializeField] private float _strafeSpeed;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private float _gravityFactor;

    [SerializeField] private float _horizontalTurnSensitivity;
    [SerializeField] private float _verticalTurnSensitivity;

    [SerializeField] private float _verticalMinAngle;
    [SerializeField] private float _verticalMaxAngle;

    private Transform _transform;
    private CharacterController _characterController;
    private Vector3 _verticalVelocity;
    private float _cameraAngle;

    private void Awake()
    {
        _transform = transform;
        _characterController = GetComponent<CharacterController>();
        _cameraAngle = _cameraTransform.localEulerAngles.x;
    }

    private void Update()
    {
        Vector3 forward = Vector3.ProjectOnPlane(_cameraTransform.forward, Vector3.up);
        forward.Normalize();
        Vector3 right = Vector3.ProjectOnPlane(_cameraTransform.right, Vector3.up);
        right.Normalize();

        _cameraAngle -= Input.GetAxis("Mouse Y") * _verticalTurnSensitivity;
        _cameraAngle = Mathf.Clamp(_cameraAngle, _verticalMinAngle, _verticalMaxAngle);
        _cameraTransform.localEulerAngles = Vector3.right * _cameraAngle;

        _transform.Rotate(_horizontalTurnSensitivity * Input.GetAxis("Mouse X") * Vector3.up);

        if (_characterController != null)
        {
            if (_characterController.isGrounded)
            {
                Vector3 playerSpeed =
                    _speed * Input.GetAxis("Vertical") * forward
                    + _strafeSpeed * Input.GetAxis("Horizontal") * right;

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _verticalVelocity = Vector3.up * _jumpSpeed;
                }
                else
                {
                    _verticalVelocity = Vector3.down;
                }

                _characterController.Move((playerSpeed + _verticalVelocity) * Time.deltaTime);
            }
            else
            {
                Vector3 horizontalVelocity = _characterController.velocity;
                horizontalVelocity.y = 0;

                _verticalVelocity += Time.deltaTime * _gravityFactor * Physics.gravity;
                _characterController.Move((horizontalVelocity + _verticalVelocity) * Time.deltaTime);
            }
        }
    }

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
