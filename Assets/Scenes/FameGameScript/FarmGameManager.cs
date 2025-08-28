using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using System;

public enum CameraState { Outside, Field, House, Animal, Board }

public class FarmGameManager : Singleton<FarmGameManager>
{
    public FarmFieldManager field;
    public FarmUIManager ui;
    public FarmItemManager item;

    public CameraState cameraState = CameraState.Outside;

    [SerializeField] private CinemachineClearShot clearShot;




    public void SetCameraState(CameraState newState)
    {
        if (cameraState != newState)
        {
            cameraState = newState;

            foreach (var camera in clearShot.ChildCameras)
                camera.Priority = 1;

            clearShot.ChildCameras[(int)cameraState].Priority = 10;
        }
    }

}
