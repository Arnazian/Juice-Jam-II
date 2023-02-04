using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dash : MonoBehaviour
{
    public bool isDashing;

    [SerializeField] private float dashForce;
    [SerializeField] private float dashCooldownStart;
    private float dashCooldown;
    private bool canPerformADash;
    private Rigidbody2D rb;
    private PlayerActionManager playerActionManager;


    void Start()
    {
        playerActionManager = GetComponent<PlayerActionManager>();
        isDashing = false;
        RestartDashCooldown(dashCooldownStart);

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(PauseMenu.Instance.IsPaused)
            return;

        DoDashLogic();
    }

    void DoDashLogic()
    {
        
        if(Keyboard.current.spaceKey.wasPressedThisFrame && canPerformADash)
        {
            DoDash(dashForce);
        }
        else
        {
            dashCooldown-=Time.deltaTime;

            if (dashCooldown <= 0)
            {
                canPerformADash = true;
                // playerActionManager.SetIsDashing(false);
                RestartDashCooldown(dashCooldownStart);
            }
        }
    }

    void RestartDashCooldown(float startValue)
    {
        dashCooldown = startValue;
    }

    void DoDash(float force)
    {
        if (playerActionManager.CheckIfFinishing()) { return; }
        // playerActionManager.SetIsDashing(true);
        canPerformADash = false;

        StartCoroutine("MakeDashShadow");

        MakePlayerImmune();

        Camera.main.GetComponent<ScreenShake>().DoScreenShake(0.25f, 0.05f);

        // it's here and in PlayerMelee.cs cause player can press attack and then dash or reverse
        if (!IsMeleeAttackingOrDashing())
            rb.AddForce(rb.velocity.normalized * 10 * force, ForceMode2D.Impulse);
        else
            rb.AddForce(rb.velocity.normalized * 10 * force/2, ForceMode2D.Impulse);
    }

    public bool IsMeleeAttackingOrDashing()
    {
        return GetComponent<PlayerMelee>().GetIsAttacking || isDashing;
    }

    void MakePlayerImmune()
    {
        isDashing = true;
    }

    void UnmakePlayerImmune()
    {
        isDashing = false;
    }

    IEnumerator MakeDashShadow()
    {
        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;
        yield return new WaitForSeconds(0.1f);
        UnmakePlayerImmune();
        Vector3 endPosition = transform.position;

        int startI = 10;

        //this is distance between each invidual shadow
        //I found out this is good distance formula
        float distanceModifier = 1f / (float)startI;

        for(int i = 1; i <= startI; i++)
        {
            yield return new WaitForSeconds(0.005f);
            endPosition = transform.position;

            Vector3 direction = Vector3.Normalize(endPosition - startPosition);
            Vector3 offset = direction * distanceModifier * Vector3.Distance(startPosition, endPosition) * i;

            float shadowLifeTime = startI*(float)i/250f;
            DashShadow.CreateDashShadow(startPosition + offset, GetComponent<SpriteRenderer>(), startRotation, shadowLifeTime);
        }
    }
}
