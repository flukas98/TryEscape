using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
  #region Singleton
  public static PlayerController Instance { get; private set; }

  private void Awake()
  {
    // If there is an instance, and it's not me, delete myself.
    if (Instance != null && Instance != this)
      Destroy(this);
    else
      Instance = this;
  }
  #endregion

  public MouseLook MouseLook;
  public PlayerMovement PlayerMovement;

  public bool CanMove { get; private set; }

  public void Start()
  {
    CanMove = true;
  }

  public void Update()
  {
    if (CanMove)
    {
      Cursor.lockState = CursorLockMode.Locked;
      MouseLook.RotatePlayer();
      PlayerMovement.MovePlayer();

      if (Input.GetKeyDown(KeyCode.LeftControl))
        MouseLook.Crouch();

      if (Input.GetKeyUp(KeyCode.Escape))
        UI_MenuPanel.Instance.Enable();
    }
  }

  public void UpdatePlayerMovingFlag(bool canMove)
  {
    this.CanMove = canMove;
  }
}