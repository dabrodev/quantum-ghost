using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _redEnemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] _powerups; // 0=Tripleshot 1=Speed 2=Shields 3=Ammo 4=Health 5=Slowdown
    [SerializeField]
    private GameObject[] _rarePowerups; // Multifire Powerup
    private bool _stopSpawning = false;
    private Vector3 _randomPos;
   
    private int _count = 1;
   

    public void StartSpawning()
    {
        _randomPos = new Vector3(Random.Range(-9.0f, 9.0f), 8.0f, 0);

        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
        StartCoroutine(SpawnRarePowerupRoutine());
        StartCoroutine(SpawnEnemyWaveRoutine());
        StartCoroutine(SpawnRareRedEnemyRoutine());
    }

    private IEnumerator SpawnPowerupRoutine()
    {
        while (_stopSpawning == false)
        {

            yield return new WaitForSeconds(4.0f);

            float randomTime = Random.Range(3.0f, 7.0f);



            //int randomPowerup = Random.Range(0, _powerups.Length);
            int randomPowerup = GetWeightedRandomPowerup();
        
      

            GameObject newPowerup = Instantiate(_powerups[randomPowerup], _randomPos, Quaternion.identity);
            yield return new WaitForSeconds(randomTime);
        }
    }

    private int GetWeightedRandomPowerup()
    {
        // Define weights for each powerup; higher numbers mean higher chance of spawning
        int[] weights = new int[] { 10, 10, 10, 50, 5, 15, 30}; // Example weights: Ammo (3) is more common, Health (4) is rare
        int totalWeight = 0;

        // Calculate the sum of all weights
        foreach (int weight in weights)
        {
            totalWeight += weight;
        }

        // Generate a random value from 0 to the sum of the weights
        int randomValue = Random.Range(0, totalWeight);
        int runningTotal = 0;

        // Determine which weight range the randomValue falls into
        for (int i = 0; i < weights.Length; i++)
        {
            runningTotal += weights[i];
            if (randomValue < runningTotal)
            {
                return i; // Return the index of the selected power-up
            }
        }

        return 0; // Default to the first power-up as a fallback
    }


    private IEnumerator SpawnRarePowerupRoutine()
    {
        while (_stopSpawning == false)
        {

            yield return new WaitForSeconds(10.0f);
            float randomRareTime = Random.Range(9.0f, 14f);
            int randomPowerup = Random.Range(0, _rarePowerups.Length);
          
            GameObject newPowerup = Instantiate(_rarePowerups[randomPowerup], _randomPos, Quaternion.identity);
            yield return new WaitForSeconds(randomRareTime);
        }
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        
        yield return new WaitForSeconds(3.0f);

        while (_stopSpawning == false)
        {
            
            GameObject newEnemy = Instantiate(_enemyPrefab, _randomPos, Quaternion.identity);
            
            newEnemy.transform.parent = _enemyContainer.transform;

            if (_count % 3 == 0)
            {
                newEnemy.transform.GetChild(0).gameObject.SetActive(true);
            }
            
            _count++;

            yield return new WaitForSeconds(5);
            
        }
    }  

    private IEnumerator SpawnEnemyWaveRoutine()
    {
        yield return new WaitForSeconds(20.0f);

        while (_stopSpawning == false)
        {

            float randomRareTime = Random.Range(9.0f, 14f);

            for (int i = 0; i < 5; i++)
            {
                GameObject newEnemy = Instantiate(_enemyPrefab, _randomPos + new Vector3(i * 2, 0, 0), Quaternion.identity);
                newEnemy.transform.parent = _enemyContainer.transform;
            }

            yield return new WaitForSeconds(randomRareTime);
        }
    }

    private IEnumerator SpawnRareRedEnemyRoutine()
    {
        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(6.0f);
            float randomRareTime = Random.Range(10f, 15f);

            GameObject newRedEnemy = Instantiate(_redEnemyPrefab, _randomPos, Quaternion.identity);
            newRedEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(randomRareTime);
        }
    }

    public void onPlayerDeath()
    {
        _stopSpawning = true;
    }
}
