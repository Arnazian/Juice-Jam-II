using UnityEngine;

[RequireComponent(typeof(EnemyMobHealthManager))]
public class RushEnemyMob : BaseMovement, IEnemy
{
    [SerializeField] private float rushSpeed;
    [SerializeField] private float damage = 10f;

    [SerializeField] private float attackCoolDownMin;
    [SerializeField] private float attackCoolDownMax;
    private float attackCooldownCur;

    [Header("Movement Variables")]
    [SerializeField] private float distanceFromPlayerToStop;

    
    private void Awake()
    {
        AssignPlayerTransform();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(PauseMenu.Instance.IsPaused)
            return;
        

        MoveTowardsPlayer(distanceFromPlayerToStop);

        HandleAttackLogic();
    }

    public void HandleAttackLogic()
    {
        if(attackCooldownCur <= 0)
        {
            PrepareAttack();
        }
        else
        {
            attackCooldownCur -= Time.deltaTime;
        }

    }


    public void Attack()
    {
        Vector3 playerVelocity = playerTransform.GetComponent<Rigidbody2D>().velocity;
        Vector3 targetPosition = playerTransform.position + playerVelocity.normalized * 5; 

        attackCooldownCur = Random.Range(attackCoolDownMin, attackCoolDownMax);

        //RotateTowards(targetPosition);

        rb.AddRelativeForce(Vector2.up*rushSpeed*1500);
        Debug.Log("I'm attacking");
    }

    private void StopMooving()
    {
        SetCanMove(false);
    }

    private void PrepareAttack()
    {
        StopMooving();

        Attack();
    }
}
