using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TestCheat : MonoBehaviour
{
    [SerializeField] private ButtonController _controller;
    private string _cheatCode = "88888888";
    private void Update()
    {
        if (_controller.GetTutorial() != -2) return;
        _cheatCode += Input.inputString.ToLower();
        if (_cheatCode.Length > 8)
        {
            _cheatCode = _cheatCode.Substring(1, 8);
            switch (_cheatCode)
            {
                case "zoolkmnb":
                    Singleton.Instance.Player.TryMoneyTransaction(50000);
                    break;
                case "zxcvvcxz":
                    Singleton.Instance.Player.AddReputation(200);
                    break;
                case "bzzxxccm":
                    Singleton.Instance.Player.AddExperience(50000);
                    break;
            }
            TextController.updatePlayerUI?.Invoke();
        }
    }
}
