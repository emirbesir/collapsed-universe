using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public GameObject inventoryPanel;
    public GameObject slotPrefab; // UI Button
    public Transform slotParent;
    public List<Item> items = new List<Item>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddItem(Item newItem)
    {
        items.Add(newItem);
        GameObject slot = Instantiate(slotPrefab, slotParent);
        slot.GetComponent<Image>().sprite = newItem.icon;
        slot.GetComponent<Button>().onClick.AddListener(() => UseItem(newItem, slot));
    }

    public void UseItem(Item item, GameObject slotObj)
    {
        Debug.Log($"Kullanýldý: {item.itemName}");
        Destroy(slotObj);
        items.Remove(item);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }
    }
}
