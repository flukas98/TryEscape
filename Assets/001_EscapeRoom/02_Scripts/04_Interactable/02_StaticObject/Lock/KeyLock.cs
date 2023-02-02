using UnityEngine;

public class KeyLock : OpenableObject
{
  [Header("Key Lock")]
  public bool IsUnlocked = false;
  [SerializeField] private string KeyItemToUnlock;

  protected override void OnMouseUpAsButton()
  {
    if (CenterRaycastManager.Instance.IsPlayerInRange() && !IsUnlocked)
      UI_KeyLockPanel.Instance.ShowPanel(this);
    else
      base.OnMouseUpAsButton();
  }

  public bool OpenAttempt(string possibleKey)
  {
    if (possibleKey == KeyItemToUnlock)
    {
      IsUnlocked = true;
      InventoryManager.Instance.RemoveItem(possibleKey);
      UI_Settings.Instance.DisableSceneUI();
      UI_KeyLockPanel.Instance.ResetSelection();
      OpenObjectAnimator.SetBool("Open", true);
      IsOpen = true;
      return true;
    }
    else
    {
      return false;
    }
  }
}
