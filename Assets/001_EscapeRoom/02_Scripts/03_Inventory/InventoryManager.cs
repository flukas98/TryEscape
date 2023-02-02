using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryManager : MonoBehaviour
{
  #region Singleton
  public static InventoryManager Instance { get; private set; }

  private void Awake()
  {
    // If there is an instance, and it's not me, delete myself.
    if (Instance != null && Instance != this)
      Destroy(this);
    else
      Instance = this;
  }
  #endregion

  [Serializable]
  private class ItemCapacity
  {
    public string ItemName;
    public int Capacity;
  }
  
  public Dictionary<string, List<Item>> Inventory;
  [SerializeField] private List<UI_Slot> InventorySlots;
  [SerializeField] private List<ItemCapacity> Capacities;

  private void Start()
  {
    Inventory = new Dictionary<string, List<Item>>(InventorySlots.Count);
  }

  private void Update()
  {
    // check if key is pressed
    if (!UI_Settings.ActiveUIExists())
      KeyPressCheck();
  }

  public void StoreItem(Item pickableItem)
  {
    var itemName = pickableItem.ItemName;
    var itemCapacity = Capacities.SingleOrDefault(x => x.ItemName == itemName);
    var capacity = itemCapacity != null ? itemCapacity.Capacity : 1;

    if (Inventory.ContainsKey(itemName))
    {
      if (Inventory[itemName].Count < capacity)
      {
        Inventory[itemName].Add(pickableItem);
        pickableItem.ItemState = ItemState.IN_INVENTORY;
        pickableItem.gameObject.SetActive(false);
      }
      else
      {
        throw new Exception("No more capacity for item.");
      }
    }
    else if (Inventory.Count != InventorySlots.Count)
    {
      var objectToStore = new List<Item>() { pickableItem };
      Inventory.Add(itemName, objectToStore);
      pickableItem.ItemState = ItemState.IN_INVENTORY;
      pickableItem.gameObject.SetActive(false);
    }

    if (pickableItem.ItemType == ItemType.KEY)
      UI_KeyLockPanel.Instance.AddKeyItemToPanel(pickableItem);

    UpdateInventoryUI();
  }

  public void RemoveItem(string itemType)
  {
    if (Inventory.ContainsKey(itemType))
    {
      Inventory[itemType].RemoveAt(0);
    }
    if (Inventory[itemType].Count == 0)
    {
      UI_KeyLockPanel.Instance.RemoveKeyItemFromKeyPanel(itemType);
      Inventory.Remove(itemType);
    }

    UpdateInventoryUI();
  }

  private void KeyPressCheck()
  {
    int? index = null;

    if (Input.GetKeyUp(KeyCode.Alpha1) || Input.GetKeyUp(KeyCode.Keypad1))
      index = 0;
    if (Input.GetKeyUp(KeyCode.Alpha2) || Input.GetKeyUp(KeyCode.Keypad2))
      index = 1;
    if (Input.GetKeyUp(KeyCode.Alpha3) || Input.GetKeyUp(KeyCode.Keypad3))
      index = 2;
    if (Input.GetKeyUp(KeyCode.Alpha4) || Input.GetKeyUp(KeyCode.Keypad4))
      index = 3;
    if (Input.GetKeyUp(KeyCode.Alpha5) || Input.GetKeyUp(KeyCode.Keypad5))
      index = 4;
    if (Input.GetKeyUp(KeyCode.Alpha6) || Input.GetKeyUp(KeyCode.Keypad6))
      index = 5;
    if (Input.GetKeyUp(KeyCode.Alpha7) || Input.GetKeyUp(KeyCode.Keypad7))
      index = 6;
    if (Input.GetKeyUp(KeyCode.Alpha8) || Input.GetKeyUp(KeyCode.Keypad8))
      index = 7;
    if (Input.GetKeyUp(KeyCode.Alpha9) || Input.GetKeyUp(KeyCode.Keypad9))
      index = 8;
    

    if (!index.HasValue)
      return;

    var inventorySlot = Inventory.ElementAtOrDefault(index.Value);

    // if there exists item in that slot
    if (!inventorySlot.Equals(default(KeyValuePair<string, List<Item>>)))
    {
      var item = Inventory.ElementAt(index.Value).Value.First();
      UI_ItemPanel.Instance.ShowPanel(item);
    }
  }

  public bool Contains(string itemType)
  {
    return Inventory.ContainsKey(itemType);
  }

  public void UpdateInventoryUI()
  {
    for (int i = 0; i < InventorySlots.Count; i++)
    {
      if (i < Inventory.Count)
      {
        InventorySlots[i].AddItems(Inventory.ElementAt(i).Value);
      }
      else
      {
        InventorySlots[i].ClearSlot();
      }
    }
  }
}
