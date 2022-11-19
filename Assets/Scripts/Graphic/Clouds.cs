using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    [SerializeField] private GameObject[] _clouds;
    [SerializeField] private int _cloudCount;

    private void Start()
    {
        StartCoroutine(StartClouds());
    }

    private IEnumerator StartClouds()
    {
        List<GameObject> gameObjects = new List<GameObject>();    
        for (int i = 0; i < _cloudCount; i++)
        {
            GameObject obj = Instantiate(_clouds[Random.Range(0,_clouds.Length)]);
            obj.transform.position = new Vector3(Random.Range(-29f,29f), Random.Range(12f,14f), 0);
            gameObjects.Add(obj);
        }
        foreach (GameObject obj in gameObjects)
        {
            obj.GetComponent<Rigidbody2D>().velocity = Vector2.left * Random.Range(0.1f,0.5f) * (Random.value > 0.5f ? 1 : -1);
            yield return null;
        }
    }
}
