using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBehaviour : MonoBehaviour
{
    private float scal;
    private Vector3 vect;
    void Update()
    {
        vect = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Debug.Log(Vector2.Angle(vect, Vector2.down*2));
        transform.position = new Vector3(vect.x,transform.position.y,transform.position.z);
        
    }
}
