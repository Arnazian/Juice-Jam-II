using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveManager : Singleton<WaveManager>
{

    [SerializeField] private GameObject firstBoss;
    [SerializeField] private SpawnPoint[] spawnPoints;
    [SerializeField] private int bossRoundOne;
    [SerializeField] private int bossRoundTwo;
    [SerializeField] private int bossRoundThree;

    // [SerializeField] private Vector2 enemySpawnRange;
    [SerializeField] private List<WeightedGameObjectList> enemies;


    // cocos
    private int curRound = 0;
    private int curRoundEnemyCount = 0;
    private List<GameObject> spawnQueue = new List<GameObject>();
    [SerializeField] private List<WeightedGameObjectList> enemiesByRound;
    [SerializeField] private int[] amountOfEnemiesPerRound;

    [Header("Debug, Remove Later")]
    public bool startOnAwake = true;
    
    private int _currentWaveNumber = 0;

    // private int _enemiesKilled = 0;

    [SerializeField]private int _enemyCount = 5;

    public Action onBossStart;

    protected override void Awake()
    {
        base.Awake();
        if (startOnAwake) { StartNewWave(); }            
    }

    void StartNewWave()
    {
        curRound++;
        if (CheckIfBossRound()) { return; }
        curRoundEnemyCount = amountOfEnemiesPerRound[curRound];
        PopulateSpawnQueue();
        DistributeSpawnQueue();
    }

    bool CheckIfBossRound()
    {
        if(curRound == bossRoundOne) 
        {
            StartFirstBoss();
            return true;
        }
        else if (curRound == bossRoundTwo)
        {
            StartSecondBoss();
            return true;
        }
        else if (curRound == bossRoundThree)
        {
            StartThirdBoss();
            return true;
        }
        else { return false; }
    }
    void DistributeSpawnQueue()
    {
        for (int i = spawnQueue.Count; i > 0; i--)
        {
            SpawnPoint curSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            curSpawnPoint.AddToLocalQueue(spawnQueue[i - 1]);
            spawnQueue.RemoveAt(i - 1);
        }
    }
    void PopulateSpawnQueue()
    {
        WeightedGameObjectList curRoundList = enemiesByRound[curRound - 1];
        for (int i = curRoundEnemyCount; i > 0; i--)
        {
            spawnQueue.Add(curRoundList.GetRandomObject());
        }
    }




    /*
    public void StartNextWave()
    {
        // _enemiesKilled = 0;
        if (_currentWaveNumber >= enemies.Count)
        {
            onBossStart?.Invoke();
            StartFirstBoss();
            return;
        }

        _currentWaveNumber++;
        var weightedList = enemies[_currentWaveNumber - 1];
        for (int i = 0; i < _enemyCount; i++)
        {
            var enemy = Instantiate(weightedList.GetRandomObject(),
                new Vector3(Random.Range(-enemySpawnRange.x, enemySpawnRange.x),
                    Random.Range(-enemySpawnRange.y, enemySpawnRange.y), 0), Quaternion.identity);
        }
    }

    */

    public void EnemyMobDeath()
    {
        curRoundEnemyCount--;
        if (curRoundEnemyCount <= 0)
        {
            StartNewWave();
        }            
    }

    #region BossFights
    private void StartFirstBoss()
    {
        var bossHealthBar = UIManager.Instance.GetBossHealthBar;
        bossHealthBar.gameObject.SetActive(true);
        var boss = Instantiate(firstBoss, Vector3.zero, Quaternion.identity);
    }
    private void StartSecondBoss()
    {

    }

    private void StartThirdBoss()
    {

    }
    #endregion
}
