using Pathfinding;
using UnityEngine;

public class EnemyMob : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100;
    
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private Transform player;

    private IAstarAI _agent;
    private bool _isAttacking;
    private float _health;

    private void Awake()
    {
        _agent = GetComponent<IAstarAI>();
        _health = maxHealth;
    }

    private void Update()
    {
        _agent.destination = player.position;
        _agent.canMove = !PauseMenu.Instance.IsPaused;
        if (_agent.reachedDestination && !_isAttacking)
        {
            _isAttacking = true;
            InvokeRepeating(nameof(Attack), attackCooldown, attackCooldown);
        }
        else if(!_agent.reachedDestination)
        {
            _isAttacking = false;
            CancelInvoke(nameof(Attack));
        }
        
        if(_health <= 0)
            Destroy(gameObject);
    }

    private void Attack()
    {
        if(!_isAttacking || PauseMenu.Instance.IsPaused)
            return;
        player.GetComponent<IDamageable>().Damage(attackDamage);
    }

    public void Damage(float amount)
    {
        _health -= amount;
    }
}
