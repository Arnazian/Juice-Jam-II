using UnityEngine;

[RequireComponent(typeof(EnemyMobHealthManager))]
public class EnemyMob : BaseMovement, IEnemy
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackDamage = 10f;
    private bool _isAttacking;

    [Header("Movement Variables")]
    [SerializeField] private float distanceFromPlayerToStop;

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

        if (canMove) { MoveTowardsPlayer(distanceFromPlayerToStop); }
        HandleAttackLogic();
    }

    public void HandleAttackLogic()
    {
        if (!IsMoving && !_isAttacking)
        {
            _isAttacking = true;
            var range = 0.5f;
            var rand = Random.Range(-range, range);
            var attackCooldownRand = attackCooldown + rand;
            InvokeRepeating(nameof(Attack), attackCooldown, attackCooldownRand);
        }
        else if(IsMoving)
        {
            _isAttacking = false;
            CancelInvoke(nameof(Attack));
        }

        _anim.SetBool(IsAttacking, _isAttacking);
    }

    public void Knockback()
    {

    }

    public void Stagger()
    {

    }
    public void Attack()
    {
        if(!_isAttacking || PauseMenu.Instance.IsPaused)
            return;
        playerTransform.GetComponent<IDamageable>().Damage(attackDamage);
    } 

}
