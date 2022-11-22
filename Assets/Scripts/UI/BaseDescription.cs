using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BaseDescription : MonoBehaviour
{
    [SerializeField] private GameObject _description; 
    public void OnPointerEnter()
    {
        _description.SetActive(true);
    }
    public void OnPointerExit()
    {
        _description.SetActive(false);
    }
}
