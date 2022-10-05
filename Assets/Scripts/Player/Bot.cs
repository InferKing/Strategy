using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum BotState
{

}
public class Bot : MonoBehaviour
{
    [SerializeField] private Tower _tower;
    private BotState _state;
    public Difficult difficult { private get; set; }
    
    private void CheckEnemies()
    {
        
    }
}
