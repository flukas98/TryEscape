using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_KeyLockPanel : UI_Dynamic
{
  #region Singleton
  public static UI_KeyLockPanel Instance { get; private set; }

  private void Awake()
  {
    // If there is an instance, and it's not me, delete myself.
    if (Instance != null && Instance != this)
      Destroy(this);
    else
      Instance = this;
  }
  #endregion

  [SerializeField] private List<UI_SelectableSlot> KeySlots;
  [SerializeField] private GameObject WrongKeyText;

  public KeyLock SelectedKeyLock;

  public void ShowPanel(Interactable selectedLock)
  {
    SelectedKeyLock = selectedLock.GetComponent<KeyLock>();
    WrongKeyText.SetActive(false);
    this.Enable();
  }

  public void UnlockButtonClickAction()
  {
    var selectedKeySlot = KeySlots
      .Where(x => x.IsSelected)
      .SingleOrDefault();

    if (selectedKeySlot == null)
      return;

    if (!SelectedKeyLock.IsUnlocked)
    {
      var isCorrectKey = SelectedKeyLock.OpenAttempt(selectedKeySlot.ItemOnSlot);
      WrongKeyText.SetActive(!isCorrectKey);
    }
  }

  public void AddKeyItemToPanel(Item keyItem)
  {
    var currentKeyItems = KeySlots
      .Select(x => x.ItemOnSlot)
      .ToList();

    if (!currentKeyItems.Contains(keyItem.ItemName))
    {
      foreach (var pks in KeySlots)
      {
        if (pks.ItemOnSlot == null)
        {
          pks.ItemOnSlot = keyItem.ItemName;
          pks.GetComponent<Image>().sprite = keyItem.Placeholder;
          return;
        }
      }
    }
  }

  public void RemoveKeyItemFromKeyPanel(string itemType)
  {
    foreach (var pks in KeySlots)
    {
      if (pks.ItemOnSlot == itemType)
      {
        pks.ItemOnSlot = null;
        pks.GetComponent<Image>().sprite = pks.SlotPlaceholder;
        return;
      }
    }
  }

  public void SelectItem(UI_SelectableSlot possibleKeySlot)
  {
    if (!string.IsNullOrEmpty(possibleKeySlot.ItemOnSlot))
    {
      foreach (var pks in KeySlots)
      {
        if (pks == possibleKeySlot)
          pks.Select();
        else
          pks.Unselect();
      }
    }
  }

  public void ResetSelection()
  {
    foreach (var pks in KeySlots)
      pks.Unselect();
  }
}