using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Weapon : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _damage;
    [SerializeField] private float _maxDistance;

    [Header("Sphere Cast options")]
    [SerializeField] private float _sphereRadius;
    [SerializeField] private float _impactForce;

    [Header("Projectile Shooting options")]
    [SerializeField] private Projectile _bulletPrefab;
    [SerializeField] private float _velocity;

    [Header("Decal")]
    [SerializeField] private Transform _decal;
    [SerializeField] private float _decalOffset;

    [Header("Effect")]
    [SerializeField] private ShootEffect _effect;
    [SerializeField] private CameraShake _shake;

    [SerializeField] private WaterSplasher _splasher;

    Collider _playerCollider;
    private Vector3 _startPoint;
    private Vector3 _direction;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        if (Physics.SphereCast(_startPoint, _sphereRadius, _direction, out RaycastHit hitInfo, _maxDistance, _layerMask, QueryTriggerInteraction.Ignore))
        {
            Gizmos.DrawLine(_startPoint, hitInfo.point);
            Gizmos.DrawSphere(hitInfo.point, _sphereRadius);
        }
    }

    public void Initialize(CharacterController characterController)
    {
        _playerCollider = characterController as Collider;
    }

    public void Shoot(Vector3 startPoint, Vector3 direction)
    {
        _startPoint = startPoint;
        _direction = direction;

        RaycastShoot(startPoint, direction);
        //ProjectileShoot(startPoint, direction * _velocity);

        _effect.Perform();
    }

    private void ProjectileShoot(Vector3 startPoint, Vector3 velocity)
    {
        Projectile projectile = Instantiate(_bulletPrefab);
        projectile.Initialize(_damage, _playerCollider);

        projectile.Shoot(startPoint, velocity);
    }

    private void RaycastShoot(Vector3 startPoint, Vector3 direction)
    {
        _shake.MakeRecoil();

        //Line
        //if (Physics.Raycast(startPoint, direction, out RaycastHit hitInfo, _maxDistance, _layerMask, QueryTriggerInteraction.Ignore))

        //Sphere
        if (Physics.SphereCast(startPoint, _sphereRadius, direction, out RaycastHit hitInfo, _maxDistance, _layerMask, QueryTriggerInteraction.Ignore))
        {
            _splasher.TryCreateWaterSplash(startPoint, hitInfo.point);

            Transform decal = Instantiate(_decal, hitInfo.transform);
            decal.position = hitInfo.point + hitInfo.normal * _decalOffset;
            decal.LookAt(hitInfo.point);
            decal.Rotate(Vector3.up, 180, Space.Self);

            BaseHealth health = hitInfo.collider.GetComponentInParent<BaseHealth>();

            if (health != null)
            {
                health.TakeDamage(_damage);
            }

            Rigidbody victimBody = hitInfo.rigidbody;

            if (victimBody != null)
            {
                victimBody.AddForceAtPosition(direction * _impactForce, hitInfo.point, ForceMode.Force);
            }
        }
    }
}
