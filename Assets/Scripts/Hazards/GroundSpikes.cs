using System;
using UnityEngine;

public class GroundSpikes : MonoBehaviour
{
    [SerializeField] private float damage = 30;
    [SerializeField] private float damageCooldown = 0.25f;
    [SerializeField] private float timeUntilActive = 1f;

    private float _damageTimer;
    private float _activeTimer;

    private void Awake()
    {
        _activeTimer = timeUntilActive;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent<IDamageable>(out var damageable))
        {
            if (_activeTimer <= 0)
            {
                if (_damageTimer <= 0)
                {
                    damageable.Damage(damage);
                    _damageTimer = damageCooldown;
                }
                else
                    _damageTimer -= Time.deltaTime;
            }
            else
                _activeTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _damageTimer = damageCooldown;
        _activeTimer = timeUntilActive;
    }
}
