using UnityEngine;

public class OpenableObject : Interactable
{
  public Animator OpenObjectAnimator;

  [Header("Initial readonly values")]
  public bool IsOpen = false;
  
  protected override void OnMouseUpAsButton()
  {
    if (CenterRaycastManager.Instance.IsPlayerInRange())
    {
      OpenObjectAnimator.SetBool("Open", !IsOpen);
      IsOpen = !IsOpen;
    }
  }
}
