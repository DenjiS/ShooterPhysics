using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _damage;
    [SerializeField] private float _maxDistance;

    [Header("Sphere Cast options")]
    [SerializeField] private float _sphereRadius;

    [Header("Projectile Shooting options")]
    [SerializeField] private Projectile _bulletPrefab;
    [SerializeField] private float _velocity;

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
    }

    private void ProjectileShoot(Vector3 startPoint, Vector3 velocity)
    {
        Projectile projectile = Instantiate(_bulletPrefab);
        projectile.Initialize(_damage, _playerCollider);

        projectile.Shoot(startPoint, velocity);
    }

    private void RaycastShoot(Vector3 startPoint, Vector3 direction)
    {
        //Line
        //if (Physics.Raycast(startPoint, direction, out RaycastHit hitInfo, _maxDistance, _layerMask, QueryTriggerInteraction.Ignore))

        //Sphere
        if (Physics.SphereCast(startPoint, _sphereRadius, direction, out RaycastHit hitInfo, _maxDistance, _layerMask, QueryTriggerInteraction.Ignore))
        {
            BaseHealth health = hitInfo.collider.GetComponentInParent<BaseHealth>();

            if (health != null)
            {
                health.TakeDamage(_damage);
            }
        }
    }
}
