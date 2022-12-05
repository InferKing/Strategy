using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public void OnStartPressed()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
    }
}
