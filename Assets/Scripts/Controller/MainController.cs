using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainController : MonoBehaviour
{
    public static int currentUnits;
    [SerializeField] private Tower[] _tower;
    [SerializeField] private TransitionScenes _transit;
    private void Awake()
    {
        _transit.StartCor("");
    }
    private void Start()
    {
        currentUnits = 0;
        TextController.updatePlayerUI?.Invoke();
        StartCoroutine(StartAddMoney());
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
