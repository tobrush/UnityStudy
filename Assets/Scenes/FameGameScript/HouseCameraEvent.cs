using Unity.Cinemachine;
using UnityEngine;
using System;

public class HouseCameraEvent : MonoBehaviour
{
    [SerializeField]
    private CinemachineClearShot clearShaot;
    [SerializeField]
    private GameObject houseLoop;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            houseLoop.SetActive(false);
            FarmGameManager.Instance.SetCameraState(CameraState.House);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            houseLoop.SetActive(true);
            FarmGameManager.Instance.SetCameraState(CameraState.Outside);
        }
    }
}
