using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject[] _powerUp;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private bool _stopSpawn = false;

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);

        while (!_stopSpawn)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-9f, 9f), 7, 0);

            // Instantiate enemy object inside enemyContainer in hierarchy
            GameObject newEnemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(3.0f);

        while (!_stopSpawn)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-9f, 9f), 7, 0);
            float powerUpSpawnTime = Random.Range(12.0f, 24.0f);

            Instantiate(_powerUp[Random.Range(0, _powerUp.Length)], spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(powerUpSpawnTime);
        }
    }

    public void OnPlayerDeath()
    { 
        _stopSpawn = true;
    }
}
