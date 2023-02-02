using UnityEngine;

[RequireComponent(typeof(EnemyMobHealthManager))]
public class RangedEnemyMob : BaseMovement, IEnemy
{
    [SerializeField] private float projectileSpeed;
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject shootSpawn;
    [SerializeField] private float projectileDamage = 10f;

    [SerializeField] private float attackCoolDownMin;
    [SerializeField] private float attackCoolDownMax;
    private float attackCooldownCur;

    [Header("Movement Variables")]
    [SerializeField] private float preferredDistance;
    [SerializeField] private float preferredDistanceBuffer;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        AssignPlayerTransform();
    }

    private void FixedUpdate()
    {
        if(PauseMenu.Instance.IsPaused)
            return;
        

        StayAwayFromPlayer(preferredDistance, preferredDistanceBuffer);
        HandleAttackLogic();
    }

    public void HandleAttackLogic()
    {
        if(attackCooldownCur <= 0)
            Attack();
        else
            attackCooldownCur -= Time.deltaTime;
    }


    public void Attack()
    {
        attackCooldownCur = Random.Range(attackCoolDownMin, attackCoolDownMax);
        if( PauseMenu.Instance.IsPaused)
            return;
        var rotationDirection = playerTransform.position - transform.position;
        var projectileRotationZ = Mathf.Atan2(rotationDirection.y, rotationDirection.x) * Mathf.Rad2Deg - 90;
        var projectileRotation = Quaternion.Euler(0f, 0f, projectileRotationZ);
        var newProjectile = Instantiate(projectile, shootSpawn.transform.position, projectileRotation);
        
        newProjectile.GetComponent<ProjectileFlyStraight>().speed = projectileSpeed;
        newProjectile.GetComponent<BulletCollider>().damage = projectileDamage;
    }
}
