using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnDuration;
    private List<GameObject> localQueue = new List<GameObject>();
    private bool isSpawning = false;
    private bool spawnsAvailable = false;

    [SerializeField] private ParticleSystem spawnParticles;

    
    
    void Start()
    {
        
    }

    
    void Update()
    {
        SpawnLocalQueue();
    }
    public void AddToLocalQueue(GameObject go)
    {
        localQueue.Add(go);
        spawnsAvailable = true;
    }

    public void SpawnLocalQueue()
    {
        if (!spawnsAvailable) { return; }
        if (isSpawning) { return; }

        isSpawning = true;
        StartCoroutine(CoroutineSpawnNextInQueue());
    }

    IEnumerator CoroutineSpawnNextInQueue()
    {
        spawnParticles.Play();
        yield return new WaitForSeconds(spawnDuration);

        GameObject newSpawn = Instantiate(localQueue[0], spawnPoint.position, Quaternion.identity);
        localQueue.RemoveAt(0);
        if(localQueue.Count <= 0) { spawnsAvailable = false; }
        spawnParticles.Stop();
        isSpawning =false;
        
    }
}
