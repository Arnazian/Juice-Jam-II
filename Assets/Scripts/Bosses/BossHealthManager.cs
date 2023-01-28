using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthManager : MonoBehaviour
{
    [SerializeField] private int bossMaxHealth;
    [SerializeField] private Slider bossHealthSlider;
    [SerializeField] private int amountOfStages;

    private int bossCurHealth;

    private void Start()
    {
        bossCurHealth = bossMaxHealth;
        bossHealthSlider.maxValue = bossMaxHealth;
        bossHealthSlider.value = bossCurHealth; 
    }
    public void TakeDamage(int damage)
    {
        bossCurHealth -= damage;
        bossHealthSlider.value = bossCurHealth;
    }


}
    

