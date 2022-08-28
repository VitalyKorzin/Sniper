using UnityEngine;

public class Bone : MonoBehaviour
{
    [SerializeField] private Transform _targetParent;

    public void BindWithTargetParent() 
        => transform.parent = _targetParent;
}