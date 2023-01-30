using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionManager : MonoBehaviour
{
    private bool isInAction = false;

    private bool isDashing = false;
    private bool isAttacking = false;
    private bool isFinishing = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckIfInAction() { return isInAction; }
    2
    
    public void SetInAction(bool newValue) { isInAction = newValue; }
}
