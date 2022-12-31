using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseGraphic : MonoBehaviour
{
    [SerializeField] private GameObject _cursorPos;
    [SerializeField] private ParticleSystem _particleSystem;
    private Vector3 pos;
    private void OnEnable()
    {
        CameraController.OnCameraTranslated += TranslateMouse;
    }
    private void OnDisable()
    {
        CameraController.OnCameraTranslated -= TranslateMouse;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _cursorPos.transform.position = new Vector3(pos.x, pos.y, 10);
            _particleSystem.Play();
        }
    }
    private void TranslateMouse(Vector3 pos)
    {
        _cursorPos.transform.Translate(pos);
    }
}
