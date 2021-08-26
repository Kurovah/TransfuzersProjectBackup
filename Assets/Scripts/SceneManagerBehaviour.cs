using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerBehaviour : MonoBehaviour
{
    public Transform inventorySlots, itemGetNotifArea;
    public GameObject inventoryPanel, ItemGetNotifPrefab;
    //item index quantity
    public Dictionary<int, int> inventory;
    public MasterItemList itemList;
    // Start is called before the first frame update
    void Start()
    {
        inventory = new Dictionary<int, int>();
        UpdateInventoryUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            InvenNotifTest();
        }
    }

    void UpdateInventoryUI()
    {
        ClearAllInventoryUI();
        int i = 0;
        foreach (KeyValuePair<int, int> items in inventory)
        {
            InventorySlot slot = inventorySlots.GetChild(i).GetComponent<InventorySlot>();
            slot.itemSprite.sprite = itemList.items[items.Key].itemSprite;
            slot.counterText.text = items.Value.ToString();
            slot.itemSprite.gameObject.SetActive(true);
            i++;
        }
        Debug.Log("invertory size:" + inventory.Count);
    }

    void ClearAllInventoryUI()
    {
        foreach (Transform t in inventorySlots)
        {
            InventorySlot i = t.gameObject.GetComponent<InventorySlot>();
            i.itemSprite.sprite = null;
            i.itemSprite.gameObject.SetActive(false);
        }
        Debug.Log("cleared");
    }

    public void AddItem(int index, int amount)
    {
        if (inventory.ContainsKey(index))
        {
            inventory[index] += 1;
        }
        else
        {
            inventory.Add(index, 1);
        }
        UpdateInventoryUI();
    }

    public void RemoveItem(int index, int amount)
    {
        if (inventory.ContainsKey(index))
        {
            inventory[index] -= amount;
            if (inventory[index] <= 0)
            {
                inventory.Remove(index);
            }
        }
        UpdateInventoryUI();
    }

    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }

    public void InvenNotifTest()
    {
        var notif = Instantiate(ItemGetNotifPrefab, itemGetNotifArea);
        notif.GetComponent<CollectionNotifBehaviour>().SetText("Testy Item");
    }
}
