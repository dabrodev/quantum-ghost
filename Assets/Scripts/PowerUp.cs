using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    private int _powerupID; // 0=Tripleshot 1=Speed 2=Shields 3=Ammo 4=Health 5=Slowdown

    // [SerializeField] 
    // private AudioClip _clip; // Alternative solution

    private AudioSource _audioSource;
    private UIManager _uiManager;

    private float _step;
    private float _pickupCollectSpeed = 6.0f;
    private Player _player;

    private void Start()
    {
        _audioSource = GameObject.Find("PowerupSound").GetComponent<AudioSource>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -8.0f)
        {
            Destroy(this.gameObject);
        }

        if (Input.GetKey(KeyCode.C))
        {
            _step = _pickupCollectSpeed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _step);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // AudioSource.PlayClipAtPoint(_clip, transform.position, 1.0f); // Alternative solution
            _audioSource.Play();
            Destroy(this.gameObject);
            Player player = other.transform.GetComponent<Player>();

            switch(_powerupID)
            {
                case 0:
                    player.TripleShotActive();
                    break;
                case 1:
                    player.SpeedActive();
                    break;
                case 2:
                    player.ShieldActive();
                    _uiManager.ShowShieldStrength();
                    break;
                case 3:
                    player.RefillAmmo();
                    break;
                case 4:
                    player.HealthCollected();
                    break;
                case 5:
                    player.MultiDirShotActive();
                    break;
                case 6:
                    player.SlowdownActive();
                    break;
                default:
                    Debug.LogWarning("Unknown PowerUp ID: " + _powerupID);
                    break;
            }
        }
    }
}
