using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VampireFinisher : MonoBehaviour
{
    [SerializeField] private BloodCheckCollider bloodCheckCollider;
    [SerializeField] private float healAmount;
    [SerializeField] private float launchSpeed;
    [SerializeField] private float suckBloodDuration;
    private Slider rageMeter;
    [SerializeField]private float rageAmountMax;
    private float rageAmountCur;
    private Collider2D playerCollision;

    private bool suckingBlood = false;
    private bool enableLaunching = false;


    private PlayersHealth playerHealth;
    void Start()
    {
        rageMeter = UIManager.Instance.GetRageMeter;
        rageAmountCur = 0;
        rageMeter.maxValue = rageAmountMax;
        rageMeter.value = rageAmountCur;
        playerHealth = GetComponent<PlayersHealth>();
        playerCollision = GetComponent<Collider2D>();
    }

   
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            StartFinisher();
        }
        MoveToTarget();
    }

    void StartFinisher()
    {
        if(rageAmountCur < rageAmountMax || bloodCheckCollider.GetSelectedEnemy() == null)
        {
            Debug.Log("Not Enough Rage || Not close enough to enemy");
            return;
        }

        GetComponent<MovePlayer>().SetCanMove(false);
        playerHealth.SetImmuneStatus(true);
        playerCollision.enabled = false;
        enableLaunching = true;
    }

    IEnumerator SuckBlood()
    {
        
        yield return new WaitForSeconds(suckBloodDuration);        
        playerCollision.enabled = true;
        enableLaunching = false;
        GetComponent<MovePlayer>().SetCanMove(true);
        playerHealth.SetImmuneStatus(false);
        playerHealth.Heal(healAmount);

        GameObject go = bloodCheckCollider.GetSelectedEnemy();
        go.GetComponent<EnemyMobHealthManager>().RunEnemyDeath();
        suckingBlood = false;
        SetRageAmount(0);
    }

    void MoveToTarget()
    {
        if (!enableLaunching) { return; }

        if (Vector3.Distance(transform.position, bloodCheckCollider.GetSelectedEnemy().transform.position) > 0.2f)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                bloodCheckCollider.GetSelectedEnemy().transform.position, launchSpeed * Time.deltaTime);
        }
        else
        {
            if (suckingBlood) { return; }
            suckingBlood = true;
            StartCoroutine(SuckBlood());
        }
    }
    void CheckRageMeter()
    {
        rageMeter.value = rageAmountCur;
        if(rageAmountCur >= rageAmountMax)
        {
            bloodCheckCollider.SetBloodCheckColliderStatus(true);
        }
        else
        {
            bloodCheckCollider.SetBloodCheckColliderStatus(false);
            bloodCheckCollider.UnselectEnemy(); 
        }
    }

    #region Increase Decrease And Set Rage
    public void IncreaseRage(float amount) 
    { 
        rageAmountCur += amount;
        CheckRageMeter();
    }
    public void DecreaseRage(float amount) 
    { 
        rageAmountCur -= amount;
        CheckRageMeter();
    }

    public void SetRageAmount(float amount)
    {
        rageAmountCur = amount;
        CheckRageMeter();
    }
    #endregion

}
