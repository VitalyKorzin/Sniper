using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _movementSpeed;
    [SerializeField] private BlastWave _tempalte;
    [SerializeField] private ParticleSystem _splash;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out DamageablePartOfGiant damageablePartOfGiant))
        {
            Instantiate(_tempalte, collision.GetContact(0).point, Quaternion.identity, damageablePartOfGiant.transform).Initialize(damageablePartOfGiant);
            Instantiate(_splash, collision.GetContact(0).point, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.Translate(_movementSpeed * Time.deltaTime * Vector3.up);
    }
}