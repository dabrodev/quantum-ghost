using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorInEnemy : MonoBehaviour
{
    private Enemy _enemy;

    void Start()
    {
        _enemy = gameObject.transform.GetComponentInParent<Enemy>(); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Powerup")
        {
            Vector3 powerupPos = other.transform.position;

            _enemy.UpdatePickupPos(powerupPos);
            _enemy.PowerupDestroyed(false);
            Debug.Log("Detection of Powerup!");
        }
    }
}
