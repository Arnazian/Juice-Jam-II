public class PlayersHealth : HealthManager
{
    private bool isImmune = false;

    protected override void Awake()
    {
        healthBarFill = UIManager.Instance.GetPlayerHealthBarFill;
        UIManager.Instance.GetGameOverScreen.SetActive(false);
        base.Awake();
    }

    public override void Damage(float amount)
    {
        if(!isImmune)
            base.Damage(amount * WaveManager.Instance.GetCurrentDifficultySetting.damageTakenMultiplier);
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
        UIManager.Instance.GetGameOverScreen.SetActive(true);
        Destroy(gameObject);
    }

    public void SetImmuneStatus(bool newStatus)
    {
        isImmune = newStatus;
    }

    public float GetHealth => currentHealth;
}
