using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemy : MonoBehaviour
{

    private float _speed = 2f;
    private bool _go = true;

    GameObject beamLaser;
    // Start is called before the first frame update
    void Start()
    {
        beamLaser = this.gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);



        if (transform.position.y < 5.0f)
        {
           
            if (_go)
            {
                StartCoroutine(LaserDownCoroutine());
                _go = false;
            }
        }
    }


    IEnumerator LaserDownCoroutine()
    {
        _speed = 0;
        yield return new WaitForSeconds(1);
        beamLaser.SetActive(true);
        yield return new WaitForSeconds(2);
        beamLaser.SetActive(false);
        yield return new WaitForSeconds(1);
        _speed = 15f;
        

    }




}
