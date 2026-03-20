using System;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private Canvas CrosshairCanvas;
    
    void OnEnable()
    {
        //player.OnStateUpdated += StateUpdate;
    }

    void OnDisable()
    {
        //player.OnStateUpdated -= StateUpdate;
    }

    private void Start()
    {
    
    }

    private void StateUpdate()
    {
        
    }
}
