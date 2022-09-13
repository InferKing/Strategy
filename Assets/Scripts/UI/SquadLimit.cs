using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SquadLimit : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Tower _tower;

    private void OnEnable()
    {
        TextController.updatePlayerUI += SetLimit;
    }
    private void OnDisable()
    {
        TextController.updatePlayerUI -= SetLimit;
    }
    private void SetLimit()
    {
        _text.text = $"{MainController.currentUnits}/{_tower.GetLimitUnit()}";
    }
}
