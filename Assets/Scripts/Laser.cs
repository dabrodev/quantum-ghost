using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.0f;
    [SerializeField]
    private bool _isEnemyLaser = false;
    private bool _isEnemyBehind = false;
    private bool _pickupBehavior = false;
    private Vector3  _actualPowerupPos;
    private Rigidbody2D _laserRigidbody;
    private GameObject _enemy;
 
    private void Start()
    {
        _laserRigidbody = GetComponent<Rigidbody2D>();
    }

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
        _enemy = gameObject.transform.parent.gameObject.transform.parent.gameObject;

        if (_pickupBehavior == false)
        {
            
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }
        else if (_pickupBehavior == true)
        {
            FirePowerup(_actualPowerupPos);  
        }

        if (transform.position.y < -10.0f)
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

    public void SetPickupPos(Vector3 powPos)
    {
        _actualPowerupPos = powPos;
        Debug.Log("Set pickup pos from Laser script");
    }

    public void PickupBehavior(bool status) {
        _pickupBehavior = status;
    }

    void FirePowerup(Vector3 powerupPos)
    {
        _laserRigidbody.AddForce(powerupPos * 20f);
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
        else if (other.tag == "Powerup")
        {
            Destroy(other.gameObject);
            //Destroy(this.gameObject);
            _enemy.GetComponent<Enemy>().PowerupDestroyed(true);
            Debug.Log("PowerUp destroyed!!!!!!!!!!!!");
        }
    }


}