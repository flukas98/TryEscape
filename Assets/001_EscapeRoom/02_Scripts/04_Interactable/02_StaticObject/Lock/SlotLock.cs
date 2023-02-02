using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlotLock : OpenableObject
{
  public bool IsUnlocked = false;
  public List<Slot> Slots;
  public GameObject ChestToOpen;

  private void Update()
  {
    if (!IsUnlocked && Slots.All(x => x.IsFull))
    {
      IsUnlocked = true;
      ChestToOpen.SetActive(false);
    }
  }
}
