using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _movementSpeed;
    [SerializeField] private BlastWave _tempalte;

    private Vector3 _targetPosition;

    public void Initialize(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out DamageablePartOfGiant damageablePartOfGiant))
        {
            Instantiate(_tempalte, collision.GetContact(0).point, Quaternion.identity, damageablePartOfGiant.transform).Initialize(damageablePartOfGiant);
            Destroy(gameObject);
        }

        
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _movementSpeed * Time.deltaTime);
    }
}