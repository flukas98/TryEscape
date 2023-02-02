using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Settings : MonoBehaviour
{
  #region Singleton
  public static UI_Settings Instance { get; private set; }

  private void Awake()
  {
    // If there is an instance, and it's not me, delete myself.
    if (Instance != null && Instance != this)
      Destroy(this);
    else
      Instance = this;
  }
  #endregion

  public GameObject WindowCenter;

  private void Start()
  {
    WindowCenter = GameObject.FindGameObjectWithTag("WindowCenter");
    DisableSceneUI();
  }

  public void DisableSceneUI()
  {
    WindowCenter.SetActive(true);
    PlayerController.Instance.UpdatePlayerMovingFlag(true);
    Resources.FindObjectsOfTypeAll<UI_Dynamic>()
      .ToList()
      .ForEach(x => x.gameObject.SetActive(false));
  }

  public static bool ActiveUIExists()
  {
    return Resources.FindObjectsOfTypeAll<UI_Dynamic>()
      .ToList()
      .Any(x => x.gameObject.activeSelf);
  }
}

