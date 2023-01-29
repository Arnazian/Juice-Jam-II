using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FB_SmallBossHealth : MonoBehaviour, IDamageable
{
    private float myHealth;
    private BossHealthManager bossHealthManager;
    void Start()
    {
        bossHealthManager = FindObjectOfType<BossHealthManager>();
    }

    
    void Update()
    {
        
    }

    void CalculateMyHealth()
    {
        float one = bossHealthManager.GetStageThresholds
    }
    public void Damage(float damage)
    {
        myHealth -= damage;
        if(myHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
