using System;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    
    public CinemachineVirtualCamera playerFollowerCam;
    
    public CinemachineVirtualCamera beginningCam, portalCam;

    private void Start()
    {
        Instance = this;

        BeginningPriority();
    }

    public void BeginningPriority()
    {
        ClearPriorities();
        
        beginningCam.Priority = 1;
    }

    public void PortalPriority()
    {
        ClearPriorities();
        
        portalCam.Priority = 1;
    }

    public void PlayerFollowPriority()
    {
        ClearPriorities();
        
        playerFollowerCam.Priority = 1;
    }

    private void ClearPriorities()
    {
        beginningCam.Priority = 0;
        portalCam.Priority = 0;
        playerFollowerCam.Priority = 0;
    }
}
