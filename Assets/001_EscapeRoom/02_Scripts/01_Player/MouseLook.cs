using UnityEngine;

public class MouseLook : MonoBehaviour
{
  [Header("Mouse Rotation")]
  public int MouseSensitivity;
  public Transform PlayerBody;


  [Header("Crouching")]
  public Animator CrouchAnimator;
  public bool IsCrouching = false;

  private float xRotation = 0f;

  public void RotatePlayer()
  {
    float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
    float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

    xRotation -= mouseY;
    xRotation = Mathf.Clamp(xRotation, -90f, 90f);

    transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    PlayerBody.Rotate(Vector3.up * mouseX);
  }

  public void Crouch()
  {
    CrouchAnimator.SetBool("Crouch", IsCrouching);
    IsCrouching = !IsCrouching;
  }
}
