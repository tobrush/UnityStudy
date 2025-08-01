using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private IItemObject item;
    public Image slotImage;
    public Button buttonUI;
    public int itemAmount;

    public bool isEmpty = true;

    void Awake()
    {
        buttonUI.onClick.AddListener(UseItem);
    }

    void OnEnable()
    {
        buttonUI.enabled = !isEmpty;
        slotImage.gameObject.SetActive(!isEmpty);
    }

    public void AddItem(IItemObject newItem)
    {
        item = newItem;

        if (isEmpty)
        {
            isEmpty = false;
            slotImage.sprite = newItem.Icon;
            slotImage.SetNativeSize();
        }

        itemAmount++;
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
            itemAmount--;

            if (itemAmount <= 0)
                Clear();
        }
    }

    public void Clear()
    {
        item = null;
        isEmpty = true;
    }
}