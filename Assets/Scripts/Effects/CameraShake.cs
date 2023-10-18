using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [Header("Shake")]
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _perlinNoiseTimeScale;
    [SerializeField] private AnimationCurve _perlinNoiseAnimationCurve;

    [Header("Recoil")]
    [SerializeField] private float _tension;
    [SerializeField] private float _damping;
    [SerializeField] private float _impulse;

    private Vector3 _shakeAngles = new();
    private Vector3 _recoilAngles = new();
    private Vector3 _recoilVelocity = new();

    private float _amplitude = 1f;

    private float _duration;
    private float _shakeTimer;

    private void Update()
    {
        UpdateRecoil();
        UpdateShake();
        _cameraTransform.localEulerAngles = _shakeAngles + _recoilAngles;
    }

    private void UpdateShake()
    {
        if (_shakeTimer > 0)
        {
            _shakeTimer -= Time.deltaTime / _duration;
        }

        float time = Time.time * _perlinNoiseTimeScale;

        _shakeAngles = new Vector3(
            Mathf.PerlinNoise(time, 0),
            Mathf.PerlinNoise(0, time),
            Mathf.PerlinNoise(time, time));

        _shakeAngles *= _amplitude;
        _shakeAngles *= _perlinNoiseAnimationCurve.Evaluate(Mathf.Clamp01(1 - _shakeTimer));
    }

    private void UpdateRecoil()
    {
        _recoilAngles += Time.deltaTime * _recoilVelocity;
        _recoilVelocity += _tension * Time.deltaTime * -_recoilAngles;
        _recoilVelocity = Vector3.Lerp(_recoilVelocity, Vector3.zero, Time.deltaTime * _damping);
    }

    [ProPlayButton]
    public void MakeShake() => MakeShake(15, 3);

    public void MakeShake(float amplitude, float duration)
    {
        _amplitude = amplitude;
        _duration = Mathf.Max(duration, 0.05f);
        _shakeTimer = 1;
    }

    [ProPlayButton]
    public void MakeRecoil()
    {
        MakeRecoil(-Vector3.right * Random.Range(_impulse * 0.5f, _impulse));
    }

    public void MakeRecoil(Vector3 impulse)
    {
        _recoilVelocity += impulse;
    }
}
