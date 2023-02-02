using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_PasswordLockPanel : UI_Dynamic
{
  #region Singleton
  public static UI_PasswordLockPanel Instance { get; private set; }

  private void Awake()
  {
    // If there is an instance, and it's not me, delete myself.
    if (Instance != null && Instance != this)
      Destroy(this);
    else
      Instance = this;
  }
  #endregion

  public List<TMP_InputField> InputFields;
  [SerializeField] private GameObject WrongPasswordText;

  public PasswordLock SelectedPasswordLock;

  public void ShowPanel(Interactable selectedLock)
  {
    SelectedPasswordLock = selectedLock.GetComponent<PasswordLock>();
    WrongPasswordText.SetActive(false);
    this.Enable();

    for (var i = 0; i < InputFields.Count; i++)
    {
      var inputField = InputFields[i];

      if (i < SelectedPasswordLock.Password.Count)
      {
        inputField.interactable = true;
        inputField.text = "0";
      }
      else
      {
        inputField.interactable = false;
        inputField.text = string.Empty;
      }
    }
  }

  public void UnlockButtonClickAction()
  {
    if (!SelectedPasswordLock.IsUnlocked)
    {
      var isCorrectPassword = SelectedPasswordLock.OpenAttempt();
      WrongPasswordText.SetActive(!isCorrectPassword);
    }
  }
}