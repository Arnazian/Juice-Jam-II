using UnityEngine;
using UnityEngine.UI;

public class BossHealthManager : MonoBehaviour, IDamageable
{
    private Slider bossHealthSlider;
    [SerializeField] private float bossMaxHealth;

    private float bossCurHealth;    
    private BossStateController bossStateController;
    
    private void Start()
    {
        bossHealthSlider = UIManager.Instance.GetBossHealthBar;
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
        bossCurHealth -= damageAmount;
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
    
