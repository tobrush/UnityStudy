using UnityEngine;
using UnityEngine.UI;

public class FarmUIManager : MonoBehaviour
{
    [SerializeField] private GameObject outSideUI;
    [SerializeField] private GameObject fieldUI;
    [SerializeField] private GameObject houseUI;
    [SerializeField] private GameObject animalUI;

    [SerializeField] private GameObject seedUI;
    [SerializeField] private GameObject inventoryUI;

    [SerializeField] private Button seedButton;
    [SerializeField] private Button harvestButton;
    [SerializeField] private Button[] plantButtons;

    void Awake()
    {
        seedButton.onClick.AddListener(OnSeedButton);
        harvestButton.onClick.AddListener(OnHarvestButton);

        for (int i = 0; i < plantButtons.Length; i++)
        {
            int j = i;
            plantButtons[i].onClick.AddListener(() => FarmGameManager.Instance.field.SetPlant(j));
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    private void OnSeedButton()
    {
        FarmGameManager.Instance.field.SetState(FarmFieldManager.FieldState.Seed);
        seedUI.SetActive(true);
    }

    private void OnHarvestButton()
    {
        FarmGameManager.Instance.field.SetState(FarmFieldManager.FieldState.Harvest);
        seedUI.SetActive(false);
    }

    public void ActivateFieldUI(bool isActive)
    {
        fieldUI.SetActive(isActive);
    }
}