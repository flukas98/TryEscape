using UnityEngine;

public class UI_MenuPanel : UI_Dynamic
{
  #region Singleton
  public static UI_MenuPanel Instance { get; private set; }

  private void Awake()
  {
    // If there is an instance, and it's not me, delete myself.
    if (Instance != null && Instance != this)
      Destroy(this);
    else
      Instance = this;
  }
  #endregion

  private void Update()
  {
    if (Input.GetKeyUp(KeyCode.Escape))
      Enable();
  }
}
