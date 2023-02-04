using System;
using DG.Tweening;
using UnityEngine;

public class SawMill : MonoBehaviour
{
    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float damage = 30;
    [SerializeField] private float damageCooldown = 1.5f;

    private float _damageTimer;

    private bool _isAtPointOne = false;
    
    private void Update()
    {
        if (!_isAtPointOne)
        {
            transform.DOMove(point1.position, movementSpeed).SetEase(Ease.Linear).OnComplete(() =>
            {
                _isAtPointOne = true;
            });
        }
        else
        {
            transform.DOMove(point2.position, movementSpeed).SetEase(Ease.Linear).OnComplete(() =>
            {
                _isAtPointOne = false;
            });
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out var damageable))
        {
            if (_damageTimer <= 0)
            {
                damageable.Damage(damage);
                _damageTimer = damageCooldown;
            }
            else
                _damageTimer -= Time.deltaTime;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        _damageTimer = 0f;
    }
}
