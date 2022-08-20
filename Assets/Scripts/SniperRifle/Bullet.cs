using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _movementSpeed;

    private Vector3 _targetPosition;

    public void Initialize(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _movementSpeed * Time.deltaTime);
    }
}