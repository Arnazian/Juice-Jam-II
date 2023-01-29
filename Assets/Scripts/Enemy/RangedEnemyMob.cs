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
    public Transform player;

    private MoveTowardsPlayer _movement;
    private bool _isAttacking;

    private void Awake()
    {
        _movement = GetComponent<MoveTowardsPlayer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if(PauseMenu.Instance.IsPaused)
            return;
        
        var rotationDirection = player.position - transform.position;
        var rotationZ = Mathf.Atan2(rotationDirection.y, rotationDirection.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(0f, 0f , rotationZ);
        
        if (!_movement.IsMoving && !_isAttacking)
        {
            _isAttacking = true;
            var range = 0.5f;
            var rand = Random.Range(-range, range);
            var attackCooldownRand = attackCooldown + rand;
            InvokeRepeating(nameof(Attack), attackCooldown / 3, attackCooldownRand);
        }
        else if(_movement.IsMoving)
        {
            _isAttacking = false;
            CancelInvoke(nameof(Attack));
        }
    }

    private void Attack()
    {
        if(!_isAttacking || PauseMenu.Instance.IsPaused)
            return;
        var rotationDirection = player.position - transform.position;
        var projectileRotationZ = Mathf.Atan2(rotationDirection.y, rotationDirection.x) * Mathf.Rad2Deg - 90;
        var projectileRotation = Quaternion.Euler(0f, 0f, projectileRotationZ);
        var newProjectile = Instantiate(projectile, shootSpawn.transform.position, projectileRotation);
        
        newProjectile.GetComponent<ProjectileFlyStraight>().speed = projectileSpeed;
        newProjectile.GetComponent<BulletCollider>().damage = projectileDamage;
    }
}
