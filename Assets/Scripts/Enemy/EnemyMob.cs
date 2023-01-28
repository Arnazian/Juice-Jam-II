using Pathfinding;
using UnityEngine;

[RequireComponent(typeof(EnemyMobHealthManager))]
public class EnemyMob : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private Transform player;

    private IAstarAI _agent;
    private bool _isAttacking;

    private void Awake()
    {
        _agent = GetComponent<IAstarAI>();
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
    }

    private void Attack()
    {
        if(!_isAttacking || PauseMenu.Instance.IsPaused)
            return;
        player.GetComponent<IDamageable>().Damage(attackDamage);
    }
}
