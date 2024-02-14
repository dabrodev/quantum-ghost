using System.Collections;
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
    private int _ammoCount = 15;
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
    [SerializeField]
    private bool _isShieldActive = false;
    [SerializeField]
    private int _score = 0;

    [SerializeField]
    private GameObject _leftEngineFire, _rightEngineFire;

    [SerializeField]
    private AudioClip _laserSoundClip;
    private AudioSource _audioSource;


    private SpawnManager _spawnManager;
    private UIManager _uiManager;
    private GameManager _gameManager;
    [SerializeField]
    private int _shieldVolume;

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        _audioSource = GetComponent<AudioSource>();

        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL.");
        }
      
        if (_uiManager == null)
        {
            Debug.LogError("UI Manager is NULL.");
        }

        if (_gameManager == null)
        {
            Debug.LogError("Game Manager is NULL.");
        }

        if (_audioSource == null)
        {
            Debug.LogError("Audio Source on the player is NULL.");
        }
        else
        {
            _audioSource.clip = _laserSoundClip;
        }
    }

    void Update()
    {
        PlayerMovement();

        if (Input.GetKey(KeyCode.Space) && Time.time > _nextFire && _ammoCount > 0)
        {
            FireLaser();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _speed *= 2;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _speed /= 2;
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

        if (transform.position.x > 10.4f)
        {
            transform.position = new Vector3(-10.4f, transform.position.y, 0);
        }
        else if (transform.position.x < -10.4f)
        {
            transform.position = new Vector3(10.4f, transform.position.y, 0);
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

        if (_ammoCount > 0)
        {
            _ammoCount--;
        }

        _uiManager.UpdateAmmo(_ammoCount);


        _audioSource.Play();
    }

    public void Damage()
    {
        if (_isShieldActive != true)
        {
            _lives--;

            _uiManager.UpdateLives(_lives);
        }
        else
        {
            _shieldVolume--;
            _uiManager.DecreaseShieldStrength();

            if (_shieldVolume == 0)
            {
                this.gameObject.transform.GetChild(0).gameObject.SetActive(false); // deactivating shield
                _isShieldActive = false;
            }
        }

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
            _uiManager.GameOverShow();
            _gameManager.GameOver();
        }

        if (_lives == 2)
        {
            _rightEngineFire.SetActive(true);
        }
        else if (_lives == 1)
        {
            _leftEngineFire.SetActive(true);
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

    public void ShieldActive()
    {
        Debug.Log("Shield activated");
        _isShieldActive = true;
        _shieldVolume = 3;
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true); // activating Shield 
       // StartCoroutine(ShieldPowerDownCoroutine());        
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

    IEnumerator ShieldPowerDownCoroutine()
    {
        yield return new WaitForSeconds(5);
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        _isShieldActive = false;
    }

    public void AddScore(int points)
    {
        _score += points;

        if (_uiManager != null)
        {
            _uiManager.UpdateScore(_score);
        }
    }
}
