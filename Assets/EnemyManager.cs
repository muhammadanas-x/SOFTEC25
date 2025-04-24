using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float minSpawnTime = 3f;
    public float maxSpawnTime = 10f;
    public int maxEnemies = 10;
    
    private List<Transform> spawnPoints = new List<Transform>();
    private float nextSpawnTime;
    private int currentEnemies = 0;

    void Start()
    {
        // Find all spawn points
        foreach (GameObject point in GameObject.FindGameObjectsWithTag("SpawnPoints"))
        {
            spawnPoints.Add(point.transform);
        }
        
        // Set first spawn time
        nextSpawnTime = Time.time + Random.Range(minSpawnTime, maxSpawnTime);
    }

    void Update()
    {
        // Only spawn if we have points, haven't reached max enemies, and it's time
        if (spawnPoints.Count > 0 && 
            currentEnemies < maxEnemies && 
            Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            // Set next spawn time with random interval
            nextSpawnTime = Time.time + Random.Range(minSpawnTime, maxSpawnTime);
        }
    }
    
    void SpawnEnemy()
    {
        // Get random spawn point
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        
        // Create enemy
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        currentEnemies++;
        
        
    }
}
