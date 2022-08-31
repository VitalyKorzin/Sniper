using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class PlayerInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private RectTransform _areaOfPerception;
    [SerializeField] private Canvas _canvas;

    private readonly float _delta = 0.2f;
    private readonly float _maximumMagnitude = 1f;
    private readonly float _moveThreshold = 0.9f;

    private RectTransform _rectTransform;
    private Vector2 _difference;
    private Vector2 _position;
    private Vector2 _radius;
    private Camera _camera;
    private float _xAxis;
    private float _yAxis;

    public Vector2 Value { get; private set; }
    public bool Used => Value != Vector2.zero;

    public event UnityAction DownClicked;
    public event UnityAction<Vector2> PositionChanged;
    public event UnityAction UpClicked;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _radius = _areaOfPerception.sizeDelta / 2;
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Used)
        {
            _xAxis += Value.y * _delta;
            _yAxis += Value.x * _delta;
            PositionChanged?.Invoke(new Vector2(-_xAxis, _yAxis));
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        DownClicked?.Invoke();
        _areaOfPerception.anchoredPosition = GetAnchoredPosition(eventData.position);
        _areaOfPerception.gameObject.SetActive(true);
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _position = RectTransformUtility.WorldToScreenPoint(_camera, _areaOfPerception.position);
        Value = (eventData.position - _position) / (_radius * _canvas.scaleFactor);
        Value = Vector2.ClampMagnitude(Value, _maximumMagnitude);

        if (Value.magnitude > _moveThreshold)
        {
            _difference = Value * (Value.magnitude - _moveThreshold) * _radius;
            _areaOfPerception.anchoredPosition += _difference;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _areaOfPerception.gameObject.SetActive(false);
        UpClicked?.Invoke();
        Value = Vector2.zero;
    }

    private Vector2 GetAnchoredPosition(Vector2 screenPosition)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, screenPosition, _camera, out Vector2 localPoint))
        {
            Vector2 pivotOffset = _rectTransform.pivot * _rectTransform.sizeDelta;
            return localPoint - (_areaOfPerception.anchorMax * _rectTransform.sizeDelta) + pivotOffset;
        }

        return Vector2.zero;
    }
}