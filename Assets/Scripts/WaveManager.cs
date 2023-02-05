using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public enum Difficulty
{
    Easy,
    Medium,
    Hard
}

[Serializable]
public class Wave
{
    public List<WeightedGameObjectList> enemiesByRound;
    public int[] amountOfEnemiesPerRound;
    public float[] spawnTime;
}

[Serializable]
public class DifficultySetting
{
    public float damageMultiplier;
    public float damageTakenMultiplier;
}

public class WaveManager : Singleton<WaveManager>
{
    public DifficultySetting GetCurrentDifficultySetting => difficultySettings[(int) difficulty];
    
    [SerializeField] private Difficulty difficulty;
    [SerializeField] private List<DifficultySetting> difficultySettings;
    
    [SerializeField] private AudioClip arena1Music;
    [SerializeField] private AudioClip arena2Music;
    [SerializeField] private AudioClip boss1Music;
    [SerializeField] private AudioClip boss2Music;
    [SerializeField] private AudioClip boss2MusicLoop;
    
    [SerializeField] private GameObject firstBoss;
    [SerializeField] private GameObject secondBoss;
    [SerializeField] private SpawnPoint[] spawnPoints;
    [SerializeField] private int bossRoundOne;
    [SerializeField] private int bossRoundTwo;
    [SerializeField] private int bossRoundThree;
    
    [FormerlySerializedAs("_wave")] [SerializeField] private Wave wave;
    
    public bool startOnAwake = true;
    public Action<int> onRoundStart; 
    
    private int _currentRound = 0;
    private int _currentRoundEnemyCount = 0;
    private List<GameObject> _spawnQueue = new List<GameObject>();

    public bool firstBossActive = false;

    private bool _startedArena1;
    private bool _startedArena2;
    private bool _startedBoss2Loop;
    
    protected override void Awake()
    {
        base.Awake();
        if (startOnAwake) 
            StartNewWave();
        difficulty = DifficultyManager.Instance.GetCurrentDifficulty;
    }

    void StartNewWave()
    {
        _currentRound++;
        onRoundStart?.Invoke(_currentRound);
        // if (CheckIfBossRound()) 
        //     return;
        CheckIfBossRound();
        _currentRoundEnemyCount = wave.amountOfEnemiesPerRound[_currentRound - 1];
        PopulateSpawnQueue();
        DistributeSpawnQueue();

        if (_currentRound < 5 && !_startedArena1)
        {
            AudioManager.Instance.PlayMusic("a1", arena1Music, volume: 0.2f);
            _startedArena1 = true;
        }
        else if (_currentRound > 5 && !_startedArena2)
        {
            AudioManager.Instance.Stop("a1");
            AudioManager.Instance.Stop("boss1");
            AudioManager.Instance.PlayMusic("a2", arena2Music, volume: 0.2f);
            _startedArena2 = true;
        }
        if(AudioManager.Instance.GetAudioSource("boss2Intro") != null)
        {
            if (!AudioManager.Instance.GetAudioSource("boss2Intro").isPlaying && !_startedBoss2Loop)
            {
                AudioManager.Instance.Stop("boss2Intro");
                AudioManager.Instance.PlayMusic("boss2Loop", boss2MusicLoop, volume: 0.2f);
                _startedBoss2Loop = true;
            }
        }
 
    }

    private bool CheckIfBossRound()
    {
        if(_currentRound == bossRoundOne) 
        {
            StartFirstBoss();
            return true;
        }
        if (_currentRound == bossRoundTwo)
        {
            StartSecondBoss();
            return true;
        }
        return false;
    }
    void DistributeSpawnQueue()
    {
        for (var i = _spawnQueue.Count; i > 0; i--)
        {
            var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            spawnPoint.spawnDuration = wave.spawnTime[_currentRound - 1];
            spawnPoint.AddToLocalQueue(_spawnQueue[i - 1]);
            _spawnQueue.RemoveAt(i - 1);
        }
    }
    void PopulateSpawnQueue()
    {
        WeightedGameObjectList curRoundList = wave.enemiesByRound[_currentRound - 1];
        for (int i = _currentRoundEnemyCount; i > 0; i--)
        {
            _spawnQueue.Add(curRoundList.GetRandomObject());
        }
    }

    public void EnemyMobDeath()
    {
        if(firstBossActive) { return; }
        _currentRoundEnemyCount--;
        if (_currentRoundEnemyCount <= 0)
            StartNewWave();
    }

    #region BossFights
    private void StartFirstBoss()
    {
        firstBossActive = true;
        var bossHealthBar = UIManager.Instance.GetBossHealthBar;
        bossHealthBar.transform.parent.gameObject.SetActive(true);
        var boss = Instantiate(firstBoss, Vector3.zero, Quaternion.identity);
        
        AudioManager.Instance.Stop("a1");
        AudioManager.Instance.Stop("a2");
        AudioManager.Instance.PlayMusic("boss1", boss1Music, volume: 0.2f);
    }

    public void EndBossFight()
    {
        firstBossActive=false;
        StartNewWave();
    }

    
    public void SpawnMinions(List<GameObject> minionsToSpawn, float spawnTime)
    {
        List<GameObject> currentSpawns = new List<GameObject>(minionsToSpawn);
        for (var i = minionsToSpawn.Count; i > 0; i--)
        {
            var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            spawnPoint.spawnDuration = spawnTime;
            spawnPoint.AddToLocalQueue(minionsToSpawn[i - 1]);
            minionsToSpawn.RemoveAt(i - 1);
        }
    }
    
    private void StartSecondBoss()
    {
        var bossHealthBar = UIManager.Instance.GetBossHealthBar;
        bossHealthBar.transform.parent.gameObject.SetActive(true);
        var boss = Instantiate(secondBoss, Vector3.zero, Quaternion.identity);
        
        AudioManager.Instance.Stop("a1");
        AudioManager.Instance.Stop("a2");
        AudioManager.Instance.Stop("boss1");
        AudioManager.Instance.PlayMusic("boss2Intro", boss2Music, false, volume: 0.2f);
    }
    #endregion
}
