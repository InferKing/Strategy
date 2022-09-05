using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private void Update()
    {
        if (transform.position.x < -30)
        {
            transform.position = new Vector3(29, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > 30)
        {
            transform.position = new Vector3(-29, transform.position.y, transform.position.z);
        }
    }
}
