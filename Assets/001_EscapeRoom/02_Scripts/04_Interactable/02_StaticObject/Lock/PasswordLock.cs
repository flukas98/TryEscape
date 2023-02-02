using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PasswordLock : OpenableObject
{
  [Header("Password Lock")]
  public bool IsUnlocked = false;
  public List<int> Password;

  private List<int> _attempt;

  protected override void OnMouseUpAsButton()
  {
    if (CenterRaycastManager.Instance.IsPlayerInRange() && !IsUnlocked)
      UI_PasswordLockPanel.Instance.ShowPanel(this);
    else
      base.OnMouseUpAsButton();
  }

  public bool OpenAttempt()
  {
    _attempt = UI_PasswordLockPanel.Instance.InputFields
      .Where(x => x.interactable)
      .Select(x => int.Parse(x.text))
      .ToList();

    if (Password.Count == _attempt.Count)
    {
      var count = Password.Count;

      for (var i = 0; i < count; i++)
      {
        if (Password[i] != _attempt[i])
        {
          Debug.Log("Incorrect pass!");
          return false;
        }
      }

      Debug.Log("Correct pass!");
      IsUnlocked = true;
      UI_Settings.Instance.DisableSceneUI();
      OpenObjectAnimator.SetBool("Open", true);
      IsOpen = true;
      return true;
    }

    Debug.Log("Inorrect pass!");
    return false;
  }
}
