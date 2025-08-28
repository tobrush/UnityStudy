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
        // �κ��丮�� �۹� �߰�

        if (FarmGameManager.Instance.item.CheckItemCount())
        {
            FarmGameManager.Instance.item.GetItem(this);
            Debug.Log($"{name}�� ȹ���Ͽ����ϴ�.");
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("�κ��丮�� ���� á���ϴ�.");
        }
    }

    public void Use()
    {
        Debug.Log($"{name}�� ����߽��ϴ�.");
    }
}
