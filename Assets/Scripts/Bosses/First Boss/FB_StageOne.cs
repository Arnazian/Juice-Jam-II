using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FB_StageOne : Stage1Base
{
    private bool stageActive = false;

    [Header("Attack One Variables")]
    [SerializeField] private float fullTurnDuration;
    [SerializeField] private GameObject rotator; 
    [SerializeField] private float shootInterval;

    private void Start()
    {
        StartStageOne();
    }

    void Update()
    {
        if (stageActive)
            Debug.Log("You're in stage ONE");
    }

    [ContextMenu("AttackOne")]
    void AttackOne()
    {
        rotator.transform.localRotation = Quaternion.Euler(0, 0, 0);
        rotator.transform.DOLocalRotate(new Vector3(0,0, 359f), fullTurnDuration);
    }

    void AttackTwo()
    {

    }

    void AttackThree()
    {

    }

    #region start and stop
    public override void StartStageOne()
    {
        stageActive = true;
    }
    public override void StopStageOne()
    {
        stageActive = false;
    }
    #endregion
}
