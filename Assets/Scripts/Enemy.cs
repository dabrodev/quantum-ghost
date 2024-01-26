using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 4.0f;
    private float _randomPosX;
    
    // Start is called before the first frame update
    void Start()
    {
        RandomPos();
        transform.position = new Vector3(_randomPosX, 8.0f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -6.0f)
        {
            RandomPos();
            transform.position = new Vector3(_randomPosX, 8.0f, 0);

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        // if other is the Player
        // damage the Player
        // Destroy us (Enemy)

        if (other.tag == "Player")
        {
            //damage Plater
            other.transform.GetComponent<Player>().Damage();
            Destroy(this.gameObject);
        }

        // if other is laser
        // destroy laser
        // destroy us (enemy)

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }


    void RandomPos()
    {
        _randomPosX = Random.Range(-9.0f, 9.0f);
    }
}
