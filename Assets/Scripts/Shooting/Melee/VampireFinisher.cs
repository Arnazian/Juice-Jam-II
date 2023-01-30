using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class VampireFinisher : MonoBehaviour
{
    [SerializeField] private GameObject bloodSuckParticles;
    [SerializeField] private GameObject bloodExplosionParticles;
    [SerializeField] private BloodCheckCollider bloodCheckCollider;
    [SerializeField] private float healAmount;
    [SerializeField] private float launchSpeed;
    [SerializeField] private float suckBloodDuration;
    [FormerlySerializedAs("rageAmountMax")] [SerializeField] private float MaxRageAmount;

    private Collider2D playerCollision;

    private bool suckingBlood = false;
    private bool enableLaunching = false;
    private PlayerActionManager playerActionManager;

    private PlayersHealth playerHealth;
    
    private Image rageMeterFill;
    private float currentRage;
    void Start()
    {
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
    }

    void StartFinisher()
    {
        if(playerActionManager.CheckIfInAction()) { return; }
        if(currentRage < MaxRageAmount || bloodCheckCollider.GetSelectedEnemy() == null)
        {
            Debug.Log("Not Enough Rage || Not close enough to enemy");
            return;
        }

        GetComponent<MovePlayer>().SetCanMove(false);
        playerActionManager.SetInAction(true);
        playerHealth.SetImmuneStatus(true);
        playerCollision.enabled = false;
        enableLaunching = true;
    }

    IEnumerator SuckBlood()
    {
        bloodSuckParticles.SetActive(true);
        yield return new WaitForSeconds(suckBloodDuration);        
        playerCollision.enabled = true;
        enableLaunching = false;
        GetComponent<MovePlayer>().SetCanMove(true);
        playerHealth.SetImmuneStatus(false);
        playerHealth.Heal(healAmount);

        GameObject go = bloodCheckCollider.GetSelectedEnemy();
        go.GetComponent<EnemyMobHealthManager>().RunEnemyDeath();
        suckingBlood = false;
        bloodSuckParticles.SetActive(false);
        SetRageAmount(0);
        Instantiate(bloodExplosionParticles, transform.position, Quaternion.identity);
        playerActionManager.SetInAction(false);
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
        UpdateGraphics();
        if(currentRage >= MaxRageAmount)
        {
            bloodCheckCollider.SetBloodCheckColliderStatus(true);
        }
        else
        {
            bloodCheckCollider.SetBloodCheckColliderStatus(false);
            bloodCheckCollider.UnselectEnemy(); 
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
