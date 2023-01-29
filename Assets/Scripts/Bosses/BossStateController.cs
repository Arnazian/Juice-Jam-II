using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateController : MonoBehaviour
{
    private int curStage;
    [SerializeField] private List<float> stageThresholds;

    private BossHealthManager bossHealthManager;

    void Start()
    {
        bossHealthManager = GetComponent<BossHealthManager>();
        curStage = 1;

    }


    void Update()
    {

    }
    public void CheckStageThreshold()
    {
        if (bossHealthManager.GetHealth <= stageThresholds[0] && bossHealthManager.GetHealth > stageThresholds[1])
        {
            GetComponent<Stage1Base>().StartStageOne();
        }
        else if (bossHealthManager.GetHealth <= stageThresholds[1] && bossHealthManager.GetHealth > stageThresholds[2])
        {
            if (curStage == 2) return;
            Debug.Log("Ran switch to stage 2");
            curStage = 2;
            bossHealthManager.SetHealth(stageThresholds[1]);
            GetComponent<Stage1Base>().StopStageOne();
            GetComponent<Stage2Base>().StartStageTwo();
        }
        else if (bossHealthManager.GetHealth <= stageThresholds[2] && curStage != 3)
        {
            if (curStage == 3) return;
            curStage = 3;
            bossHealthManager.SetHealth(stageThresholds[2]);
            GetComponent<Stage2Base>().StopStageTwo();
            GetComponent<Stage3Base>().StartStageThree();
        }
    }

    public List<float> GetStageThresholds() { return stageThresholds; }


}

