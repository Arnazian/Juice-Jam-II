using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStagger : MonoBehaviour
{
    [SerializeField] private float staggerBarMax;
    [SerializeField] private float staggerRecoveryMultiplier;
    private float staggerBarCur;

    private EnemyMobHealthManager healthManager;
    private BaseMovement baseMovement;
    private Image staggerBarFill;

    [SerializeField] private float staggerKnockbackDurationMax;
    private float staggerKnockbackDurationCur;
    private bool isStaggered = false;
    [SerializeField] private float breakDuration;
    private bool isBroken = false;

    [SerializeField] private float knockStrength;
    [SerializeField] private float knockDurationMax;
    private float knockDurationCur;
    private bool isKnocked = false;

    void Start()
    {
        staggerBarCur = 0; 
        healthManager = GetComponent<EnemyMobHealthManager>();
        baseMovement = GetComponent<BaseMovement>();
        staggerBarFill = healthManager.GetStaggerBar();
        UpdateStaggerBar(); 
    }

    
    void Update()
    {
        StaggerRecovery();
        StaggerKnockbackTimer();
    }

    void StaggerKnockbackTimer()
    {
        if (!isStaggered) { return; }

        if(staggerKnockbackDurationCur > 0)
        {
            staggerKnockbackDurationCur -= Time.deltaTime;
        }
        else
        {
            isStaggered = false;
            baseMovement.SetCanMove(true);
        }
    }
    void StaggerRecovery()
    {
        if(isBroken) { return; }
        if(staggerBarCur <= 0) { return; }
        staggerBarCur -= Time.deltaTime * staggerRecoveryMultiplier;
        UpdateStaggerBar();
    }

    IEnumerator CoroutineDoBreak()
    {
        isBroken = true;
        baseMovement.StopMovement();
        yield return new WaitForSeconds(breakDuration);
        StopBreak();
    }

    void StopBreak()
    {
        isBroken = false;
        baseMovement.SetCanMove(true);
        staggerBarCur = 0;
        UpdateStaggerBar();
    }
    public void Stagger(float amount, bool isMelee)
    {
        if(isBroken && isMelee)
        {
            StartCoroutine(CoroutineDoKnockBack());
            return;
        }

        staggerBarCur += amount;

        if (staggerBarCur >= staggerBarMax)
        {
            staggerBarCur = staggerBarMax;
            StartCoroutine(CoroutineDoBreak());
        }
        else
        {
            isStaggered = true;
            baseMovement.StopMovement();
            staggerKnockbackDurationCur = staggerKnockbackDurationMax;
        }

        UpdateStaggerBar();
    }

    IEnumerator CoroutineDoKnockBack()
    {
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector3 knockbackDirection = (transform.position - playerPos).normalized;

        StopCoroutine(CoroutineDoBreak());
        StopBreak();
        baseMovement.StopMovement();
        isKnocked = true;
        GetComponent<Rigidbody2D>().AddForce(knockbackDirection * knockStrength, ForceMode2D.Impulse);
        // GetComponent<Rigidbody2D>().velocity = knockbackDirection * knockStrength;
        isKnocked = false;
        yield return new WaitForSeconds(knockDurationMax);
        baseMovement.SetCanMove(true);
    }    
    void UpdateStaggerBar()
    {
        float newAmount = staggerBarCur / staggerBarMax;
        staggerBarFill.fillAmount = newAmount;
    }


    public bool GetIsStaggered() { return isStaggered; }
}
