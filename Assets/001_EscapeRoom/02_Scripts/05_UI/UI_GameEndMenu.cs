using System.Diagnostics;
using TMPro;
using UnityEngine;

public class UI_GameEndMenu : UI_Dynamic
{
  #region Singleton
  public static UI_GameEndMenu Instance { get; private set; }

  private void Awake()
  {
    // If there is an instance, and it's not me, delete myself.
    if (Instance != null && Instance != this)
      Destroy(this);
    else
      Instance = this;
  }
  #endregion

  public TextMeshProUGUI GameEndText;

  public override void Enable()
  {
    var m = (int)GameEndManager.Instance.GameTimer.Elapsed.TotalMinutes;
    var s = GameEndManager.Instance.GameTimer.Elapsed.Seconds;

    GameEndText.text = $"Congratulations!\nYou finished the game in {m} minutes and {s} seconds.";
    base.Enable();
  }
}
