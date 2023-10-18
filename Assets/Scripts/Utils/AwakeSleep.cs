using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AwakeSleep : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Rigidbody>().Sleep();
    }
}
