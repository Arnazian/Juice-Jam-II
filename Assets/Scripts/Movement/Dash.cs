using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dash : MonoBehaviour
{
    [SerializeField] private float dashForce;
    [SerializeField] private int dashCooldownStart;
    private int dashCooldown;
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
            DecreaseDashCooldown(1);

            if (dashCooldown <= 0)
            {
                canPerformADash = true;
                RestartDashCooldown(dashCooldownStart);
            }
        }
    }

    void DecreaseDashCooldown(int value)
    {
        dashCooldown-=value;
        
    }

    void DoDash(float force)
    {
        canPerformADash = false;
        rb.AddForce(rb.velocity * force, ForceMode2D.Impulse);
    }

    void RestartDashCooldown(int startValue)
    {
        dashCooldown = startValue;
    }
}
