using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "Model", menuName = "ScriptableObjects/Units", order = 1)]
public class Model : ScriptableObject
{
    public GameObject[] gameObjects;
}
