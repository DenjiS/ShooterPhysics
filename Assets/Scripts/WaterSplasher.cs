using System.Drawing;
using UnityEngine;

public class WaterSplasher : MonoBehaviour
{
    [SerializeField] ParticleSystem _splashPrefab;
    [SerializeField] float _splashDuration;

    private Transform _transform;

    void Awake()
    {
        _transform = transform;
    }

    public void TryCreateWaterSplash(Vector3 startPoint, Vector3 endpoint)
    {
        if (CheckWater(endpoint))
        {
            Vector3 point = RaycastToVirtualPlane(startPoint, endpoint - startPoint);
            Debug.Log(point);

            ParticleSystem splash = Instantiate(_splashPrefab, point, Quaternion.identity, null);
            splash.Play();
            Destroy(splash.gameObject, _splashDuration);
        }
    }

    private bool CheckWater(Vector3 endpoint)
    {
        return endpoint.y < _transform.position.y;
    }

    private Vector3 RaycastToVirtualPlane(Vector3 startPoint, Vector3 direction)
    {
        Plane plane = new(Vector3.up, _transform.position);
        Debug.Log(plane);
        Ray ray = new(startPoint, direction);
        Debug.Log(ray);

        if (plane.Raycast(ray, out float enter))
            return (startPoint + direction.normalized * enter);

        return Vector3.zero;
    }
}
