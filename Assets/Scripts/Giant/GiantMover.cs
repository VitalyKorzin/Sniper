using UnityEngine;

public class GiantMover : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _speed;
    [SerializeField] private Transform[] _path;
    [SerializeField] private GiantRotator _rotator;

    private readonly float _targetDistanceToWaypoint = 0.1f;

    private Vector3 _diraction;
    private int _currentWaypointIndex;

    private void Update() => Move();

    private void Move()
    {
        _diraction = (_path[_currentWaypointIndex].position - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, _path[_currentWaypointIndex].position, _speed * Time.deltaTime);
        _rotator.Rotate(_diraction);
        ChangeCurrentWaypointIndex();
    }

    private void ChangeCurrentWaypointIndex()
    {
        if (Vector3.Distance(transform.position, _path[_currentWaypointIndex].position) <= _targetDistanceToWaypoint)
        {
            _currentWaypointIndex++;

            if (_currentWaypointIndex == _path.Length)
                _currentWaypointIndex = 0;
        }
    }
}