using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  public CharacterController controller;
  public int speed;
  public float gravity = -9.81f;
  
  [SerializeField] private float groundDistance = 0.4f;
  [SerializeField] private Transform groundCheck;
  [SerializeField] private LayerMask groundMask;

  private Vector3 velocity;
  private bool isGrounded;

  public void MovePlayer()
  {
    isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

    if (isGrounded && velocity.y < 0)
    {
      velocity.y = -2f;
    }

    float x = Input.GetAxis("Horizontal");
    float z = Input.GetAxis("Vertical");

    var move = transform.right * x + transform.forward * z;
    controller.Move(speed * Time.deltaTime * move);
    
    velocity.y += gravity * Time.deltaTime;
    controller.Move(velocity * Time.deltaTime);
  }
}
