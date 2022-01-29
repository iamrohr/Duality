using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : Singleton<EnemyManager>
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float timeBetweenSpawns;
    [SerializeField] private Transform[] spawnPoints;
    
    private List<GameObject> enemies = new List<GameObject>();

    private void Start()
    {
        StartSpawnEnemyRepeating(0, timeBetweenSpawns);
    }

    public void StartSpawnEnemyRepeating(float delaySeconds, float rateSeconds)
    {
        InvokeRepeating(nameof(SpawnEnemy), delaySeconds, rateSeconds);
    }

    public void StopSpawnEnemyRepeating()
    {
        CancelInvoke(nameof(SpawnEnemy));
    }

    public void SpawnEnemy()
    {
        var spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
        var newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, transform);
        enemies.Add(newEnemy);
    }

    public void RemoveEnemyFromList(GameObject enemy)
    {
        enemies.Remove(enemy);
    }
}
