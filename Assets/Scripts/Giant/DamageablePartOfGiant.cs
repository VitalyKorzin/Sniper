using System;
using System.Collections.Generic;
using UnityEngine;

public class DamageablePartOfGiant : MonoBehaviour
{
    [SerializeField] private Giant _giant;
    [SerializeField] private List<Ball> _balls;

    public int Balls => _balls.Count;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Bullet _))
            _giant.ApplyOneHit();
    }

    public void TearOffAllBalls()
    {
        if (Balls == 0)
            return;

        foreach (var ball in _balls)
            ball.TearOff();

        _balls.Clear();
    }

    public void TearOff(Ball ball)
    {
        if (ball == null)
            throw new ArgumentNullException(nameof(ball));

        if (Contains(ball) == false)
            throw new InvalidOperationException();

        _balls.Remove(ball);
        ball.TearOff();
    }

    public bool Contains(Ball ball)
    {
        if (ball == null)
            throw new ArgumentNullException(nameof(ball));

        return _balls.Contains(ball);
    }
}