using UnityEngine;

[RequireComponent(typeof(EnemyMobHealthManager))]
public class RushEnemyMob : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float rushSpeed;
    [SerializeField] private float damage = 10f;

    [SerializeField] private float attackCoolDownMin;
    [SerializeField] private float attackCoolDownMax;
    private float attackCooldownCur;

    public Transform player;

    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if(PauseMenu.Instance.IsPaused)
            return;
        

        HandleAttacking();
    }

    void HandleAttacking()
    {
        if(attackCooldownCur <= 0)
        {
            Attack();
        }
        else
        {
            attackCooldownCur -= Time.deltaTime;
        }

    }


    private void Attack()
    {
        Vector3 playerVelocity = player.GetComponent<Rigidbody2D>().velocity;
        Vector3 targetPosition = player.position + playerVelocity.normalized * 5; 

        attackCooldownCur = Random.Range(attackCoolDownMin, attackCoolDownMax);
        if( PauseMenu.Instance.IsPaused)
            return;
        var rotationDirection = targetPosition - transform.position;
        var projectileRotationZ = Mathf.Atan2(rotationDirection.y, rotationDirection.x) * Mathf.Rad2Deg - 90;
        var projectileRotation = Quaternion.Euler(0f, 0f, projectileRotationZ);

        rb.AddRelativeForce(Vector2.up*rushSpeed);
        Debug.Log("I'm attacking");
    }

    private void StopMooving()
    {
        //GetComponent<MoveTowardsPlayer>().enabled = false;
    }

    private void PrepareAttack()
    {
        StopMooving();

        Attack();
    }
}
