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
        PlayerMovement();       

    }


    void PlayerMovement()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        // transform.Translate(Vector3.right * horizontalMovement * _speed * Time.deltaTime );
        // transform.Translate(Vector3.up * verticalMovement * _speed * Time.deltaTime);

        // transform.Translate(new Vector3(horizontalMovement, verticalMovement, 0) * _speed * Time.deltaTime);

        Vector3 direction = new Vector3(horizontalMovement, verticalMovement, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        /*
        if (transform.position.y > 6.0f)
        {
            transform.position = new Vector3(transform.position.x, 6.0f, 0);
        }
        else if (transform.position.y < -4.0f)
        {
            transform.position = new Vector3(transform.position.x, -4.0f, 0);
        }
        */

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.0f, 6.0f), 0);

        // Teleportation

        if (transform.position.x > 9.4f)
        {
            transform.position = new Vector3(-9.4f, transform.position.y, 0);
        }
        else if (transform.position.x < -9.4f)
        {
            transform.position = new Vector3(9.4f, transform.position.y, 0);
        }
    }
}
