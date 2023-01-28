using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float _health;


    void Start()
    {
        _health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void FixedUpdate()
    {
        if(_health <= 0)
            Destroy(gameObject);
    }


    public void Damage(float amount)
    {
        _health -= amount;
        healthBar.SetHealth(_health);
    }
}
