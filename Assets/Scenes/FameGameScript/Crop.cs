using System;
using UnityEngine;

public class Crop : MonoBehaviour
{
    [SerializeField] private string name;
    public Sprite icon;

    public Action useAction;

    void Start()
    {
        useAction += Use;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Get();
        }
    }

    public void Get()
    {
        // 인벤토리에 작물 추가

        if (FarmGameManager.Instance.item.CheckItemCount())
        {
            FarmGameManager.Instance.item.GetItem(this);
            Debug.Log($"{name}을 획득하였습니다.");
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("인벤토리가 가득 찼습니다.");
        }
    }

    public void Use()
    {
        Debug.Log($"{name}을 사용했습니다.");
    }
}
