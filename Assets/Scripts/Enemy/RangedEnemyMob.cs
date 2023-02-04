using UnityEngine;

[RequireComponent(typeof(EnemyMobHealthManager))]
public class RangedEnemyMob : BaseMovement, IEnemy
{
    [SerializeField] private ParticleSystem _attackParticles;
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

    private Animator _anim;
    private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        AssignPlayerTransform();
    }

    private void FixedUpdate()
    {
        if(PauseMenu.Instance.IsPaused)
            return;

        if (canMove) { StayAwayFromPlayer(preferredDistance, preferredDistanceBuffer); }        
        HandleAttackLogic();
    }

    public void HandleAttackLogic()
    {
        if (attackCooldownCur <= 0)
        {
            attackCooldownCur = Random.Range(attackCoolDownMin, attackCoolDownMax);
            _anim.SetBool(IsAttacking, true);
            _attackParticles.Play(true);
        }
        else
        {
            attackCooldownCur -= Time.deltaTime;
            _anim.SetBool(IsAttacking, false);
        }
    }
    
    public void Attack()
    {
        if(PauseMenu.Instance.IsPaused)
            return;
        var rotationDirection = playerTransform.position - transform.position;
        var projectileRotationZ = Mathf.Atan2(rotationDirection.y, rotationDirection.x) * Mathf.Rad2Deg - 90;
        var projectileRotation = Quaternion.Euler(0f, 0f, projectileRotationZ);
        var newProjectile = Instantiate(projectile, shootSpawn.transform.position, projectileRotation);
        
        newProjectile.GetComponent<ProjectileFlyStraight>().speed = projectileSpeed;
        newProjectile.GetComponent<BulletCollider>().damage = projectileDamage;
    }
}
