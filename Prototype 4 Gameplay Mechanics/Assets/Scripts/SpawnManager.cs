using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Object variables
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;

    // Variables
    private float spawnRange = 9.0f;
    public int enemyCount;
    public int waveNumber = 1;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber);
    }

    // Update is called once per frame
    void Update()
    {
        // Storing how many enemy in the scene
        enemyCount = FindObjectsOfType<Enemy>().Length;

        // if there is no enemy spawn again with additional
        if (enemyCount == 0)
        {
            waveNumber++; // enemy number
            SpawnEnemyWave(waveNumber);
        }
    }

    // SpawnEnemyWave is called when there is no enemy left
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++) // "++" is just short for i+=1 or i=i+1
        {
            SpawnEnemy();
            SpawnPowerup();
        }
    }

    // SpawnEnemy is to spawn enemy
    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
    }

    // SpawnPowerup is to spawn powerup
    void SpawnPowerup()
    {
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
    }

    // GenerateSpawnPosition is to generate random position in the scene
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return randomPos;
    }
}
