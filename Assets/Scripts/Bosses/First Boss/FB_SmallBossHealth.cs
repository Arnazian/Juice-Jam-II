using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FB_SmallBossHealth : MonoBehaviour, IDamageable
{
    private float myHealthCur;
    private float myHealthMax;
    private BossStateController bossStateController;
    private BossHealthManager bossHealthManager;
    [SerializeField] private Slider healthBar;
    [SerializeField] private GameObject deathParticles;

    void Start()
    {
     
        bossHealthManager = FindObjectOfType<BossHealthManager>();
        bossStateController = FindObjectOfType<BossStateController>();
        CalculateMyHealth();
        myHealthCur = myHealthMax;
        healthBar.maxValue = myHealthMax;
        healthBar.value = myHealthCur;
    }

    void CalculateMyHealth()
    {
        float stageTwoThreshold = bossStateController.GetStageThresholds()[1];
        float stageThreeThreshold = bossStateController.GetStageThresholds()[2];

        myHealthMax = (stageTwoThreshold - stageThreeThreshold) / 3;
        myHealthCur = myHealthMax;
    }
    public void Damage(float damage)
    {
        myHealthCur -= damage;
        healthBar.value = myHealthCur;

        if(myHealthCur <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        bossHealthManager.Damage(myHealthMax);
        Destroy(gameObject);
    }
}
