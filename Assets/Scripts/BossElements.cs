using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossElements : MonoBehaviour
{
    [SerializeField]
    private int _bossID;
    private bool _greyEnemyMove = false;

    [SerializeField]
    private GameObject _laser;
    private Animator _anim;
    private AudioSource _audioSource;

    [SerializeField]
    private float _fireRate = 3f;
    private float _fireRateRed = 2f;
    private float _fireRateYellow = 12f;
    private float _nextFire = 0;
    private bool _startFiring = false;
    int[] strengths = new int[] { 3, 3, 3, 5, 5, 6, 6 };

    void Start()
    {
        StartCoroutine(EnemiesMovementRoutine());

        _anim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_greyEnemyMove)
        {
            EnemiesMovement();
        }
                FireLaser(); 
    }

    void EnemiesMovement()
    {
    
        switch (_bossID)
        {
            case 1:
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 1f, 0), 3f * Time.deltaTime);
                break;
            case 2:
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(-4f, 1f, 0), 3f * Time.deltaTime);
                break;
            case 3:
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(4f, 1f, 0), 3f * Time.deltaTime);
                break;
            case 4:
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(-6f, transform.position.y, 0), 3f * Time.deltaTime);
                break;
            case 5:
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(6f, transform.position.y, 0), 3f * Time.deltaTime);
                break;
            case 6:
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(-2f, 3f, 0), 3f * Time.deltaTime);
                break;
            case 7:
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(2f, 3f, 0), 3f * Time.deltaTime);
                break;
            default:
                Debug.LogError("Warnign, unknown boss id element: " + _bossID);
                break;
        }
    }

    void FireLaser()
    {
        

        if (_bossID == 1 || _bossID == 2 || _bossID == 3)
        {
            if (Time.time > _nextFire && _startFiring == true)
            {
                _nextFire = Time.time + _fireRate;
                GameObject enemyLaser = Instantiate(_laser, transform.position - new Vector3(0, 1.5f, 0), Quaternion.identity);
                enemyLaser.GetComponent<Laser>().SetEnemyLaser();
            }
        }

        if (_bossID == 6 || _bossID == 7) {

            if (Time.time > _nextFire && _startFiring == true)
            {
                _nextFire = Time.time + _fireRateYellow;

                StartCoroutine(LaserBeamCoroutine());
            }
        }

        if (_bossID == 4)

        {
            if (Time.time > _nextFire && _startFiring == true)
            {
                _nextFire = Time.time + _fireRateRed;
                GameObject enemyLaser = Instantiate(_laser, transform.position - new Vector3(0, 1.5f, 0), Quaternion.identity);

                enemyLaser.GetComponent<Laser>().SetEnemyLaser();
                if (GameObject.Find("Player").transform.position.x < -9f)
                {
                    enemyLaser.GetComponent<Laser>().SetYellowEnemy(true);
                }
                else
                {
                    enemyLaser.GetComponent<Laser>().SetYellowEnemy(false);
                }
            }
        }

        if (_bossID == 5)

        {
            if (Time.time > _nextFire && _startFiring == true)
            {
                _nextFire = Time.time + _fireRateRed;
                GameObject enemyLaser = Instantiate(_laser, transform.position - new Vector3(0, 1.5f, 0), Quaternion.identity);
                enemyLaser.GetComponent<Laser>().SetEnemyLaser();
                if (GameObject.Find("Player").transform.position.x > 9f)
                {
                    enemyLaser.GetComponent<Laser>().SetYellowEnemy(true);
                }
                else
                {
                    enemyLaser.GetComponent<Laser>().SetYellowEnemy(false);
                }
            }
        }
    }

    void DamageBoss()
    {
        for (int i=0; i < strengths.Length; i++)
        {
            if (_bossID == i+1 )
            {
                int alive = --strengths[i];
                Debug.Log("Boss alive: " + alive);
                if (alive == 0)
                {
                    Destroy(this.gameObject, 2f);
                    _anim.SetTrigger("OnEnemyDeath");
                    _audioSource.Play();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Laser")
        {

            DamageBoss();
            Destroy(other.gameObject);
        }
    }

    IEnumerator EnemiesMovementRoutine()
    {
        yield return new WaitForSeconds(5f);
        _greyEnemyMove = true;
        yield return new WaitForSeconds(2f);
        _startFiring = true;
    }

    IEnumerator FireCoroutine()
    {
        yield return new WaitForSeconds(10f);
        FireLaser();
    }

    IEnumerator LaserBeamCoroutine()
    {
        yield return new WaitForSeconds(2f);
        transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        transform.GetChild(0).gameObject.SetActive(false);
       
      

    }
}
