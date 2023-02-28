using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainController : MonoBehaviour
{
    public static Action<int, string> ShowFinish;
    public static int currentUnits;
    public static Hero hero = null;
    [SerializeField] private Tower[] _tower;
    [SerializeField] private Animator _finishUIAnim;
    private void Awake()
    {
        MusicTrans music = FindObjectOfType<MusicTrans>();
        if (music != null) Destroy(music.gameObject);
    }
    private void Start()
    {
        currentUnits = 0;
        TextController.updatePlayerUI?.Invoke();
        StartCoroutine(StartAddMoney());
        StartCoroutine(CheckHealth());
    }
    private IEnumerator CheckHealth()
    {
        while (_tower[0].health > 0 && _tower[1].health > 0)
        {
            yield return null;
        }
        StopCoroutine(StartAddMoney());
        yield return new WaitForSeconds(3);
        _finishUIAnim.SetBool("Idle", true);
        if (_tower[0].health > 0.01f * _tower[0].maxHealth)
        {
            ShowFinish?.Invoke(12, Constants.Congratulations[UnityEngine.Random.Range(0, Constants.Congratulations.Count)]);
        }
        else
        {
            ShowFinish?.Invoke(12, Constants.Lose[UnityEngine.Random.Range(0, Constants.Lose.Count)]);
        }
    }

    private IEnumerator StartAddMoney()
    {
        while (Singleton.Instance.Player.IsAlive())
        {
            yield return new WaitForSeconds(1.5f);
            _tower[0].CalculateHealth();
            _tower[1].CalculateHealth();
            Singleton.Instance.Player.CalculateRep();
            TextController.updatePlayerUI?.Invoke();
        }
    }
}
