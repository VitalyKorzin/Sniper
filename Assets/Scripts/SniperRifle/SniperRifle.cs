using System.Collections;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Animator))]
public class SniperRifle : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _delayBeforeFading;
    [Min(0)]
    [SerializeField] private float _movementDuration;
    [SerializeField] private Transform _targetPlace;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Bullet _template;
    [SerializeField] private Transform _shotPoint;
    [SerializeField] private MeshRenderer[] _meshRenderers;
    [SerializeField] private CameraMover _cameraMover;

    private Animator _animator;

    private void OnEnable()
    {
        _cameraMover.EndWaypointReached += OnEndWaypointReached;
        _animator.enabled = false;
    }

    private void OnDisable()
    {
        _cameraMover.EndWaypointReached -= OnEndWaypointReached;
        _playerInput.DownClicked -= OnDownClicked;
        _playerInput.UpClicked -= OnUpClicked;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEndWaypointReached()
    {
        _playerInput.DownClicked += OnDownClicked;
        _playerInput.UpClicked += OnUpClicked;
        transform.DOMove(_targetPlace.position, _movementDuration);
        StartCoroutine(EnableAnimator());
    }

    private IEnumerator EnableAnimator()
    {
        yield return new WaitForSeconds(_movementDuration);
        _animator.enabled = true;
    }

    private void OnDownClicked()
    {
        _animator.SetTrigger(SniperRifleAnimator.Params.TookAim);
        StartCoroutine(Fade());
    }
    
    private void OnUpClicked()
    {
        _animator.SetTrigger(SniperRifleAnimator.Params.Fired);
        EnableMesh(true);
        Shoot();
    }

    private IEnumerator Fade()
    {
        yield return new WaitForSeconds(_delayBeforeFading);
        EnableMesh(false);
    }

    private void EnableMesh(bool enabled)
    {
        foreach (MeshRenderer meshRenderer in _meshRenderers)
            meshRenderer.enabled = enabled;
    }

    private void Shoot()
    {
        Instantiate(_template, _shotPoint.position, _shotPoint.rotation);
    }
}