using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    public event UnityAction DownClicked;
    public event UnityAction<Vector2> PositionChanged;
    public event UnityAction UpClicked;

    private float _xAxis;
    private float _yAxis;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            DownClicked?.Invoke();

        if (Input.GetMouseButton(0))
        {
            _xAxis += Input.GetAxis("Mouse Y");
            _yAxis += Input.GetAxis("Mouse X");
            PositionChanged?.Invoke(new Vector2(-_xAxis, _yAxis));
        }

        if (Input.GetMouseButtonUp(0))
            UpClicked?.Invoke();
    }
}