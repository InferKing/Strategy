using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Singleton : MonoBehaviour
{
    public static Singleton Instance { get; private set; }
    public Player Player { get; private set; }
    private void Awake()
    {
        Player = new Player(500,100,5000);
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        Player = new Player(500, 100, 5000);
    }
}
