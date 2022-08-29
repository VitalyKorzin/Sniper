using UnityEngine;
using UnityEngine.Events;

public class Giant : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private int _health;
    [SerializeField] private PartOfGiant[] _parts;
    [SerializeField] private DamageablePartOfGiant[] _controllers;
    [SerializeField] private PartOfGiantRenderer[] _renderers;

    public bool Dying => _health == 1;
    public bool Died => _health == 0;

    public event UnityAction TookDamage;

    public void ApplyOneHit()
    {
        _health--;
        TookDamage?.Invoke();

        if (Dying)
            TearOffAllBalls();

        if (Died)
            Die();
    }

    private void TearOffAllBalls()
    {
        foreach (var controller in _controllers)
            controller.TearOffAllBalls();
    }

    private void Die()
    {
        foreach (var part in _parts)
            part.TearOff();

        foreach (var renderer in _renderers)
            renderer.Draw();

        Destroy(gameObject);
    }
}