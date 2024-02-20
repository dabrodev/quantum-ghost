using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemy : MonoBehaviour
{

    private float _speed = 2f;
    private bool _go = true;
    private float _randomPosX;
    private Animator _anim;
    private AudioSource _audioSource;
    private Player _player;
    private bool _isRedEnemyDead = false;


    GameObject beamLaser;
    // Start is called before the first frame update
    void Start()
    {

        beamLaser = this.gameObject.transform.GetChild(0).gameObject;


        _anim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        _player = GameObject.Find("Player").GetComponent<Player>();

        if (_anim == null)
        {
            Debug.LogError("The Animator is NULL");
        }

        if (_player == null)
        {
            Debug.LogError("The Player is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);


        if (transform.position.y < 5.0f && _go)
        {
          
            StartCoroutine(LaserDownCoroutine());
            _go = false;
            
        }
        else if (transform.position.y < -6.0f)
        {
            Destroy(this.gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _isRedEnemyDead = true;
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
            Destroy(beamLaser.gameObject);
        }

        if (other.tag == "Laser")
        {
            _isRedEnemyDead = true;
            _anim.SetTrigger("OnEnemyDeath");
            _audioSource.Play();
            _speed = 0;
            Destroy(other.gameObject);
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2f);
            Destroy(beamLaser.gameObject);
            Debug.Log("Trigger Laser");


            if (_player != null)
            {
                _player.AddScore(20);
            }
        }

    }


    IEnumerator LaserDownCoroutine()
    {
        _speed = 0;
        yield return new WaitForSeconds(1);
        if (_isRedEnemyDead != true)
        {
            beamLaser.SetActive(true);
        }
            yield return new WaitForSeconds(2);
        if (_isRedEnemyDead != true)
        {
            beamLaser.SetActive(false);
        }
            yield return new WaitForSeconds(1);
            _speed = 15f;      

    }


    void RandomPos()
    {
        _randomPosX = Random.Range(-9.0f, 9.0f);
    }

}
