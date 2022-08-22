using UnityEngine;

public class PartOfGiant : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Bullet _))
            Debug.Log(true);
    }
}