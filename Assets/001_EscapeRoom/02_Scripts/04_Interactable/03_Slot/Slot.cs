using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Slot : Interactable
{
  public string SlotKey;
  public bool IsSticky;

  [Header("Initial readonly values")]
  public bool IsFull;

  private void Start()
  {
    IsFull = false;
    this.GetComponent<MeshRenderer>().material = SlotManager.Instance.DefaultSlotMaterial;
  }

  protected override void OnMouseUpAsButton()
  {
    if (CenterRaycastManager.Instance.IsPlayerInRange() && this.InventoryHasKey() == true)
    {
      var slotKeyItemToPlace = InventoryManager.Instance.Inventory[this.SlotKey].First();
      UI_ItemPanel.Instance.ShowPanel(slotKeyItemToPlace);
    }
  }

  public void PlaceItemOnSlot(Item slotKeyItem)
  {
    slotKeyItem.gameObject.transform.position = this.gameObject.transform.position;
    slotKeyItem.gameObject.transform.rotation = this.gameObject.transform.rotation;
    slotKeyItem.ItemState = ItemState.ON_SLOT;
    slotKeyItem.gameObject.SetActive(true);
    InventoryManager.Instance.RemoveItem(slotKeyItem.ItemName);
  }

  public void ActivateSlot()
  {
    this.gameObject.SetActive(true);
    this.IsFull = false;
  }

  public void DeactivateSlot()
  {
    this.gameObject.SetActive(false);
    this.IsFull = true;
  }

  public bool InventoryHasKey()
  {
    return InventoryManager.Instance.Contains(SlotKey);
  }
}
