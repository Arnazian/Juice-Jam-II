using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FB_StageTwo : Stage2Base
{
    private bool stageActive = false;

    void Update()
    {
        if(stageActive)
        Debug.Log("You're in stage TWO");
    }

    #region start and stop
    public override void StartStageTwo()
    {
        stageActive = true;
    }
    public override void StopStageTwo()
    {
        stageActive = false;
    }
    #endregion
}
