using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField] private GameObject[] _arrows;
    public void SetArrows(bool left, bool right)
    {
        _arrows[0].SetActive(left);
        _arrows[1].SetActive(right);
    }
}
