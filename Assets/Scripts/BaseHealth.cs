using UnityEngine;

public abstract class BaseHealth : MonoBehaviour
{
    [SerializeField] private float _health;

    public float Health => _health;

    public void TakeDamage(float damage)
    {
        if (damage < 0)
            return;

        if (_health <= 0)
            return;

        _health -= damage;

        if (_health <= 0)
        {
            _health = 0;
            Die();
        }
    }

    public abstract void Die();
}
