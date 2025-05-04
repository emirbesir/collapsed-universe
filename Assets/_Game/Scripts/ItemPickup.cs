using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public GameObject item1; // Sahnedeki "item1" GameObject'i, Inspector'dan atamalýsýn.

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Oyuncu ile çarpýþma kontrolü
        {
            if (item1 != null)
                item1.SetActive(true); // item1'i aktif hale getir

            Destroy(gameObject); // Üçgen objesini sahneden kaldýr
        }
    }
}
