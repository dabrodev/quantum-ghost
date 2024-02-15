using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int _powerupID; // =Triple-Shot 1=Speed 2=Shields 3=Ammo 4=Health

    // [SerializeField] 
    // private AudioClip _clip; // Alternative solution

    private AudioSource _audioSource;
    private UIManager _uiManager;

    private void Start()
    {
        _audioSource = GameObject.Find("PowerupSound").GetComponent<AudioSource>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -8.0f)
        {
            Destroy(this.gameObject);
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
                    player.HealCollected();
                    break;
                default:
                    Debug.LogWarning("Unknown PowerUp ID: " + _powerupID);
                    break;
            }
        }
    }
}
