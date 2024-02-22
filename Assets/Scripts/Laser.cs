using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.0f;
    private bool _isEnemyLaser = false;
    private bool _isEnemyBehind = false;

    void Update()
    {
        if (_isEnemyLaser == false || _isEnemyBehind == true)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }
    }

    void MoveUp()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y > 8.0f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    void MoveDown()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -8.0f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void SetEnemyLaser()
    {
        _isEnemyLaser = true;
    }

    public void SetEnemyBehind()
    {
        _isEnemyBehind = true;
    }

    public void UnsetEnemyBehind()
    {
        _isEnemyBehind = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isEnemyLaser && other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }
        }
    }
}