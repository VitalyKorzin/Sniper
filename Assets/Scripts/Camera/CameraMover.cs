using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.Events;

public class CameraMover : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _delayBetweenTransitions;
    [Min(0)]
    [SerializeField] private float _movingDuration;
    [SerializeField] private Transform[] _path;
    [SerializeField] private Camera _target;

    public event UnityAction EndWaypointReached;

    private void Start() => StartCoroutine(Move());

    private IEnumerator Move()
    {
        Sequence sequence = DOTween.Sequence();

        for (int waypointIndex = 0; waypointIndex < _path.Length; waypointIndex++)
        {
            sequence.Append(_target.transform.DOMove(_path[waypointIndex].position, _movingDuration));
            sequence.Insert(waypointIndex, _target.transform.DORotate(_path[waypointIndex].rotation.eulerAngles, _movingDuration));
            yield return new WaitForSeconds(_delayBetweenTransitions);
        }

        EndWaypointReached?.Invoke();
    }
}