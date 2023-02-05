using UnityEngine.UI;

public class PlayersHealth : HealthManager
{
    private bool isImmune = false;

    private bool _isDead = false;
    public bool IsDead => _isDead;
    
    protected override void Awake()
    {
        healthBarFill = UIManager.Instance.GetPlayerHealthBarFill;
        healthBarFill.maxValue = maxHealth;
        healthBarFill.value = currentHealth;
        UIManager.Instance.GetGameOverScreen.SetActive(false);
        _isDead = false;
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
        _isDead = true;
        Destroy(gameObject);
    }

    public void SetImmuneStatus(bool newStatus)
    {
        isImmune = newStatus;
    }

    public float GetHealth => currentHealth;
}
