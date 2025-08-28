using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FarmSlot : MonoBehaviour
{
    private Crop crop;
    [SerializeField] private Image slotImage;
    [SerializeField] private Button slotButton;

    public bool isEmpty = true;

    void Awake()
    {
        slotButton.onClick.AddListener(UseCrop);
    }

    void OnEnable()
    {
        slotImage.gameObject.SetActive(!isEmpty);
        slotButton.interactable = !isEmpty;
    }

    public void AddCrop(Crop crop)
    {
        isEmpty = false;

        this.crop = crop;
        slotImage.sprite = crop.icon;
    }

    private void UseCrop()
    {
        if (crop != null)
        {
            crop.Use();
            isEmpty = true;
            slotButton.interactable = false;
            slotImage.gameObject.SetActive(false);
            crop.useAction?.Invoke();
        }
    }
}
