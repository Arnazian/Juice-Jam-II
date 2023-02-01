using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class VampireFinisher : MonoBehaviour
{
    public bool CanSuckBlood => currentRage >= MaxRageAmount;
    
    [SerializeField] private GameObject bloodSuckParticles;
    [SerializeField] private GameObject bloodExplosionParticles;
    [SerializeField] private BloodCheckCollider bloodCheckCollider;
    [SerializeField] private float healAmount;
    [SerializeField] private float damageAmount;
    [SerializeField] private float launchSpeed;
    [SerializeField] private float suckBloodIntervalMax;
    private float suckBloodIntervalCur;
    [SerializeField] private float suckBloodDuration;
    [FormerlySerializedAs("rageAmountMax")] [SerializeField] private float MaxRageAmount;

    private Collider2D playerCollision;
    private GameObject myTarget;
    private bool suckingBlood = false;
    private bool enableLaunching = false;
    private PlayerActionManager playerActionManager;

    private PlayersHealth playerHealth;
    
    private Image rageMeterFill;
    private float currentRage;
    void Start()
    {
        suckBloodIntervalCur = 0;
        playerActionManager = GetComponent<PlayerActionManager>();
        bloodSuckParticles.SetActive(false);
        rageMeterFill = UIManager.Instance.GetRageMeterFill;
        UpdateGraphics();
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
        SuckBloodTimer();
    }

    void StartFinisher()
    {
        if(suckingBlood || currentRage < MaxRageAmount) 
            return;
        if(playerActionManager.CheckIfInAction()) { return; }
        if(bloodCheckCollider.GetSelectedEnemy() == null)
        {
            Debug.Log("Not close enough to enemy");
            return;
        }

        currentRage = 0f;
        CheckRageMeter();
        
        GetComponent<MovePlayer>().SetCanMove(false);
        playerActionManager.SetIsFinishing(true);
        playerHealth.SetImmuneStatus(true);
        playerCollision.enabled = false;
        myTarget = bloodCheckCollider.GetSelectedEnemy();
        enableLaunching = true;
    }
    void SuckBloodTimer()
    {
        if(!suckingBlood) { return; }
        if (suckBloodIntervalCur <= 0)
        {
            suckBloodIntervalCur = suckBloodIntervalMax;
            BloodSuckingInterval();
        }
        else
        {
            suckBloodIntervalCur -= Time.deltaTime;
        }
    }

    void BloodSuckingInterval()
    {
        playerHealth.Heal(healAmount);
        if(myTarget == null) 
        { 
            StopSuckingBlood();
            return;
        }
        myTarget.GetComponent<EnemyMobHealthManager>().Damage(damageAmount);
        
    }

    void StopSuckingBlood()
    {
        enableLaunching = false;
        suckingBlood = false;
        playerCollision.enabled = true;
       
        bloodSuckParticles.SetActive(false);
        playerActionManager.SetIsFinishing(false);

        GetComponent<MovePlayer>().SetCanMove(true);
        playerHealth.SetImmuneStatus(false);
    }

    void MoveToTarget()
    {
        if (!enableLaunching) { return; }
        if(myTarget == null) { return; }
        if (Vector3.Distance(transform.position, bloodCheckCollider.GetSelectedEnemy().transform.position) > 0.2f)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                myTarget.transform.position, launchSpeed * Time.deltaTime);
        }
        else
        {
            if (suckingBlood) { return; }
            suckingBlood = true;
        }
    }

    void CheckRageMeter()
    {
        UpdateGraphics();
        if(currentRage >= MaxRageAmount)
        {
        }
        else
        {
        }
    }

    private void UpdateGraphics()
    {
        var fillAmount = currentRage / MaxRageAmount;
        rageMeterFill.fillAmount = fillAmount;
    }

    #region Increase Decrease And Set Rage
    public void IncreaseRage(float amount) 
    { 
        currentRage += amount;
        CheckRageMeter();
    }
    public void DecreaseRage(float amount) 
    { 
        currentRage -= amount;
        CheckRageMeter();
    }

    public void SetRageAmount(float amount)
    {
        currentRage = amount;
        CheckRageMeter();
    }
    #endregion

}
