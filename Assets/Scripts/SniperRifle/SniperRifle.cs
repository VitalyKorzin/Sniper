using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SniperRifle : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _delay;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Bullet _template;
    [SerializeField] private Transform _shotPoint;
    [SerializeField] private MeshRenderer[] _meshRenderers;
    [SerializeField] private CameraMover _cameraMover;

    private Animator _animator;

    private void OnEnable()
    {
        _playerInput.DownClicked += OnDownClicked;
        _playerInput.UpClicked += OnUpClicked;
    }

    private void OnDisable()
    {
        _playerInput.DownClicked -= OnDownClicked;
        _playerInput.UpClicked -= OnUpClicked;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
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
        yield return new WaitForSeconds(_delay);
        EnableMesh(false);
    }

    private void EnableMesh(bool enabled)
    {
        foreach (MeshRenderer meshRenderer in _meshRenderers)
            meshRenderer.enabled = enabled;
    }

    private void Shoot()
    {
        Vector3 targetPosition = _cameraMover.GetTargetPosition();
        Instantiate(_template, _shotPoint.position, _shotPoint.rotation).Initialize(targetPosition);
    }
}