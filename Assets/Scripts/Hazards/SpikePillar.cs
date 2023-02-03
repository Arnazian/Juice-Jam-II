using UnityEngine;

public class SpikePillar : MonoBehaviour
{
    [SerializeField] private float damage = 30;
    [SerializeField] private float damageCooldown = 1.5f;

    private float _damageTimer;

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
