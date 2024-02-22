using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 3.0f;
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
    private Vector3 direction;
    private float _startXPosition;
    private int _randomSpecial = 0;
    private GameObject _enemyShield;
    private bool _isBehind = false;

    private Vector3 _laserPos;


    void Start()
    {

        _startXPosition = transform.position.x;

        direction = new Vector3(1f, -1f, 0);


        _enemyShield = this.gameObject.transform.GetChild(0).gameObject;


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
        IsBehindCheck();

        if (_randomSpecial == 1)
        {
            SpecialMovement();

        }
        else
        {
            BasicMovement();
        }

        StartCoroutine(RandomSpecialCoroutine());
        

        if (Time.time > _nextFire)
        {
            FireEnemyLaser();
        }
    }

    void BasicMovement() { 
       
        transform.Translate(Vector3.down * _speed * Time.deltaTime);


        if (transform.position.y < -6.0f)
        {
            RandomPos();
            transform.position = new Vector3(_randomPosX, 8.0f, 0);
        }
    }

    void SpecialMovement()
    {
        if (transform.position.x > _startXPosition + 2.0f)
        {
            direction = new Vector3(-1f, -1f, 0);
        }
        else if ( transform.position.x < _startXPosition - 2.0f)
        {
            direction = new Vector3(1f, -1f, 0);
        }

        transform.Translate(direction * _speed * Time.deltaTime);


        if (transform.position.y < -6.0f)
        {
            RandomPos();
            transform.position = new Vector3(_randomPosX, 8.0f, 0);
        }
    }

    void IsBehindCheck()
    {
        float playerHeight = 1.8f;

        if (_player != null)
        {
            if (transform.position.y + playerHeight < _player.transform.position.y)
            {
                Debug.Log("Enemy Behind");

                _isBehind = true;
            }
            else
            {
                Debug.Log("Enemy in Front");
                _isBehind = false;
            }
        }
    }

    void FireEnemyLaser()
    {
        _fireRate = Random.Range(3f, 5f);
        _nextFire = Time.time + _fireRate;

        if (_isBehind)
        {
            _laserPos = transform.position + new Vector3(0, 2.8f, 0);
        }
        else
        { 
            _laserPos = transform.position;
        }

        GameObject enemyLaser = Instantiate(_enemyLaserPrefab, _laserPos, Quaternion.identity);

        Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();


        for (int i = 0; i < lasers.Length; i++)
        {
            lasers[i].SetEnemyLaser();
            if (_isBehind)
            {
                lasers[i].SetEnemyBehind();
            }
            else
            {
                lasers[i].UnsetEnemyBehind();

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

       
            DestroyEnemy(other);

            if (player != null)
            {
                player.Damage();
            }
        }

        if (other.tag == "Laser")
        {

            DestroyEnemy(other);

            if (_player != null)
            {
                _player.AddScore(10);
            }
        }
    }

    void DestroyEnemy(Collider2D collider)
    {
        if (_enemyShield.activeSelf == false)
        {
            _anim.SetTrigger("OnEnemyDeath");
            _audioSource.Play();
            _speed = 0;
            Destroy(collider.gameObject);
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.0f);
        }
        else
        {
            _enemyShield.SetActive(false);
        }
    }

    void RandomPos()
    {
        _randomPosX = Random.Range(-9.0f, 9.0f);
    }




    IEnumerator RandomSpecialCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            _randomSpecial = 1;
            yield return new WaitForSeconds(5f);
            _randomSpecial = 0;
        }
    }
}
