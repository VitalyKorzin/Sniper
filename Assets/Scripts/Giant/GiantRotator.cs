using UnityEngine;

public class GiantRotator : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _speed;

    public void Rotate(Vector3 diraction)
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(diraction), _speed * Time.deltaTime);
    }
}