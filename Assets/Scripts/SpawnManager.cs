using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _TripleShootPowerUpPrefab;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private bool _stopSpawn = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnEnemyRoutine()
    {
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
        while (!_stopSpawn)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-9f, 9f), 7, 0);
            float TripleShootSpawnTime = Random.Range(12.0f, 24.0f);

            Instantiate(_TripleShootPowerUpPrefab, spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(TripleShootSpawnTime);
        }
    }

    public void OnPlayerDeath()
    { 
        _stopSpawn = true;
    }
}
