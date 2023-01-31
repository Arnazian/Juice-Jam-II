using System;
using UnityEngine;

public class DamagePlayerOnTouch : MonoBehaviour
{
    [SerializeField] private float damage = 10;
    [SerializeField] private float damageCooldown = 0.5f;
    
    private float _timer;
    
    private PlayersHealth _playerHealth;
    
    private void Awake()
    {
        _playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayersHealth>();
        _timer = damageCooldown;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.transform.CompareTag("Player") && _timer <= damageCooldown)
        {
            _timer = damageCooldown;
            _playerHealth.Damage(damage);
        }
        else
            _timer -= Time.deltaTime;
    }
}
