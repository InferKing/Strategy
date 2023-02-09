using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrans : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    private void Awake()
    {
        MusicTrans[] musics = FindObjectsOfType<MusicTrans>();
        if (musics.Length == 2)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        StartCoroutine(Delay());
    }
    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        musicSource.Play();
    }
}
