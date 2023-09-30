using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Collider _projectileCollider;

    private float _damage;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider != null)
        {
            BaseHealth health = collision.collider.GetComponentInParent<BaseHealth>();

            if (health != null)
            {
                health.TakeDamage(_damage);
            }
        }
    }

    public void Initialize(float damage, Collider playerCollider)
    {
        _damage = damage;
        Physics.IgnoreCollision(_projectileCollider, playerCollider);
    }

    public void Shoot(Vector3 startPosition, Vector3 velocity)
    {
        _rigidbody.position = startPosition;
        _rigidbody.velocity = velocity;
    }
}
