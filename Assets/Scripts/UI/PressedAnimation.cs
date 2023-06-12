using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressedAnimation : MonoBehaviour
{
    [SerializeField] private float _limit;
    [SerializeField] private AudioSource _audio;
    public void AnimationButton()
    {
        StopAllCoroutines();
        StartCoroutine(StartAnim());
    }
    private IEnumerator StartAnim()
    {
        _audio.Play();
        for (float i = 1; i >= _limit; i -= 0.04f)
        {
            transform.localScale = new Vector3(Mathf.Clamp(i, _limit, 1), Mathf.Clamp(i, _limit, 1), transform.lossyScale.z);
            yield return null;
        }
        for (float i = _limit; i <= 1; i += 0.04f)
        {
            transform.localScale = new Vector3(Mathf.Clamp(i, _limit, 1), Mathf.Clamp(i, _limit, 1), transform.lossyScale.z);
            yield return null;
        }
    }

}
