using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _duration;
    [Min(0)]
    [SerializeField] private float _magnitude;
    [Min(0.1f)]
    [SerializeField] private float _range;
    [SerializeField] private CameraMover _cameraMover;
    [SerializeField] private GiantFoot[] _giantFoots;

    private Coroutine _shakingJob;
    private Vector3 _originalPosition;

    private void OnEnable() 
        => _cameraMover.EndWaypointReached += OnEndWaypointReached;

    private void OnDisable()
    {
        _cameraMover.EndWaypointReached -= OnEndWaypointReached;

        foreach (GiantFoot foot in _giantFoots)
            foot.TouchedGround -= OnTouchedGround;
    }

    private void Awake() 
        => _originalPosition = transform.localPosition;

    private void OnEndWaypointReached()
    {
        foreach (GiantFoot foot in _giantFoots)
            foot.TouchedGround += OnTouchedGround;
    }

    private void OnTouchedGround()
    {
        if (_shakingJob != null)
            StopCoroutine(_shakingJob);

        _shakingJob = StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        float elapsedTime = 0f;
        float xAxis;
        float yAxis;

        while (elapsedTime < _duration)
        {
            xAxis = Random.Range(-_range, _range) * _magnitude;
            yAxis = Random.Range(-_range, _range) * _magnitude;
            transform.localPosition = new Vector3(xAxis, yAxis, _originalPosition.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = _originalPosition;
    }
}