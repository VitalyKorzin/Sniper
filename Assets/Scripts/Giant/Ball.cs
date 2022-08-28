using System.Collections;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody), typeof(MeshRenderer))]
public class Ball : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _movementSpeed;
    [Min(0)]
    [SerializeField] private float _movementDuration;
    [Min(0)]
    [SerializeField] private float _targetScaleAtIncreasing;
    [Min(0)]
    [SerializeField] private float targetScaleAtDecreasing;
    [Min(0)]
    [SerializeField] private float _delayBeforeDestroying;
    [Min(0)]
    [SerializeField] private float _delayBeforeDecreasing;
    [Min(0)]
    [SerializeField] private float _materialChangingSpeed;
    [SerializeField] private Material _targetMaterial;
    [SerializeField] private Transform _center;

    private Rigidbody _rigidbody;
    private Material _currentMaterial;
    private Vector3 _directionFromCenter;

    private void Awake()
    {
        _currentMaterial = GetComponent<MeshRenderer>().material;
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
    }

    public void TearOff()
    {
        transform.DOScale(_targetScaleAtIncreasing, _movementDuration);
        StartCoroutine(ChangeMaterial());
        StartCoroutine(Move());
    }

    private IEnumerator ChangeMaterial()
    {
        while (_currentMaterial != _targetMaterial)
        {
            _currentMaterial.Lerp(_currentMaterial, _targetMaterial, _materialChangingSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator Move()
    {
        float elapsedTime = 0f;

        while (elapsedTime < _movementDuration)
        {
            _directionFromCenter = -(_center.position - transform.position).normalized;
            transform.Translate(_movementSpeed * Time.deltaTime * _directionFromCenter);
            transform.rotation = Quaternion.Euler(Vector3.zero);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.parent = null;
        _rigidbody.isKinematic = false;
        _rigidbody.velocity = _directionFromCenter * _movementSpeed;
        StartCoroutine(Destroy());
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(_delayBeforeDecreasing);
        transform.DOScale(targetScaleAtDecreasing, _delayBeforeDestroying);
        yield return new WaitForSeconds(_delayBeforeDestroying);
        Destroy(gameObject);
    }
}