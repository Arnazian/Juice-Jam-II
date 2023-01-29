using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthManager : MonoBehaviour, IDamageable
{
    [SerializeField] private float bossMaxHealth;
    [SerializeField] private Slider bossHealthSlider;
    
  
 
    private float bossCurHealth;

    private BossStateController bossStateController;
    private void Start()
    {
        bossStateController = GetComponent<BossStateController>(); 
        bossCurHealth = bossMaxHealth;
        bossHealthSlider.maxValue = bossMaxHealth;
        bossHealthSlider.value = bossCurHealth; 
    }
    public void Damage(float damage)
    {
        bossCurHealth -= damage;
        bossHealthSlider.value = bossCurHealth;
        bossStateController.CheckStageThreshold();
    }

    public float GetHealth => bossCurHealth;
    public void SetHealth(float newHealth) { bossCurHealth = newHealth; }




}
    
