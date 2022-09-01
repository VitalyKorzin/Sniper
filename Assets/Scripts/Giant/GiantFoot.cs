using UnityEngine;
using UnityEngine.Events;

public class GiantFoot : MonoBehaviour
{
    [SerializeField] private ParticleSystem _slamWave;

    private Quaternion _targetSlamWaveRotation;

    public event UnityAction TouchedGround;

    private void Awake() 
        => _targetSlamWaveRotation = Quaternion.Euler(-90f, 0f, 0f);

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Ground _))
        {
            TouchedGround?.Invoke();
            Instantiate(_slamWave, other.ClosestPoint(transform.position), _targetSlamWaveRotation);
        }
    }
}