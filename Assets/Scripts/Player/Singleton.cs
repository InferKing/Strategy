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
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        Difficult difficult = (Difficult)PlayerPrefs.GetInt("Difficult");
        switch (difficult)
        {
            case Difficult.Easy:
                Player = new Player(750, 100, 1000);
                break;
            case Difficult.Medium:
                Player = new Player(550, 50, 0);
                break;
            case Difficult.Hard:
                Player = new Player(350, 0, 0);
                break;
        }
        
    }
}