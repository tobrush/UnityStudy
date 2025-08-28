using Unity.Cinemachine;
using UnityEngine;

public class FarmCameraEvent : MonoBehaviour
{
    [SerializeField]
    private CinemachineClearShot clearShaot;

    public int InNumber;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            if (InNumber == 1)
            {
                FarmGameManager.Instance.SetCameraState(CameraState.Field);
                FarmGameManager.Instance.ui.ActivateFieldUI(true);
            }
               
            
            if (InNumber == 2)
                FarmGameManager.Instance.SetCameraState(CameraState.Animal);
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FarmGameManager.Instance.SetCameraState(CameraState.Outside);
            FarmGameManager.Instance.ui.ActivateFieldUI(false);
        }
    }
}
