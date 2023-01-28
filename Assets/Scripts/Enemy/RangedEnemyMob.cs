using DG.Tweening;
using Pathfinding;
using UnityEngine;

[RequireComponent(typeof(EnemyMobHealthManager))]
public class RangedEnemyMob : MonoBehaviour
{
    [SerializeField] private float projectileSpeed;
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject shootSpawn;
    [SerializeField] private float projectileDamage = 10f;
    
    [SerializeField] private float attackCooldown;
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
            var range = 0.5f;
            var rand = Random.Range(-range, range);
            var attackCooldownRand = attackCooldown + rand;
            InvokeRepeating(nameof(Attack), attackCooldown, attackCooldownRand);
        }
        else if(!_agent.reachedDestination)
        {
            _isAttacking = false;
            CancelInvoke(nameof(Attack));
        }
        
        if(PauseMenu.Instance.IsPaused)
            return;
        
        var rotationDirection = player.position - transform.position;
        var rotationZ = Mathf.Atan2(rotationDirection.y, rotationDirection.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(0f, 0f , rotationZ);
    }

    private void Attack()
    {
        if(!_isAttacking || PauseMenu.Instance.IsPaused)
            return;
        var projectileRotationZ = Mathf.Atan2(player.position.y, player.position.x) * Mathf.Rad2Deg - 90;
        var projectileRotation = Quaternion.Euler(0f, 0f, projectileRotationZ);
        var newProjectile = Instantiate(projectile, shootSpawn.transform.position, projectileRotation);
        
        newProjectile.GetComponent<ProjectileFlyStraight>().speed = projectileSpeed;
        newProjectile.GetComponent<EnemyBulletCollider>().damage = projectileDamage;
    }
}
