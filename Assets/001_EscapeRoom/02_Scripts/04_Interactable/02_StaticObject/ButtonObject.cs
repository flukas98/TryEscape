using System.Collections;
using UnityEngine;

public class ButtonObject : Interactable
{
  [SerializeField] private Animator ButtonClickAnimator;
  [SerializeField] private Animator EventEffectAnimator;
  [SerializeField] private bool MultipleClick = false;

  [Header("Initial readonly values")]
  public bool IsClicked = false;

  protected override void OnMouseUpAsButton()
  {
    if (CenterRaycastManager.Instance.IsPlayerInRange() && (!IsClicked || MultipleClick))
    {
      IsClicked = !IsClicked;
      
      if (ButtonClickAnimator != null)
        ButtonClickAnimator.SetBool("Clicked", IsClicked);
      if (EventEffectAnimator != null)
        EventEffectAnimator.SetBool("StartAnimation", IsClicked);
    }
  }
}
