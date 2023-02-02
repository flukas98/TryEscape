using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GameEndManager : MonoBehaviour
{
  #region Singleton
  public static GameEndManager Instance { get; private set; }

  private void Awake()
  {
    // If there is an instance, and it's not me, delete myself.
    if (Instance != null && Instance != this)
      Destroy(this);
    else
      Instance = this;
  }
  #endregion

  public Stopwatch GameTimer;
  [SerializeField] private List<ButtonObject> PlanetButtons;

  private bool _isFinished = false;

  private void Start()
  {
    GameTimer = new Stopwatch();
    GameTimer.Start();
  }

  private void Update()
  {
    var allButtonsClicked = PlanetButtons.TrueForAll(x => x.IsClicked);
    
    if (!_isFinished && allButtonsClicked)
    {
      _isFinished = true;
      GameTimer.Stop();
      UI_GameEndMenu.Instance.Enable();
    }
  }
}
