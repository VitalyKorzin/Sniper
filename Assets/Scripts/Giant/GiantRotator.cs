using UnityEngine;

public class GiantRotator : MonoBehaviour
{
    public void Rotate(Vector3 diraction)
    {
        transform.rotation = Quaternion.LookRotation(diraction);
    }
}