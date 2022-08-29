using System.Collections;
using UnityEngine;

public class CameraSight : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private int _targetFieldOfView;
    [Min(0)]
    [SerializeField] private int _defaultFieldOfView;
    [Min(0)]
    [SerializeField] private float _aimingSpeed;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Camera _target;
    [SerializeField] private CameraMover _cameraMover;

    private Coroutine _fieldOfViewChangingJob;

    private void OnEnable()
    {
        _cameraMover.EndWaypointReached += OnEndWaypointReached;
    }

    private void OnDisable()
    {
        _cameraMover.EndWaypointReached -= OnEndWaypointReached;
        _playerInput.DownClicked -= OnDownClicked;
        _playerInput.PositionChanged -= OnPositionChanged;
        _playerInput.UpClicked -= OnUpClicked;
    }

    private void OnEndWaypointReached()
    {
        _playerInput.DownClicked += OnDownClicked;
        _playerInput.PositionChanged += OnPositionChanged;
        _playerInput.UpClicked += OnUpClicked;
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