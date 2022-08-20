using System.Collections;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private int _targetFieldOfView;
    [Min(0)]
    [SerializeField] private int _defaultFieldOfView;
    [Min(0)]
    [SerializeField] private float _aimingSpeed;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Camera _target;

    private Coroutine _fieldOfViewChangingJob;

    private void OnEnable()
    {
        _playerInput.DownClicked += OnDownClicked;
        _playerInput.PositionChanged += OnPositionChanged;
        _playerInput.UpClicked += OnUpClicked;
    }

    private void OnDisable()
    {
        _playerInput.DownClicked -= OnDownClicked;
        _playerInput.PositionChanged -= OnPositionChanged;
        _playerInput.UpClicked -= OnUpClicked;
    }

    public Vector3 GetTargetPosition()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit raycastHit;
        Vector3 targetPosition = Vector3.zero;

        if (Physics.Raycast(ray, out raycastHit, 100f))
        {
            targetPosition = raycastHit.point;
        }
        else
        {
            targetPosition = ray.GetPoint(100f);
        }

        return targetPosition;
    }

    private void OnDownClicked()
    {
        StartChangeFieldOfView(ChangeFieldOfView(_targetFieldOfView));
    }

    private void OnPositionChanged(Vector2 mousePosition)
    {
        _target.transform.rotation = Quaternion.Euler(mousePosition);
    }

    private void OnUpClicked()
    {
        StartChangeFieldOfView(ChangeFieldOfView(_defaultFieldOfView));
    }

    private void StartChangeFieldOfView(IEnumerator job)
    {
        if (_fieldOfViewChangingJob != null)
            StopCoroutine(_fieldOfViewChangingJob);

        _fieldOfViewChangingJob = StartCoroutine(job);
    }

    private IEnumerator ChangeFieldOfView(int targetValue)
    {
        while (_target.fieldOfView != targetValue)
        {
            _target.fieldOfView = Mathf.Lerp(_target.fieldOfView, targetValue, _aimingSpeed);
            yield return null;
        }
    }
}