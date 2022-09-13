using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mountain : MonoBehaviour
{
    [SerializeField] private GameObject _camera;

    private void LateUpdate()
    {
        transform.position = new Vector3(_camera.transform.position.x * 0.95f, transform.position.y, transform.position.z);
    }
}
