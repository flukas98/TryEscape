using System.Linq;
using UnityEngine.UI;

public class UI_SelectableSlot : UI_Slot
{
  public bool IsSelected;

  private Image _selectionBorder;

  protected override void Awake()
  {
    _selectionBorder = gameObject
      .GetComponentsInChildren<Image>(includeInactive: true)
      .Where(x => x.name == "Border")
      .Single();
    Unselect();
    base.Awake();
  }

  public void Select()
  {
    IsSelected = true;
    _selectionBorder.gameObject.SetActive(true);
  }

  public void Unselect()
  {
    IsSelected = false;
    _selectionBorder.gameObject.SetActive(false);
  }
}