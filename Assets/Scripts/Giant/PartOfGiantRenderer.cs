using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SkinnedMeshRenderer))]
public class PartOfGiantRenderer : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _materialChangingSpeed;
    [SerializeField] private Material _targetMaterial;

    private Material _currentMaterial;
    private SkinnedMeshRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SkinnedMeshRenderer>();
        _currentMaterial = _renderer.material;
    }

    public void Draw() => StartCoroutine(ChangeMaterial());

    private IEnumerator ChangeMaterial()
    {
        while (_currentMaterial != _targetMaterial)
        {
            _currentMaterial.Lerp(_currentMaterial, _targetMaterial, _materialChangingSpeed * Time.deltaTime);
            yield return null;
        }
    }
}