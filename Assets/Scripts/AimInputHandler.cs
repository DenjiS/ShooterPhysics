using UnityEngine;

[RequireComponent(typeof(Player))]
public class AimInputHandler : MonoBehaviour
{
    private const string MouseX = "Mouse X";
    private const string MouseY = "Mouse Y";

    [Header("Sensitivity")]
    [SerializeField] private float _horizontalTurnSensitivity;
    [SerializeField] private float _verticalTurnSensitivity;

    [Header("Vertical rotation limits")]
    [SerializeField][Range(-90, 0)] private float _verticalMinAngle;
    [SerializeField][Range(0, 90)] private float _verticalMaxAngle;

    private Transform _transform;
    private Transform _cameraTransform;

    private float _cameraAngle;

    private void Awake()
    {
        _transform = transform;
        _cameraTransform = GetComponent<Player>().CameraTransform;

        _cameraAngle = _cameraTransform.localEulerAngles.x;
    }

    private void Update()
    {
        UpdateVerticalAngle();

        _transform.Rotate(_horizontalTurnSensitivity * Input.GetAxis(MouseX) * Vector3.up);
    }

    private void UpdateVerticalAngle()
    {
        _cameraAngle -= Input.GetAxis(MouseY) * _verticalTurnSensitivity;
        _cameraAngle = Mathf.Clamp(_cameraAngle, _verticalMinAngle, _verticalMaxAngle);
        _cameraTransform.localEulerAngles = Vector3.right * _cameraAngle;
    }
}
