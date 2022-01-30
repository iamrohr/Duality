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
        Vector2 spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
        var newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, transform);
        var enemyComp = newEnemy.GetComponent<Enemy>();
        enemyComp.moveForce = Random.Range(enemyComp.moveForce * 0.8f, enemyComp.moveForce * 1.25f);
        enemyComp.moveForce = Random.Range(enemyComp.rotateSpeed * 0.8f, enemyComp.rotateSpeed * 1.25f);
        
        enemies.Add(newEnemy);
    }

    public void RemoveEnemyFromList(GameObject enemy)
    {
        enemies.Remove(enemy);
    }
}
