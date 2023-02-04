using UnityEngine;

[RequireComponent(typeof(EnemyMobHealthManager))]
public class RushEnemyMob : BaseMovement, IEnemy
{
    [SerializeField] private float rushSpeed = 10f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float attackCoolDownMin = 5f;
    [SerializeField] private float attackCoolDownMax = 10f;
    [SerializeField] private float howLongPreparingAttackTakes = 2f;
    [SerializeField] private float chargeLength = 1f;
    [SerializeField] private float stunLength = 2f;
    [SerializeField] private float distanceFromPlayerToStop = 10f;

    private float attackCooldownCur;
    private float preparingAttackTimer;
    private float chargeTimer;
    private float stunTimer;
    private string currentState = "Moving";

    
    private void Awake()
    {
        attackCooldownCur = Random.Range(attackCoolDownMin, attackCoolDownMax);
        chargeTimer = chargeLength;
        preparingAttackTimer = howLongPreparingAttackTakes;
        stunTimer = stunLength;

        AssignPlayerTransform();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(PauseMenu.Instance.IsPaused)
            return;

        ManageUnitAttackStages(currentState);
    }

    private void ManageUnitAttackStages(string currentState)
    {
        switch (currentState)
        {
            case "Moving":
                SetRotation(true);
                EnableMovement();
                HandleAttackLogic();
                MoveTowardsPlayer(distanceFromPlayerToStop);
                break;
            case "Preparing attack":
                StopMovement();
                PrepareAttackTimerCounter();
                break;
            case "Attack":
                Attack();
                break;
            case "Charge":
                SetRotation(false);
                ChargeTimerCounter();
                break;
            case "Stun":
                StunTimerCounter();
                break;
        }
    }

    public void HandleAttackLogic()
    {
        if(attackCooldownCur <= 0)
        {
            currentState = "Preparing attack";
            preparingAttackTimer = howLongPreparingAttackTakes;
        }
        else
        {
            attackCooldownCur -= Time.deltaTime;
        }
    }

    public void Attack()
    {
        //Vector3 playerVelocity = playerTransform.GetComponent<Rigidbody2D>().velocity;
        //Vector3 targetPosition = playerTransform.position + playerVelocity.normalized * 5; 

        //RotateTowards(targetPosition);

        rb.velocity = (transform.rotation * Vector2.up) * rushSpeed * 2;

        currentState = "Charge"; 
    }


    private void PrepareAttackTimerCounter()
    {
        if (preparingAttackTimer <= 0)
        {
            currentState = "Attack";
        }
        else
        {
            preparingAttackTimer -= Time.deltaTime;
        }
    }

    private void ChargeTimerCounter()
    {
        if (chargeTimer <= 0)
        {
            currentState =  "Stun";
            chargeTimer = chargeLength;
        }
        else
        {
            chargeTimer -= Time.deltaTime;
        }
    }
    
    private void StunTimerCounter()
    {
        if (stunTimer <= 0)
        {
            currentState = "Moving";

            EnableMovement();
            attackCooldownCur = Random.Range(attackCoolDownMin, attackCoolDownMax);
            stunTimer = stunLength;
        }
        else
        {
            Stun();
            stunTimer -= Time.deltaTime;
        }
    }

    public void Stun()
    {
        SetRotation(false);
        StopMovement();
    }

    void SetRotation(bool enableRotation)
    {
        SetCanRotate(enableRotation);
        GetComponent<FacePlayer>().enabled = enableRotation;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (currentState == "Charge")
        {
            if (other.gameObject.CompareTag("Player"))
            {
                    Vector3 knockbackDirection = (other.transform.position - transform.position).normalized;
                    float knockback = 10;
                    other.gameObject.GetComponent<Rigidbody2D>().AddForce(knockbackDirection * knockback, ForceMode2D.Impulse);
                    
                    other.gameObject.GetComponent<IDamageable>().Damage(damage);
                    currentState =  "Stun";
            }
        }
    }
}
