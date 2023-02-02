using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_InspectPanel : UI_Dynamic
{
  #region Singleton
  public static UI_InspectPanel Instance { get; private set; }

  private void Awake()
  {
    // If there is an instance, and it's not me, delete myself.
    if (Instance != null && Instance != this)
      Destroy(this);
    else
      Instance = this;
  }
  #endregion

  public Item ItemToInspect;
  public float ScaleSize;
  public float ZoomFactor;
  public float RotationSpeed;

  public override void Enable()
  {
    CenterRaycastManager.Instance.RaycastCheck = false;
    CameraSelector.Instance.SelectCamera(CameraType.INSPECTION);
    UI_Settings.Instance.DisableSceneUI();
    base.Enable();
    SetItemToInspect();
  }

  public void CloseInspectPanel()
  {
    CameraSelector.Instance.SelectCamera(CameraType.MAIN);
    CenterRaycastManager.Instance.RaycastCheck = true;
    Object.Destroy(ItemToInspect.gameObject);
    UI_Settings.Instance.DisableSceneUI();
  }

  private void SetItemToInspect()
  {
    ItemToInspect = Instantiate(UI_ItemPanel.Instance.SelectedItem);
    ItemToInspect.gameObject.SetActive(true);
    ItemToInspect.IsBeingInspectable = true;
    ItemToInspect.transform.parent = transform;
    ItemToInspect.transform.localPosition = new Vector3(0, 0, -150);
    ItemToInspect.transform.localRotation = ItemToInspect.InspectRotation;
    ItemToInspect.transform.localScale = ItemToInspect.InspectScale * ItemToInspect.transform.lossyScale;
  }
}