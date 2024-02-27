using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 3.5f, 0), 5f * Time.deltaTime);
    }
}
