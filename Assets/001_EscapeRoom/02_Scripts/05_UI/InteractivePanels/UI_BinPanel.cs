using System.Linq;

public class UI_BinPanel : UI_Dynamic
{
  #region Singleton
  public static UI_BinPanel Instance { get; private set; }

  private void Awake()
  {
    // If there is an instance, and it's not me, delete myself.
    if (Instance != null && Instance != this)
      Destroy(this);
    else
      Instance = this;
  }
  #endregion

  public void RestoreItem()
  {
    var selectedBinSlot = Bin.Instance.BinSlots
      .Where(x => x.IsSelected)
      .SingleOrDefault();

    if (selectedBinSlot == null)
      return;

    Bin.Instance.RestoreItemInInventory(selectedBinSlot.ItemOnSlot);

    if (!Bin.Instance.BinStorage.Any())
    {
      UI_Settings.Instance.DisableSceneUI();
    }
  }

  public void SelectItem(UI_SelectableSlot binSlotToSelect)
  {
    if (!string.IsNullOrEmpty(binSlotToSelect.ItemOnSlot))
    {
      foreach (var binSlot in Bin.Instance.BinSlots)
      {
        if (binSlot == binSlotToSelect)
          binSlot.Select();
        else
          binSlot.Unselect();
      }
    }
  }

  public void ResetSelection()
  {
    foreach (var binSlot in Bin.Instance.BinSlots)
      binSlot.Unselect();
  }
}
