using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemPanel : UI_Dynamic
{
  #region Singleton
  public static UI_ItemPanel Instance { get; private set; }

  private void Awake()
  {
    // If there is an instance, and it's not me, delete myself.
    if (Instance != null && Instance != this)
      Destroy(this);
    else
      Instance = this;
  }
  #endregion

  [SerializeField] private Image Placeholder;
  [SerializeField] private Button BinButton;
  [SerializeField] private Button SubmitButton;
  [SerializeField] private TextMeshProUGUI SubmitButtonText;

  public Item SelectedItem;

  public void ShowPanel(Interactable selectedItem)
  {
    SelectedItem = selectedItem.GetComponent<Item>();
    Placeholder.sprite = SelectedItem.Placeholder;

    // option 1: PICKABLE ITEM
    if (SelectedItem != null)
    {
      BinButton.gameObject.SetActive(false);
      SubmitButton.gameObject.SetActive(true);

      if (SelectedItem.ItemState == ItemState.NOT_PICKED)
        SubmitButtonText.text = "STORE ITEM";

      else if (SelectedItem.ItemState == ItemState.IN_INVENTORY)
      {
        // option 1.1: SLOT KEY ITEM
        if (SelectedItem.ItemType == ItemType.SLOT_KEY && SlotManager.Instance.HighlightedSlotExists())
          SubmitButtonText.text = "PLACE ITEM ON SLOT";

        else
          SubmitButton.gameObject.SetActive(false);

        BinButton.gameObject.SetActive(true);
      }

      else if (SelectedItem.ItemState == ItemState.ON_SLOT)
        SubmitButtonText.text = "STORE ITEM";

      this.Enable();
    }
  }
  
  public void SubmitButtonClickAction()
  {
    SelectedItem.ChangeItemState();
    SelectedItem = null;
    UI_Settings.Instance.DisableSceneUI();
  }

  public void BinButtonClickAction()
  {
    Bin.Instance.StoreItemInBin(SelectedItem);
    UI_ItemPanel.Instance.BinButton.onClick.RemoveAllListeners();
    UI_Settings.Instance.DisableSceneUI();
  }
}