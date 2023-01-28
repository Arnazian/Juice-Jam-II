using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthManager : MonoBehaviour
{
    [SerializeField] private int bossMaxHealth;
    [SerializeField] private Slider bossHealthSlider;
    
  
 
    private int bossCurHealth;

    private BossStateController bossStateController;
    private void Start()
    {
        bossStateController = GetComponent<BossStateController>(); 
        bossCurHealth = bossMaxHealth;
        bossHealthSlider.maxValue = bossMaxHealth;
        bossHealthSlider.value = bossCurHealth; 
    }
    public void TakeDamage(int damage)
    {
        bossCurHealth -= damage;
        bossHealthSlider.value = bossCurHealth;
        bossStateController.CheckStageThreshold();
    }

    public int GetHealth => bossCurHealth;
    public void SetHealth(int newHealth) { bossCurHealth = newHealth; }




}
    
