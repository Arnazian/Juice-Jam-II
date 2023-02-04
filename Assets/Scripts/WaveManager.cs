using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public enum Difficulty
{
    Easy,
    Medium,
    Hard
}

[System.Serializable]
public class Wave
{
    public List<WeightedGameObjectList> enemiesByRound;
    public int[] amountOfEnemiesPerRound;
    public float[] spawnTime;
}

public class WaveManager : Singleton<WaveManager>
{
    [SerializeField] private Difficulty difficulty;
    
    [SerializeField] private GameObject firstBoss;
    [SerializeField] private GameObject secondBoss;
    [SerializeField] private SpawnPoint[] spawnPoints;
    [SerializeField] private int bossRoundOne;
    [SerializeField] private int bossRoundTwo;
    [SerializeField] private int bossRoundThree;
    
    private int _currentRound = 0;
    private int _currentRoundEnemyCount = 0;
    private List<GameObject> spawnQueue = new List<GameObject>();
    [SerializeField] private List<Wave> _waves;

    public bool startOnAwake = true;

    public Action<int> onRoundStart; 

    protected override void Awake()
    {
        base.Awake();
        if (startOnAwake) 
            StartNewWave();          
    }

    void StartNewWave()
    {
        _currentRound++;
        onRoundStart?.Invoke(_currentRound);
        // if (CheckIfBossRound()) 
        //     return;
        CheckIfBossRound();
        _currentRoundEnemyCount = _waves[(int)difficulty].amountOfEnemiesPerRound[_currentRound];
        PopulateSpawnQueue();
        DistributeSpawnQueue();
    }

    bool CheckIfBossRound()
    {
        if(_currentRound == bossRoundOne) 
        {
            StartFirstBoss();
            return true;
        }
        else if (_currentRound == bossRoundTwo)
        {
            StartSecondBoss();
            return true;
        }
        else if (_currentRound == bossRoundThree)
        {
            StartThirdBoss();
            return true;
        }
        else 
            return false;
    }
    void DistributeSpawnQueue()
    {
        for (var i = spawnQueue.Count; i > 0; i--)
        {
            var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            spawnPoint.spawnDuration = _waves[(int) difficulty].spawnTime[_currentRound - 1];
            spawnPoint.AddToLocalQueue(spawnQueue[i - 1]);
            spawnQueue.RemoveAt(i - 1);
        }
    }
    void PopulateSpawnQueue()
    {
        WeightedGameObjectList curRoundList = _waves[(int)difficulty].enemiesByRound[_currentRound - 1];
        for (int i = _currentRoundEnemyCount; i > 0; i--)
        {
            spawnQueue.Add(curRoundList.GetRandomObject());
        }
    }

    public void EnemyMobDeath()
    {
        _currentRoundEnemyCount--;
        if (_currentRoundEnemyCount <= 0)
            StartNewWave();
    }

    #region BossFights
    private void StartFirstBoss()
    {
        var bossHealthBar = UIManager.Instance.GetBossHealthBar;
        bossHealthBar.transform.parent.gameObject.SetActive(true);
        var boss = Instantiate(firstBoss, Vector3.zero, Quaternion.identity);
    }
    private void StartSecondBoss()
    {
        var bossHealthBar = UIManager.Instance.GetBossHealthBar;
        bossHealthBar.transform.parent.gameObject.SetActive(true);
        var boss = Instantiate(secondBoss, Vector3.zero, Quaternion.identity);
    }

    private void StartThirdBoss()
    {

    }
    #endregion
}
