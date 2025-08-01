using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private GameObject[] items;
    [SerializeField] private Transform slotGroup;
    public Slot[] slots;

    void Start()
    {
        slots = slotGroup.GetComponentsInChildren<Slot>(true);
    }

    public void DropCoin(Vector3 dropPos)
    {
        var randomIndex = Random.Range(0, items.Length); // ���� �ε��� ����

        GameObject item = Instantiate(items[randomIndex], dropPos, Quaternion.identity); // ������ ����

        Rigidbody2D itemRb = item.GetComponent<Rigidbody2D>();

        itemRb.AddForceX(Random.Range(-2f, 2f), ForceMode2D.Impulse);
        itemRb.AddForceY(3f, ForceMode2D.Impulse);

        float ranPower = Random.Range(-1.5f, 1.5f);
        itemRb.AddTorque(ranPower, ForceMode2D.Impulse);
    }
    public void GetItem(IItemObject item)
    {
        foreach (var slot in slots)
        {
            if (slot.isEmpty)
            {
                slot.AddItem(item);
                break;
            }
        }
    }
    public void DropItem(Vector3 dropPos)
    {
        // ���� ������ ����
        var randomIndex = Random.Range(0, items.Length);

        // ������ ����
        GameObject item = Instantiate(items[randomIndex], dropPos, Quaternion.identity);
        Rigidbody2D itemRb = item.GetComponent<Rigidbody2D>();

        // ������ �������� ���� ���ϴ� ���
        itemRb.AddForceX(Random.Range(-2f, 2f), ForceMode2D.Impulse);
        itemRb.AddForceY(3f, ForceMode2D.Impulse);

        // ������ ȸ������ ���� ���ϴ� ���
        float ranPower = Random.Range(-1.5f, 1.5f);
        itemRb.AddTorque(ranPower, ForceMode2D.Impulse);
    }
}