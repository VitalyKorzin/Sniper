using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PartOfGiant : MonoBehaviour
{
    [SerializeField] private List<Bone> _bones;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
    }

    public void TearOff()
    {
        foreach (var bone in _bones)
            bone.BindWithTargetParent();

        transform.parent = null;
        _rigidbody.isKinematic = false;
    }
}