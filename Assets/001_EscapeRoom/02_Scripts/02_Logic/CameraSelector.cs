using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CameraSelector : MonoBehaviour
{
  #region Singleton
  public static CameraSelector Instance { get; private set; }

  private void Awake()
  {
    // If there is an instance, and it's not me, delete myself.
    if (Instance != null && Instance != this)
      Destroy(this);
    else
      Instance = this;
  }
  #endregion

  [Header ("Main Canvas")]
  [SerializeField] private Canvas MainCanvas;
  [SerializeField] private Camera MainCamera;

  [Header("Inspection Canvas")]
  [SerializeField] private Canvas InspectionCanvas;
  [SerializeField] private Camera InspectionCamera;

  public void SelectCamera(CameraType cameraType)
  {
    switch (cameraType)
    {
      case CameraType.MAIN:
        InspectionCanvas.gameObject.SetActive(false);
        InspectionCamera.gameObject.SetActive(false);
        MainCanvas.gameObject.SetActive(true);
        MainCamera.gameObject.SetActive(true);
        break;
      case CameraType.INSPECTION:
        MainCanvas.gameObject.SetActive(false);
        MainCamera.gameObject.SetActive(false);
        InspectionCanvas.gameObject.SetActive(true);
        InspectionCamera.gameObject.SetActive(true);
        break;
    }
  }
}
