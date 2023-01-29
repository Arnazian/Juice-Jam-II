using UnityEngine;

public class BossHealthManager : MonoBehaviour, IDamageable
{
    private HealthBar _healthBar;
    private BossStateController _bossStateController;
    
    private void Start()
    {
        _bossStateController = GetComponent<BossStateController>();
        _healthBar = UIManager.Instance.GetBossHealthBar;
    }
    
    public void Damage(float damage)
    {
        _healthBar.Damage(damage);
        _bossStateController.CheckStageThreshold();

        if (_healthBar.GetHealth <= 0)
            Destroy(gameObject);
    }

    public float GetHealth => _healthBar.GetHealth;

    public void SetHealth(float newHealth)
    {
        _healthBar.SetHealth(newHealth);
    }
}
    
