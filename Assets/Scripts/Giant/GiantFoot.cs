using UnityEngine;
using UnityEngine.Events;

public class GiantFoot : MonoBehaviour
{
    public event UnityAction TouchedGround;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Ground _))
            TouchedGround?.Invoke();
    }
}