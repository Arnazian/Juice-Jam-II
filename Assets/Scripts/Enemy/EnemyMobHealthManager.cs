using UnityEngine;
using UnityEngine.UI;

public class EnemyMobHealthManager : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private Image healthBarFill;

    private float _health;

    private void Awake()
    {
        _health = maxHealth;
        UpdateHealthBar();
    }

    private void Update()
    {
        if(_health <= 0)
            Destroy(gameObject);
    }

    private void UpdateHealthBar()
    {
        var fillAmount = _health / maxHealth;
        healthBarFill.fillAmount = fillAmount;
    }

    public void Damage(float amount)
    {
        _health -= amount;
        UpdateHealthBar();
    }
}
