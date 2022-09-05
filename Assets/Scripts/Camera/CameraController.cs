using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [SerializeField] private ArrowController _arrowController;
    public float leftPosX, rightPosX, speed;

    private void Start()
    {
        _arrowController.SetArrows(true, true);
    }
    private void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.mousePosition.x > Screen.width / 1.3f)
            {
                transform.Translate(Vector3.right * Time.deltaTime * speed);
            }
            else if (Input.mousePosition.x < Screen.width - Screen.width / 1.3f)
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            }
        }
        if (transform.position.x >= rightPosX)
        {
            transform.position = new Vector3(rightPosX, transform.position.y, transform.position.z);
            _arrowController.SetArrows(true, false);
        }
        else if (transform.position.x <= leftPosX)
        {
            transform.position = new Vector3(leftPosX, transform.position.y, transform.position.z);
            _arrowController.SetArrows(false, true);
        }
        else
        {
            _arrowController.SetArrows(true, true);
        }
    }
}
