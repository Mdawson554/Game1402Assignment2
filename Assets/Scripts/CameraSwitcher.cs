using System;
using Unity.Cinemachine;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineCamera explorerCamera;
    [SerializeField] private CinemachineCamera aimCamera;
    [SerializeField] private PlayerController playerController;

    private void OnEnable()
    {
        playerController.OnStateUpdated += SwitchCamera;
    }

    private void OnDisable()
    {
        playerController.OnStateUpdated -= SwitchCamera;
    }

    private void SwitchCamera(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.EXPLORE:
                explorerCamera.Prioritize();
                break;
            case PlayerState.AIM:
                aimCamera.Prioritize();
                break;
            default:
                break;
        }
    }

}
