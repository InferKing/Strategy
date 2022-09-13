using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShader : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sr;
    private Material _material;
    void Start()
    {
        _material = _sr.material;
        StartCoroutine(TestShad());
    }
    private IEnumerator TestShad()
    {
        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            _material.SetFloat("Float",i);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
