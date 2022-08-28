using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class BlastWave : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _spreadingSpeed;
    [Min(0)]
    [SerializeField] private float _maximumRadius;

    private SphereCollider _collider;
    private DamageablePartOfGiant _damageablePartOfGiant;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Ball ball))
        {
            if (_damageablePartOfGiant.Contains(ball))
                _damageablePartOfGiant.TearOff(ball);
        }
    }

    public void Initialize(DamageablePartOfGiant damageablePartOfGiant)
    {
        if (damageablePartOfGiant == null)
            throw new ArgumentNullException(nameof(damageablePartOfGiant));

        _damageablePartOfGiant = damageablePartOfGiant;
        _collider = GetComponent<SphereCollider>();
        StartCoroutine(Spread());
    }

    private IEnumerator Spread()
    {
        while (_damageablePartOfGiant.Balls != 0)
        {
            _collider.radius = Mathf.Lerp(_collider.radius, _maximumRadius, _spreadingSpeed * Time.deltaTime);
            yield return null;
        }

        Destroy(gameObject);
    }
}