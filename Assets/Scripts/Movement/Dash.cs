using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dash : MonoBehaviour
{
    [SerializeField] private float dashForce;
    [SerializeField] private float dashCooldownStart;
    private float dashCooldown;
    private bool canPerformADash;
    private Rigidbody2D rb;


    void Start()
    {
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
        canPerformADash = false;

        StartCoroutine("MakeDashShadow");

        MakePlayerImmune();

        rb.AddForce(rb.velocity.normalized * 10 * force, ForceMode2D.Impulse);
    }

    void MakePlayerImmune()
    {
        GetComponent<PlayersHealth>().isDashing = true;
    }

    void UnmakePlayerImmune()
    {
        GetComponent<PlayersHealth>().isDashing = false;
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

        for(int i = 0; i <= startI; i++)
        {
            yield return new WaitForSeconds(0.005f);
            endPosition = transform.position;

            Vector3 direction = Vector3.Normalize(endPosition - startPosition);
            Vector3 offset = direction * distanceModifier * Vector3.Distance(startPosition, endPosition) * i;

            float shadowLiveTime = startI*(float)i/250f;
            DashShadow.CreateDashShadow(startPosition + offset, transform.Find("Sprite").Find("Image").gameObject, startRotation, shadowLiveTime);
        }
    }
}
