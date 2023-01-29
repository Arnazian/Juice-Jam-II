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
        if (GetComponent<Dash>().isDashing)
            return;

        _healthBar.Damage(amount);
    }
}
