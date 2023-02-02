using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Item : Interactable
{
  [Header("InspectPanel - Scale and Rotation")]
  public float InspectScale;
  public Quaternion InspectRotation;

  [Space(20)]
  public Sprite Placeholder;
  public ItemType ItemType;
  public string ItemName;

  [Header("Initial readonly values")]
  public ItemState ItemState = ItemState.NOT_PICKED;
  public bool IsBeingInspectable = false;
  public Slot Slot = null;

  protected override void OnMouseUpAsButton()
  {
    if (CenterRaycastManager.Instance.IsPlayerInRange())
    {
      if (ItemType == ItemType.SLOT_KEY && Slot != null && Slot.IsSticky)
        return;
      
      UI_ItemPanel.Instance.ShowPanel(this);
    }
  }

  public void ChangeItemState()
  {
    switch (ItemState)
    {
      case ItemState.NOT_PICKED:
        InventoryManager.Instance.StoreItem(this);
        break;

      case ItemState.IN_INVENTORY:
        if (ItemType == ItemType.SLOT_KEY && SlotManager.Instance.HighlightedSlotExists())
        {
          if (ItemName == SlotManager.Instance.highlightedSlot.SlotKey)
          {
            Slot = SlotManager.Instance.highlightedSlot;
            Slot.PlaceItemOnSlot(this);
            Slot.DeactivateSlot();
          }
        }
        break;

      case ItemState.ON_SLOT:
        InventoryManager.Instance.StoreItem(this);
        Slot.ActivateSlot();
        break;

      case ItemState.IN_BIN:
        InventoryManager.Instance.StoreItem(this);
        break;

      default:
        throw new Exception($"Item {this} cannot be in state {ItemState}.");
    }
  }

  private void OnMouseDrag()
  {
    if (!IsBeingInspectable)
      return;

    float x = Input.GetAxis("Mouse X") * UI_InspectPanel.Instance.RotationSpeed;
    float y = Input.GetAxis("Mouse Y") * UI_InspectPanel.Instance.RotationSpeed;

    transform.Rotate(Vector3.down, x, Space.World);
    transform.Rotate(Vector3.right, y, Space.World);
  }
}
