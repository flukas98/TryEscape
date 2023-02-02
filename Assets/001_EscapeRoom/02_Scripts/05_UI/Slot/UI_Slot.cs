using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Slot : MonoBehaviour
{
  public Sprite SlotPlaceholder;
  public string ItemOnSlot;
  public bool IsStackable;

  protected virtual void Awake()
  {
    ClearSlot();
  }
  
  public void AddItems(List<Item> items)
  {
    ItemOnSlot = items.First().ItemName;
    SetSlotImage(items.First().Placeholder);
    SetUIItemCount(IsStackable && !string.IsNullOrEmpty(ItemOnSlot), items.Count);
  }

  public void ClearSlot()
  {
    ItemOnSlot = null;
    SetSlotImage(SlotPlaceholder);
    SetUIItemCount(IsStackable && !string.IsNullOrEmpty(ItemOnSlot), 0);
  }

  private void SetUIItemCount(bool visible, int itemCount)
  {
    if (IsStackable)
    {
      var UI_ItemCount = this
        .GetComponentsInChildren<TextMeshProUGUI>(includeInactive: true)
        .Where(x => x.name == "Stack Number")
        .Single();

      UI_ItemCount.text = itemCount.ToString();
      UI_ItemCount.gameObject.SetActive(visible);
    }
  }

  private void SetSlotImage(Sprite image)
  {
    this.GetComponent<Image>().sprite = image;
  }
}