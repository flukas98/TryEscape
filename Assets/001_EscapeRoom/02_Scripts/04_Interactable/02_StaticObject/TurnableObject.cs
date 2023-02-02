using System.Collections;
using UnityEngine;

public class TurnableObject : Interactable
{
  [SerializeField] private TurnableDirection Direction;
  [SerializeField] private float Degrees;

  protected override void OnMouseUpAsButton()
  {
    if (CenterRaycastManager.Instance.IsPlayerInRange())
    {
      var direction = new Vector3();

      switch (Direction)
      {
        case TurnableDirection.Clockwise:
          direction = Vector3.up;
          break;
        case TurnableDirection.Anticlockwise:
          direction = Vector3.down;
          break;
      }

      StartCoroutine(Rotate(direction, Degrees, 1.0f));
    }
  }

  private IEnumerator Rotate(Vector3 axis, float angle, float duration = 1.0f)
  {
    Quaternion from = transform.rotation;
    Quaternion to = transform.rotation;
    to *= Quaternion.Euler(axis * angle);

    float elapsed = 0.0f;
    while (elapsed < duration)
    {
      transform.rotation = Quaternion.Slerp(from, to, elapsed / duration);
      elapsed += Time.deltaTime;
      yield return null;
    }
    transform.rotation = to;
  }
}

public enum TurnableDirection
{
  Clockwise, Anticlockwise
}
