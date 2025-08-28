using UnityEngine;

public class FarmItemManager : MonoBehaviour
{
    [SerializeField] private Transform slotGroup;
    [SerializeField] private FarmSlot[] slots;
    [SerializeField] private GameObject slotPrefab;

    [SerializeField] private int slotAmount = 20;
    private int itemCount = 0;

    void Start()
    {
        for (int i = 0; i < slotAmount; i++)
            Instantiate(slotPrefab, slotGroup);

        slots = slotGroup.GetComponentsInChildren<FarmSlot>();
    }

    public void GetItem(Crop crop)
    {
        foreach (var slot in slots)
        {
            if (slot.isEmpty)
            {
                slot.AddCrop(crop);
                itemCount++;
                crop.useAction += UseItem;
                break;
            }
        }
    }

    public bool CheckItemCount()
    {
        bool result = itemCount < slotAmount;

        return result;
    }

    public void UseItem()
    {
        itemCount--;
    }
}
