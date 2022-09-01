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

    public event UnityAction EndWaypointReached;

    private void Start() => StartCoroutine(Move());

    private IEnumerator Move()
    {
        Sequence sequence = DOTween.Sequence();
        var delay = new WaitForSeconds(_delayBetweenTransitions);

        for (int waypointIndex = 0; waypointIndex < _path.Length; waypointIndex++)
        {
            yield return delay;
            sequence.Append(transform.DOMove(_path[waypointIndex].position, _movingDuration));
            sequence.Insert(waypointIndex, transform.DORotate(_path[waypointIndex].rotation.eulerAngles, _movingDuration));
        }

        yield return new WaitForSeconds(_delayBetweenTransitions / 2f);
        EndWaypointReached?.Invoke();
    }
}