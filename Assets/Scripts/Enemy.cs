using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 4.0f;
    private float _randomPosX;
    private Player _player;
    private Animator _anim;
    private float _fireRate;
    private float _nextFire = 0;

    [SerializeField]
    private GameObject _enemyLaserPrefab;

    [SerializeField]
    private AudioClip _explosionSoundClip;
    private AudioSource _audioSource;
    

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();

        _audioSource.clip = _explosionSoundClip;

        if (_player == null)
        {
            Debug.LogError("The Player is NULL");
        }

        if (_audioSource == null) {
            Debug.LogError("The Audio Source is NULL");
        }

        _anim = GetComponent<Animator>();

        if (_anim == null)
        {
            Debug.LogError("The Animator is NULL");
        }
    }

    void Update()
    {
        EnemyMovement();

        if (Time.time > _nextFire)
        {
            FireEnemyLaser();
        }
    }

    void EnemyMovement() {

        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -6.0f)
        {
            RandomPos();
            transform.position = new Vector3(_randomPosX, 8.0f, 0);
        }
    }

    void FireEnemyLaser()
    {
        _fireRate = Random.Range(3f, 7f);
        _nextFire = Time.time + _fireRate;
        
        GameObject enemyLaser = Instantiate(_enemyLaserPrefab, transform.position, Quaternion.identity);
        Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();

        for (int i = 0; i < lasers.Length; i++)
        {
            lasers[i].SetEnemyLaser();
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

            _anim.SetTrigger("OnEnemyDeath");
            _audioSource.Play();
            _speed = 0;
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.0f);
        }

        if (other.tag == "Laser")
        {
            _anim.SetTrigger("OnEnemyDeath");
            _audioSource.Play();
            _speed = 0;
            Destroy(other.gameObject);
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.0f);


            if (_player != null)
            {
                _player.AddScore(10);
            }
        }
    }

    void RandomPos()
    {
        _randomPosX = Random.Range(-9.0f, 9.0f);
    }
}
