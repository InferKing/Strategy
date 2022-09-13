using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainController : MonoBehaviour
{
    [SerializeField] private Tower[] _tower;
    private void Start()
    {
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
