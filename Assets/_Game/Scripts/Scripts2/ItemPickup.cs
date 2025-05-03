using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item itemData;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory.Instance.AddItem(itemData);
            Destroy(gameObject);
        }
    }
}
