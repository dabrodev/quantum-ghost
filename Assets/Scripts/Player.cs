using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _speed = 3.5f;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        //transform.Translate(Vector3.right * horizontalMovement * _speed * Time.deltaTime );
        //transform.Translate(Vector3.up * verticalMovement * _speed * Time.deltaTime);

        transform.Translate(new Vector3(horizontalMovement, verticalMovement, 0) * _speed * Time.deltaTime);
    }
}
