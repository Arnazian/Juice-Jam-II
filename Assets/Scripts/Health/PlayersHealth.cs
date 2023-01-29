using UnityEngine;

public class PlayersHealth : MonoBehaviour, IDamageable
{
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
        _healthBar.Damage(amount);
    }
}
