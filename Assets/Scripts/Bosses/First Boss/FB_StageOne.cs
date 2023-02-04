using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FB_StageOne : Stage1Base
{
    private bool stageActive = false;

    [Header("ROTATING ATTACK Variables")]

    [SerializeField] private float fullTurnDuration;
    [SerializeField] private GameObject rotator;
    [SerializeField] private float rotatingShootInterval;

    [Header("CONSTANT SHOOTING Variables")]

    private bool isConstantlyShooting = false;
    [SerializeField] private GameObject constantShootProjectile;
    [SerializeField] private Transform constantShootPoint;
    [SerializeField] private float constantShootDurationMin;
    [SerializeField] private float constantShootDurationMax;

    [SerializeField] private float constantShootIntervalMin;
    [SerializeField] private float constantShootIntervalMax;
    private float constantShootIntervalCur;

    [Header("SPAWN MINIONS Variables")]
    [SerializeField] private List<GameObject> minionsToSpawn = new List<GameObject>();
    [SerializeField] private float minionSpawnTime;
    [SerializeField] private float bossSpawnRecoveryTime;

    private ProjectileSpawner projectileSpawner;
    private FacePlayer facePlayer;


    private void Start()
    {
        facePlayer = GetComponent<FacePlayer>();
        projectileSpawner = GetComponent<ProjectileSpawner>();
        projectileSpawner.SetShootInterval(rotatingShootInterval);
        StartStageOne();

    }

    void Update()
    {
        if (stageActive)
        {
            DoConstantShoot();
        }
    }

    void StageLogic()
    {

    }

    #region Rotatig Attack
    [ContextMenu("AttackOne")]
    void RotatingAttack()
    {
        if (!stageActive) { return; }
        projectileSpawner.EnableShooting();
        facePlayer.DisableFacePlayer();
        rotator.transform.DOLocalRotate(new Vector3(0, 0, 360), fullTurnDuration, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear).OnComplete(() =>
        {
            projectileSpawner.DisableShooting();
            facePlayer.EnableFacePlayer();
            StartConstantShooting();
        }); ;
    }
    #endregion

    void StartConstantShooting()
    {
        if (!stageActive) { return; }
        isConstantlyShooting = true;
        StartCoroutine("CoroutineConstantlyShooting");
    }

    void StopConstantShooting()
    {
        isConstantlyShooting = false;

        // change random to 0, 2 after minions exist
        int i = Random.Range(0, 2);
        if (i == 0)
        {
            RotatingAttack();
        }
        else
        {
            StartCoroutine("SpawnMinions");
        }
    }

    void DoConstantShoot()
    {
        if (!isConstantlyShooting || !stageActive) { return; }

        if(constantShootIntervalCur <= 0)
        {
            Instantiate(constantShootProjectile, constantShootPoint.position, constantShootPoint.rotation);
            float newInterval = Random.Range(constantShootIntervalMin, constantShootIntervalMax);
            constantShootIntervalCur = newInterval;
        }
        constantShootIntervalCur -= Time.deltaTime;
        
          
    }
    IEnumerator CoroutineConstantlyShooting()
    {
        float duration = Random.Range(constantShootDurationMin, constantShootDurationMax);
        yield return new WaitForSeconds(duration);
        StopConstantShooting();
    }

    IEnumerator SpawnMinions()
    {
        if (stageActive)
        {
            WaveManager.Instance.SpawnMinions(minionsToSpawn, minionSpawnTime);
            yield return new WaitForSeconds(bossSpawnRecoveryTime);
            //Invoke("DoConstantShoot", bossSpawnRecoveryTime);
            StartConstantShooting();
        }        
    }

    

    #region start and stop
    public override void StartStageOne()
    {
        stageActive = true;
        StartConstantShooting();

    }
    public override void StopStageOne()
    {
        stageActive = false;
    }
    #endregion
}
