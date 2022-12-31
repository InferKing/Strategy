using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
public class CameraController : MonoBehaviour
{
    [SerializeField] private ArrowController _arrowController;
    public float leftPosX, rightPosX, speed;
    public static Action<Vector3> OnCameraTranslated; 

    private void Start()
    {
        _arrowController.SetArrows(true, true);
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
            OnCameraTranslated?.Invoke(Vector3.right*Time.deltaTime*speed);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
            OnCameraTranslated?.Invoke(Vector3.left * Time.deltaTime * speed);
        }
        if (transform.position.x > rightPosX)
        {
            OnCameraTranslated?.Invoke(Vector3.left * Time.deltaTime * speed);
            transform.position = new Vector3(rightPosX, transform.position.y, transform.position.z);
            _arrowController.SetArrows(true, false);
        }
        else if (transform.position.x < leftPosX)
        {
            OnCameraTranslated?.Invoke(Vector3.right * Time.deltaTime * speed);
            transform.position = new Vector3(leftPosX, transform.position.y, transform.position.z);
            _arrowController.SetArrows(false, true);
        }
        else
        {
            _arrowController.SetArrows(true, true);
        }
    }
}
