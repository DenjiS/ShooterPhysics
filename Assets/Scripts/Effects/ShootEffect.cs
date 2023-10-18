using System.Collections;
using UnityEngine;

public class ShootEffect : MonoBehaviour
{
    [Header("Bullet Launch")]
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private GameObject _lightSource;
    [SerializeField] private AudioSource _reloadSound;
    [SerializeField] private AudioSource _shootSound;
    [SerializeField] private Animator _animator;

    [Header("Shell Extraction")]
    [SerializeField] private Rigidbody _shellPrefab;
    [SerializeField] private Transform _shellPoint;
    [SerializeField] private float _shellSpeed;
    [SerializeField] private float _shellAngularVelocity;

    public void Perform()
    {
        StartCoroutine(Effecting());
    }

    public void ExtractShell()
    {
        _reloadSound.Play();
        Rigidbody shell = Instantiate(_shellPrefab, _shellPoint.position, _shellPoint.rotation, null);
        shell.velocity = _shellPoint.right * _shellSpeed;
        shell.angularVelocity = Vector3.up * _shellAngularVelocity;
    }

    private IEnumerator Effecting()
    {
        _shootSound.Play();
        _lightSource.SetActive(true);
        _particleSystem.Clear();
        _particleSystem.Play();
        _animator.SetTrigger("Shoot");

        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        _lightSource.SetActive(false);
    }
}
