using UnityEngine;

public class HpPotion : MonoBehaviour, IItemObject
{
    public ItemManager Inventory { get; set; }

    public GameObject Obj { get; set; }
    public string ItemName { get; set; }
    public Sprite Icon { get; set; }

    void Start()
    {
        Inventory = FindFirstObjectByType<ItemManager>();

        Obj = gameObject;
        ItemName = name;
        Icon = GetComponent<SpriteRenderer>().sprite;
    }

    public void Get()
    {
        gameObject.SetActive(false);

        Inventory.GetItem(this);
    }

    public void Use()
    {
        GameObject player = GameObject.Find("Player");

        //player.GetComponent<KnightController_Keyboard>()

        //Debug.Log("Hp ���� ���");
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Get();
        }
    }
}