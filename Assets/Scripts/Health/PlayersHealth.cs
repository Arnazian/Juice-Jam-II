using UnityEngine;

public class PlayersHealth : MonoBehaviour, IDamageable
{
    public float GetMaxHealth => _healthBar.GetMaxHealth;
    public float GetHealth => _healthBar.GetHealth;
    private HealthBar _healthBar;

    void Start()
    {
        _healthBar = UIManager.Instance.GetPlayerHealthBar;
    }

    void FixedUpdate()
    {
        if(_healthBar.GetHealth <= 0)
            Destroy(gameObject);
    }


    public void Damage(float amount)
    {
        if (GetComponent<Dash>().isDashing)
            return;

        _healthBar.Damage(amount);
    }

    public void SetHealth(float hp)
    {
        _healthBar.SetHealth(hp);
    }
}
