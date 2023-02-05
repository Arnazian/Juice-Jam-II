using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMinionSpawner : MonoBehaviour
{
    [SerializeField] private float minionSpawnTimerMax;
    [SerializeField] private float minionSpawnTimerMin;
    [SerializeField] private GameObject minionToSpawn;

    private float minionSpawnTimerCur;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (minionSpawnTimerCur <= 0)
        {
            ConstantMinionSPawn();
            minionSpawnTimerCur = Random.Range(minionSpawnTimerMin, minionSpawnTimerMax);
        }
        else
        {
            minionSpawnTimerCur -= Time.deltaTime;
        }
    }
    void ConstantMinionSPawn()
    {
        // if (stageActive)
        {
            WaveManager.Instance.SpawnSingleMinion(minionToSpawn, 1f);
        }
    }

}
