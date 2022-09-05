using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private Animator[] _animators;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _leftX,_rightX, _speed;
    private void Start()
    {
        foreach (var animator in _animators)
        {
            animator.speed = _speed;
        }
        _rb.velocity = Vector2.left * (Random.Range(0, 2) * 2 - 1) * _speed;
    }
    private void Update()
    {
        if (transform.position.x > _rightX)
        {
            transform.position = new Vector3(_leftX + 1, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < _leftX)
        {
            transform.position = new Vector3(_rightX - 1, transform.position.y, transform.position.z);
        }
    }
}
