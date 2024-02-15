using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] _powerups;

    private bool _stopSpawning = false;

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            Vector3 randomPos = new Vector3(Random.Range(-9.0f, 9.0f), 8.0f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, randomPos, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5);
        }
    }

    private IEnumerator SpawnPowerupRoutine()
    {
        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(3.0f);
            float randomTime = Random.Range(3.0f, 7.0f);
            int randomPowerup = Random.Range(0, _powerups.Length);
            Vector3 randomPos = new Vector3(Random.Range(-9.0f, 9.0f), 8.0f, 0);
            GameObject newPowerup = Instantiate(_powerups[randomPowerup], randomPos, Quaternion.identity);
            yield return new WaitForSeconds(randomTime);
        }
    }

    public void onPlayerDeath()
    {
        _stopSpawning = true;
    }
}
