using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FB_StageTwo : Stage2Base
{
    private bool stageActive = false;
    [SerializeField] GameObject smallBosses;

    private SpriteRenderer mainBossSprite;
    private Collider2D mainBossCollider;

    private void Start()
    {
        mainBossCollider = GetComponent<Collider2D>();
        mainBossSprite = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if(stageActive)
        Debug.Log("You're in stage TWO");
    }

    #region start and stop
    public override void StartStageTwo()
    {
        stageActive = true;
        mainBossSprite.enabled = false;
        mainBossCollider.enabled = false;
        smallBosses.SetActive(true);
    }
    public override void StopStageTwo()
    {
        mainBossSprite.enabled = true;
        mainBossCollider.enabled = true;
        stageActive = false;
       //  FindObjectOfType<BossStateController>().StartStageThree();
    }
    #endregion
}
