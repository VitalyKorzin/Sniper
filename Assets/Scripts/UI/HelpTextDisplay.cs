using UnityEngine;
using DG.Tweening;
using TMPro;

public class HelpTextDisplay : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _pulsatingDuration;
    [Min(0)]
    [SerializeField] private float _targetScale;
    [Min(-1)]
    [SerializeField] private int _loops;
    [SerializeField] private LoopType _loopType;
    [SerializeField] private TMP_Text _value;
    [SerializeField] private Camera _camera;

    private Vector3 _worldPosition;
    private Vector3 _worldUp;

    private void Start() => StartPulsate();

    private void Update() => LookAtCamera();

    private void LookAtCamera()
    {
        _worldPosition = transform.position + _camera.transform.rotation * Vector3.forward;
        _worldUp = _camera.transform.rotation * Vector3.up;
        transform.LookAt(_worldPosition, _worldUp);
    }

    private void StartPulsate()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_value.rectTransform.DOScale(_targetScale, _pulsatingDuration));
        sequence.SetLoops(_loops, _loopType);
    }
}