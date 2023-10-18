using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(Player))]
public class MovementInputHandler : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    [Header("Plane movement")]
    [SerializeField] private float _speed;
    [SerializeField] private float _strafeSpeed;

    [Header("Jump")]
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private float _gravityFactor;

    private Transform _cameraTransform;
    private CharacterController _characterController;

    private Vector3 _horizontalPlaneVelocity;
    private Vector3 _verticalVelocity;

    private void Awake()
    {
        _cameraTransform = GetComponent<Player>().CameraTransform;
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (_characterController != null)
        {
            if (_characterController.isGrounded)
            {
                DefineDirectionAxises(out Vector3 forward, out Vector3 right);

                UpdateMove(forward, right);

                UpdateJump();

            }
            else
            {
                UpdateFall();
            }

            _characterController.Move((_horizontalPlaneVelocity + _verticalVelocity) * Time.deltaTime);
        }
    }

    private void DefineDirectionAxises(out Vector3 forward, out Vector3 right)
    {
        forward = Vector3.ProjectOnPlane(_cameraTransform.forward, Vector3.up);
        forward.Normalize();

        right = Vector3.ProjectOnPlane(_cameraTransform.right, Vector3.up);
        right.Normalize();
    }

    private void UpdateMove(Vector3 forward, Vector3 right)
    {
        _horizontalPlaneVelocity =
            _speed * Input.GetAxis(Vertical) * forward
            + _strafeSpeed * Input.GetAxis(Horizontal) * right;

    }

    private void UpdateJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _verticalVelocity = Vector3.up * _jumpSpeed;
        }
        else
        {
            _verticalVelocity = Vector3.down;
        }
    }

    private void UpdateFall()
    {
        _horizontalPlaneVelocity = _characterController.velocity;
        _horizontalPlaneVelocity.y = 0;

        _verticalVelocity += Time.deltaTime * _gravityFactor * Physics.gravity;
    }
}
