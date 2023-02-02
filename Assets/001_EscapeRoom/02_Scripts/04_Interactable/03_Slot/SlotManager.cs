using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
  #region Singleton
  public static SlotManager Instance { get; private set; }

  private void Awake()
  {
    // If there is an instance, and it's not me, delete myself.
    if (Instance != null && Instance != this)
      Destroy(this);
    else
      Instance = this;
  }
  #endregion

  public Slot highlightedSlot;

  [Header("Slot Materials")]
  public Material DefaultSlotMaterial;
  public Material HoverSlotMaterial;
  public Material CorrectSlotMaterial;
  public Material WrongSlotMaterial;

  public void HighlightSlot(Slot hittedSlot)
  {
    highlightedSlot = hittedSlot;

    if (CenterRaycastManager.Instance.IsPlayerInRange())
    {
      if (highlightedSlot.InventoryHasKey())
      {
        hittedSlot.GetComponent<MeshRenderer>().material = CorrectSlotMaterial;
      }
      else
      {
        hittedSlot.GetComponent<MeshRenderer>().material = HoverSlotMaterial;
      }
    }
  }

  public void ResetSelection()
  {
    highlightedSlot.GetComponent<MeshRenderer>().material = DefaultSlotMaterial;
    highlightedSlot = null;
  }

  public bool HighlightedSlotExists()
  {
    return this.highlightedSlot != null;
  }
}