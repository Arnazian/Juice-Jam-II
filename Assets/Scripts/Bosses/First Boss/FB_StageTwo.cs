using UnityEngine;

public class FB_StageTwo : Stage2Base
{
    private bool stageActive = false;
    [SerializeField] GameObject smallBosses;

    private SpriteRenderer mainBossSprite;
    private Collider2D mainBossCollider;
    private int bossesAlive = 3;

    private void Start()
    {
        mainBossCollider = GetComponent<Collider2D>();
        mainBossSprite = GetComponent<SpriteRenderer>();
    }

    #region Start and Stop
    public override void StartStageTwo()
    {
        stageActive = true;
        mainBossSprite.enabled = false;
        mainBossCollider.enabled = false;
        Instantiate(smallBosses, Vector3.zero, Quaternion.identity);
    }
    public override void StopStageTwo()
    {
        mainBossSprite.enabled = true;
        mainBossCollider.enabled = true;
        stageActive = false;
    }

    public void KillOneBoss()
    {
        bossesAlive--;
        Debug.Log(bossesAlive);
        if(bossesAlive <= 0)
        {
            GetComponent<BossStateController>().StartStageThree();
        }
    }
    #endregion
}
