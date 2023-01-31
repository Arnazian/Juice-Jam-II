using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionManager : MonoBehaviour
{
    private bool isInAction = false;

    private bool isDashing = false;
    private bool isAttacking = false;
    private bool isFinishing = false;


    private bool canFinish = true;
    private bool canAttack = true;
    private bool canDash = true;
    private bool canMove = true;
    public void SetIsDashing(bool newValue)
    {
        isDashing = newValue;
        isInAction = newValue;
    }
    public void SetIsAttacking(bool newValue) 
    {
        isInAction = newValue;
        isAttacking = newValue;         
    }
    public void SetIsFinishing(bool newValue)
    {
        isInAction = newValue;
        isFinishing = newValue;        
    }

    public bool CheckIfAttacking() { return isAttacking; }
    public bool CheckIfFinishing() { return isFinishing; }
    public bool CheckIfInAction() { return isInAction; }    
    public void SetInAction(bool newValue) 
    {
        isInAction = newValue; 
    }
}
