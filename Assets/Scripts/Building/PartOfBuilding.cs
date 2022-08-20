using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PartOfBuilding : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        _rigidbody.isKinematic = false;
    }
}