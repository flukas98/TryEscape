using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class CenterRaycastManager : MonoBehaviour
{
  #region Singleton
  public static CenterRaycastManager Instance { get; private set; }

  private void Awake()
  {
    // If there is an instance, and it's not me, delete myself.
    if (Instance != null && Instance != this)
      Destroy(this);
    else
      Instance = this;
  }
  #endregion

  [Header("Window Center Images")]
  [SerializeField] private Sprite Sprite_Default;
  [SerializeField] private Sprite Sprite_Pointer;
  [SerializeField] private Sprite Sprite_Lock;
  [SerializeField] private Sprite Sprite_Bin;
  [SerializeField] private Sprite Sprite_Turnable;

  [Space(20)]
  public float DistanceFromCamera = 2.5f;
  public bool RaycastCheck = true;
  public Vector3 ItemPlaceOffset;
  private GameObject _windowCenter;
  private float _currentDistance;
  private readonly int _itemPlaceDistance = 2;

  private void Start()
  {
    _windowCenter = GameObject.FindGameObjectWithTag("WindowCenter");
  }

  void Update()
  {
    if (!RaycastCheck)
      return;

    var ray = Camera.main.ScreenPointToRay(_windowCenter.transform.position);
    if (Physics.Raycast(ray, out RaycastHit hit))
    {
      var hittedInteractable = InteractableHitCheck(hit);
      var hittedSlot = SlotHitCheck(hit);
      SetWindowCenterIcon(hittedInteractable);

      var endPoint = hit.point;
      var placeVector = new Vector3(
        endPoint.x - PlayerController.Instance.transform.position.x,
        endPoint.y - PlayerController.Instance.transform.position.y,
        endPoint.z - PlayerController.Instance.transform.position.z);
      ItemPlaceOffset = placeVector.normalized * _itemPlaceDistance;
    }
  }

  public bool IsPlayerInRange()
  {
    return _currentDistance < DistanceFromCamera;
  }

  private void SetWindowCenterIcon(Interactable hittedInteractable)
  {
    if (hittedInteractable != null)
    {
      _currentDistance = Vector3.Distance(Camera.main.gameObject.transform.position, hittedInteractable.transform.position);
      if (IsPlayerInRange())
      {
        if ((hittedInteractable is KeyLock @keyLock && !@keyLock.IsUnlocked) ||
          (hittedInteractable is PasswordLock @passwordLock && !@passwordLock.IsUnlocked))
          SetWindowCenterIcon(Sprite_Lock);
        else if (hittedInteractable is Bin)
          SetWindowCenterIcon(Sprite_Bin);
        else if (hittedInteractable is TurnableObject)
          SetWindowCenterIcon(Sprite_Turnable);
        else
        {
          if (hittedInteractable is Item @item && @item.ItemType == ItemType.SLOT_KEY && @item.Slot != null && @item.Slot.IsSticky)
          {
            ResetWindowCenterIcon();
            return;
          }
          SetWindowCenterIcon(Sprite_Pointer);
        }
      }
      else
      {
        ResetWindowCenterIcon();
      }
    }
    else
    {
      ResetWindowCenterIcon();
    }
  }

  private void SetWindowCenterIcon(Sprite sprite)
  {
    _windowCenter.GetComponent<Image>().sprite = sprite;
    _windowCenter.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 30);
  }

  private void ResetWindowCenterIcon()
  {
    _windowCenter.GetComponent<Image>().sprite = Sprite_Default;
    _windowCenter.GetComponent<RectTransform>().sizeDelta = new Vector2(10, 10);
  }

  private Interactable InteractableHitCheck(RaycastHit hit)
  {
    var hittedInteractable = hit.transform.gameObject.GetComponent<Interactable>();
    return hittedInteractable;
  }

  private Slot SlotHitCheck(RaycastHit hit)
  {
    var hittedSlot = hit.transform.gameObject.GetComponent<Slot>();
    var hghlightedSlotExists = SlotManager.Instance.HighlightedSlotExists();
    var playerInRange = CenterRaycastManager.Instance.IsPlayerInRange();

    if (hittedSlot != null && hittedSlot.IsFull)
    return null;

    // previous frame: EXISTED
    // current frame: DOES NOT EXIST
    if (hghlightedSlotExists && (hittedSlot == null || !playerInRange))
    {
      SlotManager.Instance.ResetSelection();
    }
    // previous frame: EXISTED
    // current frame: STILL EXISTS
    else if (playerInRange && hghlightedSlotExists)
    {
      if (SlotManager.Instance.highlightedSlot != hittedSlot)
      {
        SlotManager.Instance.ResetSelection();
        SlotManager.Instance.HighlightSlot(hittedSlot);
      }
    }
    // previous frame: DID NOT EXIST
    // current frame: EXISTS
    else if (playerInRange && !hghlightedSlotExists && hittedSlot)
    {
      SlotManager.Instance.HighlightSlot(hittedSlot);
    }

    return hittedSlot;
  }
}
