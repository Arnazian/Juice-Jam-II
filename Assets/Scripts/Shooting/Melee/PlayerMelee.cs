using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    [SerializeField] private float recoveryDurationMax;
    [SerializeField] private float playerForwardForce;
    [SerializeField] private MeleeCollider meleeCollider;

    private float recoveryDurationCur;

    private bool isAttacking;
    private bool isRecovering;

    private RotateTowardsCursor rotateTowardsCursor;
    private MovePlayer movePlayer;
    private Animator anim;
    private Rigidbody2D rb;
    


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movePlayer = GetComponent<MovePlayer>();
        rotateTowardsCursor = GetComponent<RotateTowardsCursor>();
        recoveryDurationCur = recoveryDurationMax;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        ControlMeleeLogic();
        ControlRecoveringLogic();
    }

    void ControlRecoveringLogic()
    {
        if (!isRecovering) { return; }

        if (recoveryDurationCur > 0) {  recoveryDurationCur -= Time.deltaTime;  }
        else { SetRecoveringFalse(); }      
    }    

    void ControlMeleeLogic()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (!isAttacking) { StartAttack(); }

            else if(isRecovering)
            {
                isRecovering = false;
                anim.SetTrigger("ContinueAttack");   
                StartAttack();
            }
        }
    }

    void StartAttack()
    {
        // rotateTowardsCursor.SetCanRotate(false);
        movePlayer.SetCanMove(false);
        rb.AddForce(transform.up * playerForwardForce, ForceMode2D.Impulse);
        recoveryDurationCur = recoveryDurationMax; 
        isAttacking = true;
        anim.SetBool("IsAttacking", true);
    }

    public void SetRecoveringTrue()
    {
        recoveryDurationCur = recoveryDurationMax;
        isRecovering = true;
    }

    public void SetRecoveringFalse()
    {
        isRecovering = false;
        isAttacking = false;
        recoveryDurationCur = recoveryDurationMax;
        //rotateTowardsCursor.SetCanRotate(true);
        movePlayer.SetCanMove(true);
        anim.SetBool("IsAttacking", false);
    }

    public void DoDamageToEnemiesHit()
    {
        meleeCollider.DamageEnemies();
    }
}
