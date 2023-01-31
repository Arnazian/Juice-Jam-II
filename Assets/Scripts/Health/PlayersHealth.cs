using UnityEngine.UI;

public class PlayersHealth : HealthManager
{
    private Slider playerHealthSlider;
    private bool isImmune = false;

    public override void Damage(float amount)
    {
        if(!isImmune)
            base.Damage(amount);
    }

    protected override void UpdateHealth()
    {
        base.UpdateHealth();
        if (currentHealth <= 0)
            RunPlayerDeath();
        if(currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    private void RunPlayerDeath()
    {
        Destroy(gameObject);
    }

    public void SetImmuneStatus(bool newStatus)
    {
        isImmune = newStatus;
    }

    public float GetHealth => currentHealth;
}
