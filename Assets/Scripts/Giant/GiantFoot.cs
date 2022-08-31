using UnityEngine;
using UnityEngine.Events;

public class GiantFoot : MonoBehaviour
{
    [SerializeField] private ParticleSystem _slamWave;

    public event UnityAction TouchedGround;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Ground _))
        {
            TouchedGround?.Invoke();
            Instantiate(_slamWave, other.ClosestPoint(transform.position), Quaternion.Euler(-90f, 0f, 0f));
        }
    }
}