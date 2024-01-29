using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
  
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;

    private bool _stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // spawn game objects every 5 seconds
    // Create a coroutine of type IEnumerator -- Yield Events
    // while loop


    private IEnumerator SpawnRoutine() {

        // while loop (infinite loop)
            // Instantiate enemy prefab
            // yield wait for 5 seconds

        while (_stopSpawning == false) {
           GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(Random.Range(-9.0f, 9.0f), 8.0f, 0), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5);
            
        }

    }

    public void onPlayerDeath()
    {
        _stopSpawning = true;
    }

}
