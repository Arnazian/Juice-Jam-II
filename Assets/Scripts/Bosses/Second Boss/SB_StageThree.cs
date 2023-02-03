using UnityEngine;

public class SB_StageThree : Stage3Base
{
    private Stage1Base _stage1;
    private SB_StageTwo _stage2;

    private void Awake()
    {
        _stage1 = GetComponent<Stage1Base>();
        _stage2 = GetComponent<SB_StageTwo>();
    }

    public override void StartStageThree()
    {
        _stage1.StartStageOne();
        _stage2.DestroyAllPools();
    }
}
