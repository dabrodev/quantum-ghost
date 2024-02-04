using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 4.0f;
    private float _randomPosX;

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -6.0f)
        {
            RandomPos();
            transform.position = new Vector3(_randomPosX, 8.0f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }

            Destroy(this.gameObject);
        }

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
