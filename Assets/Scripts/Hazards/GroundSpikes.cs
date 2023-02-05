using System;
using UnityEngine;

public class GroundSpikes : MonoBehaviour
{
    [SerializeField] private float damage = 30;
    [SerializeField] private float damageCooldown = 0.25f;
    [SerializeField] private float timeUntilActive = 1f;

    private float _damageTimer;
    private float _activeTimer;

    private Animator _anim;
    private static readonly int Active = Animator.StringToHash("Active");

    private void Awake()
    {
        _activeTimer = timeUntilActive;
        _anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        _anim.SetBool(Active, true);
        Invoke(nameof(ResetSpikes), _anim.GetCurrentAnimatorClipInfo(0).Length);
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

    private void ResetSpikes()
    {
        _damageTimer = damageCooldown;
        _activeTimer = timeUntilActive;
        _anim.SetBool(Active, false);
    }
}
