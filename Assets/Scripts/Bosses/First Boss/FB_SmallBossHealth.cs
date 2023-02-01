using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FB_SmallBossHealth : MonoBehaviour, IDamageable
{
    private float myHealthCur;
    private float myHealthMax;
    private BossStateController bossStateController;
    private BossHealthManager bossHealthManager;
    void Start()
    {
        bossHealthManager = FindObjectOfType<BossHealthManager>();
        bossStateController = FindObjectOfType<BossStateController>();
        CalculateMyHealth();
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
        if(myHealthCur <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        bossHealthManager.Damage(myHealthMax);
        Destroy(gameObject);
    }
}
