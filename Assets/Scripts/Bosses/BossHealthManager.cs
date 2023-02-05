using UnityEngine;
using UnityEngine.UI;

public class BossHealthManager : MonoBehaviour, IDamageable
{
    private Slider bossHealthSlider;
    [SerializeField] private float bossMaxHealth;

    private float bossCurHealth;
    private BossStateController bossStateController;

    private VampireFinisher _vampireFinisher;
    
    private void Start()
    {
        bossHealthSlider = UIManager.Instance.GetBossHealthBar;
        _vampireFinisher = GameObject.FindWithTag("Player").GetComponent<VampireFinisher>();
        bossHealthSlider.gameObject.SetActive(true);
        bossCurHealth = bossMaxHealth;
        bossHealthSlider.maxValue = bossCurHealth;
        bossHealthSlider.value = bossCurHealth;
        bossStateController = GetComponent<BossStateController>();
    }

    public float GetHealth => bossCurHealth;
    public void SetHealth(float newHealthAmount)
    {
        bossCurHealth = newHealthAmount;
        UpdateHealthValues();
    }
    public void Damage(float damageAmount)
    {

        float rageAdjustedDamage = _vampireFinisher.GetRageAdjustedDamage(damageAmount);
        float difficultyAdjustedDamage = rageAdjustedDamage * WaveManager.Instance.GetCurrentDifficultySetting.damageMultiplier;

        bossCurHealth -= difficultyAdjustedDamage;

        bossStateController.CheckStageThreshold();
        UpdateHealthValues(); 
    }
    public void Heal(float healAmount)
    {
        bossCurHealth += healAmount;
        UpdateHealthValues();
    }

    void RunBossDeath()
    {
        WaveManager.Instance.EndBossFight();
        bossHealthSlider.transform.parent.gameObject.SetActive(false);
        Destroy(gameObject);
    }
    void UpdateHealthValues()
    {
        if(bossCurHealth <= 0)
        {
            RunBossDeath();
        }
        bossHealthSlider.value = bossCurHealth;

    }
}
    
