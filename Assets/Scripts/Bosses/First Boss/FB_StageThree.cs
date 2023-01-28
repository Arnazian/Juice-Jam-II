using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FB_StageThree : Stage3Base
{
    private bool stageActive = false;

    void Update()
    {
        if (stageActive)
            Debug.Log("You're in stage THREE");
    }

    #region start and stop
    public override void StartStageThree()
    {
        stageActive = true;
    }
    public override void StopStageThree()
    {
        stageActive = false;
    }
    #endregion
}
