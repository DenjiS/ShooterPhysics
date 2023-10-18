using UnityEngine;

public class ShellSound : MonoBehaviour
{
    [SerializeField] private AudioSource _sound;

    private void OnCollisionEnter(Collision collision)
    {
        _sound.Play();
    }
}
