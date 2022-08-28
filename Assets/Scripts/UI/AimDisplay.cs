using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AimDisplay : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _appearanceDuration;
    [Min(0)]
    [SerializeField] private float _fadingDuration;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Image _bigTarget;
    [SerializeField] private Image _mask;
    [SerializeField] private Image _smallTarget;
    [SerializeField] private CameraMover _cameraMover;

    private readonly float _maskAppearanceEndValue = 0.7f;
    private readonly float _fadingEndValue = 0f;
    private readonly float _appearanceEndValue = 1f;

    private void OnEnable()
    {
        _cameraMover.EndWaypointReached += OnEndWaypointReached;
        _smallTarget.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _cameraMover.EndWaypointReached -= OnEndWaypointReached;
        _playerInput.DownClicked -= OnDownClicked;
        _playerInput.UpClicked -= OnUpClicked;
    }

    private void OnEndWaypointReached()
    {
        _playerInput.DownClicked += OnDownClicked;
        _playerInput.UpClicked += OnUpClicked;
        _smallTarget.gameObject.SetActive(true);
    }

    private void OnDownClicked()
    {
        _bigTarget.DOFade(_appearanceEndValue, _appearanceDuration);
        _mask.DOFade(_maskAppearanceEndValue, _appearanceDuration);
        _smallTarget.DOFade(_fadingEndValue, _fadingDuration);
    }

    private void OnUpClicked()
    {
        _bigTarget.DOFade(_fadingEndValue, _fadingDuration);
        _mask.DOFade(_fadingEndValue, _fadingDuration);
        _smallTarget.DOFade(_appearanceEndValue, _appearanceDuration);
    }
}