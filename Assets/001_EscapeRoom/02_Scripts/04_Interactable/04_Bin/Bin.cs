using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bin : Interactable
{
  #region Singleton
  public static Bin Instance { get; private set; }

  private void Awake()
  {
    // If there is an instance, and it's not me, delete myself.
    if (Instance != null && Instance != this)
      Destroy(this);
    else
      Instance = this;
  }
  #endregion

  public Dictionary<string, List<Item>> BinStorage = new Dictionary<string, List<Item>>();
  public List<UI_SelectableSlot> BinSlots;

  protected override void OnMouseUpAsButton()
  {
    if (CenterRaycastManager.Instance.IsPlayerInRange())
      UI_BinPanel.Instance.Enable();
  }

  public void StoreItemInBin(Item item)
  {
    var key = item.ItemName;

    if (BinStorage.ContainsKey(key))
    {
      BinStorage[key].Add(item);
    }
    else
    {
      var objectToStore = new List<Item>() { item };
      BinStorage.Add(key, objectToStore);
    }

    InventoryManager.Instance.RemoveItem(item.ItemName);
    item.ItemState = ItemState.IN_BIN;
    item.gameObject.SetActive(false);

    UpdateBinUI();
  }

  public void RestoreItemInInventory(string itemName)
  {
    var itemToRestore = BinStorage[itemName].First();

    if (BinStorage.ContainsKey(itemName))
      BinStorage[itemName].RemoveAt(0);

    if (BinStorage[itemName].Count == 0)
      BinStorage.Remove(itemName);

    InventoryManager.Instance.StoreItem(itemToRestore);

    UpdateBinUI();
  }

  public void UpdateBinUI()
  {
    // foreach slot
    for (int i = 0; i < BinSlots.Count; i++)
    {
      // show ieach item from bin in first N slots
      if (i < BinStorage.Count)
        BinSlots[i].AddItems(BinStorage.ElementAt(i).Value);

      // clear remaining bin slots
      else
      {
        BinSlots[i].Unselect();
        BinSlots[i].ClearSlot();
      }
    }
  }
}
