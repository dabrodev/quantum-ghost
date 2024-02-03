﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    private float _speedMultiplier = 2f;
    [SerializeField]
    private float _laserOffset = 0.8f;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _nextFire = 0.0f;
    [SerializeField]
    private int _lives = 3;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private bool _isTripleShotActive = false;
    [SerializeField]
    private bool _isSpeedActive = false;

    private SpawnManager _spawnManager;

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    void Update()
    {
        PlayerMovement();

        if (Input.GetKey(KeyCode.Space) && Time.time > _nextFire)
        {
            FireLaser();
        }
    }

    void PlayerMovement()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalMovement, verticalMovement, 0);


        if (_isSpeedActive == false)
        {
            transform.Translate(direction * _speed * Time.deltaTime);

        }
        else
        {
            transform.Translate(direction * _speed * _speedMultiplier * Time.deltaTime);
        }


        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.0f, 6.0f), 0);

        if (transform.position.x > 9.4f)
        {
            transform.position = new Vector3(-9.4f, transform.position.y, 0);
        }
        else if (transform.position.x < -9.4f)
        {
            transform.position = new Vector3(9.4f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        _nextFire = Time.time + _fireRate;

        if(_isTripleShotActive)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, _laserOffset, 0), Quaternion.identity);

        }

    }

    public void Damage()
    {
        _lives--;


        if (_lives < 1)
        {
            if (_spawnManager == null)
            {
                Debug.LogError("The Spawn Manager is NULL!");
            }
            else
            {
                _spawnManager.onPlayerDeath();
            }
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutune());
    }

    public void SpeedActive()
    {
        _isSpeedActive = true;
        
        StartCoroutine(SpeedPowerDownRoutune());
    }


    IEnumerator TripleShotPowerDownRoutune()
    {
        yield return new WaitForSeconds(5);
        _isTripleShotActive = false;
    }

    IEnumerator SpeedPowerDownRoutune()
    {
        yield return new WaitForSeconds(5);
        _isSpeedActive = false;
    }

}
