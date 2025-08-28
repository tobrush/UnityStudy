using Unity.Cinemachine;
using UnityEditor.Search;
using UnityEngine;

public class AnimalEvent : MonoBehaviour
{
    [SerializeField] private GameObject flag;
    private BoxCollider boxCollider;

    private float timer;
    private bool isTimer;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        if (!isTimer)
            return;

        timer += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTimer = true;
            SetRandomPosition();

            FarmGameManager.Instance.SetCameraState(CameraState.Animal);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"깃발 찾는데 걸린 시간은 {timer:F1}초입니다.");
            isTimer = false;
            timer = 0f;

            SetFlag(Vector3.zero, false);
            FarmGameManager.Instance.SetCameraState(CameraState.Outside);
        }
    }

    private void SetRandomPosition()
    {
        float randomX = Random.Range(boxCollider.bounds.min.x, boxCollider.bounds.max.x);
        float randomZ = Random.Range(boxCollider.bounds.min.z, boxCollider.bounds.max.z);

        var randomPos = new Vector3(randomX, 0f, randomZ);

        SetFlag(randomPos, true);
    }

    private void SetFlag(Vector3 pos, bool isActive)
    {
        flag.transform.SetParent(transform);
        flag.transform.position = pos;
        flag.SetActive(isActive);
    }
}