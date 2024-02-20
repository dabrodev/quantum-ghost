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
    [SerializeField]
    private GameObject[] _rarePowerups;


    private bool _stopSpawning = false;

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
        StartCoroutine(SpawnRarePowerupRoutine());
        StartCoroutine(SpawnEnemyWaveRoutine());
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

    private IEnumerator SpawnEnemyWaveRoutine()
    {
        yield return new WaitForSeconds(20.0f);

        while (_stopSpawning == false)
        {
           
            Vector3 randomPos = new Vector3(Random.Range(-9.0f, 9.0f), 8.0f, 0);

            float randomRareTime = Random.Range(9.0f, 14f);
            

            for (int i = 0; i < 5; i++)
            {
                GameObject newEnemy = Instantiate(_enemyPrefab, randomPos + new Vector3(i*2, 0, 0) , Quaternion.identity);
                newEnemy.transform.parent = _enemyContainer.transform;
            }
          
            yield return new WaitForSeconds(randomRareTime);
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

    private IEnumerator SpawnRarePowerupRoutine()
    {
        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(10.0f);
            float randomTime = Random.Range(9.0f, 14f);
            int randomPowerup = Random.Range(0, _rarePowerups.Length);
            Vector3 randomPos = new Vector3(Random.Range(-9.0f, 9.0f), 8.0f, 0);
            GameObject newPowerup = Instantiate(_rarePowerups[randomPowerup], randomPos, Quaternion.identity);
            yield return new WaitForSeconds(randomTime);
        }
    }

    public void onPlayerDeath()
    {
        _stopSpawning = true;
    }
}
