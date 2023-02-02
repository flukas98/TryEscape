using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class UI_Dynamic : MonoBehaviour
{
  public virtual void Enable()
  {
    UI_Settings.Instance.WindowCenter.SetActive(false);
    Cursor.lockState = CursorLockMode.None;
    PlayerController.Instance.UpdatePlayerMovingFlag(false);

    this.gameObject.SetActive(true);
  }
}
