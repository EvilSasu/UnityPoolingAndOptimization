using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform target;
    public Transform respTarget;
    public float timeBetweenWaves = 2f;
    public int enemiesPerWave = 5;
    public float timeBetweenEnemies = 0.4f;
    public float waveIntervalReduction = 0.1f;
    public int enemiesPerWaveIncrease = 2;

    private ObjectPool enemyPool;
    private float waveTimer = 0f;

    private void Start()
    {
        enemyPool = GameObject.FindGameObjectWithTag("EnemyPool").GetComponent<ObjectPool>();
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        while (true)
        {
            for (int i = 0; i < enemiesPerWave; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(timeBetweenEnemies);
            }

            yield return new WaitForSeconds(timeBetweenWaves);

            waveTimer += timeBetweenWaves;
            if (waveTimer >= 10f)
            {
                waveTimer = 0f;
                IncreaseDifficulty();
            }
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemy = enemyPool.GetObject();
        enemy.transform.position = respTarget.position;
        enemy.GetComponent<Enemy>().target = target;
    }

    private void IncreaseDifficulty()
    {
        enemiesPerWave += enemiesPerWaveIncrease;
        timeBetweenEnemies -= waveIntervalReduction;
        if (timeBetweenEnemies < 0.1f) timeBetweenEnemies = 0.1f;
    }
}
