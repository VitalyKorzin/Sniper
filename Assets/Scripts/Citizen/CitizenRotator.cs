using UnityEngine;

public class CitizenRotator : MonoBehaviour
{
    public void Rotate(Vector3 diraction)
    {
        transform.rotation = Quaternion.LookRotation(diraction);
    }
}