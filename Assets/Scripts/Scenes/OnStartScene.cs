using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStartScene : MonoBehaviour
{
    public void Start()
    {
        StartCoroutine(StartScene());
    }
    private IEnumerator StartScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.StartScene("Menu");
    }
}
