using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int _powerupID; //0 = Triple Shot 1 = Speed 2 = Shields

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
                    // player.ShieldsActive();
                    break;
                default:
                    Debug.LogWarning("Unknown PowerUp ID: " + _powerupID);
                    break;
            }

        }
    }
}
