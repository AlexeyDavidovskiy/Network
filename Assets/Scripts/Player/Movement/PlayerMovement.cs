using Fusion;
using UnityEngine;


public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Camera camera;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityValue;

    private bool jumpPressed;
    private Vector3 velocity;
    private Vector3 moveDirection;
    public float Velocity => velocity.y;
    public Camera Camera => camera;
    public Vector3 MoveDirection => moveDirection;

    public override void Spawned()
    {
        if (HasStateAuthority)
        {
            camera = Camera.main;
            camera.GetComponent<FirstPersonCamera>().Target = transform;
        }
    }

    public override void FixedUpdateNetwork()
    {
        MoveCharacter(playerInput.Horizontal, playerInput.Vertical);

        if (playerInput.JumpPressed)
        {
            Jump();
            playerInput.ConsumeJump();
        }
    }

    private void MoveCharacter(float horizontal, float vertical)
    {
        if (characterController.isGrounded)
        {
            velocity = new Vector3(0, -1, 0);
        }

        Quaternion cameraRotationY = Quaternion.Euler(0, camera.transform.eulerAngles.y, 0);

        Vector3 inputDirection = new Vector3(horizontal, 0, vertical);
        moveDirection = cameraRotationY * inputDirection * Runner.DeltaTime * speed;

        velocity.y += gravityValue * Runner.DeltaTime;

        characterController.Move(moveDirection + velocity * Runner.DeltaTime);

        transform.rotation = Quaternion.Euler(0, camera.transform.eulerAngles.y, 0);
    }

    private void Jump()
    {
        if (characterController.isGrounded)
        {
            velocity.y = jumpForce;
        }
    }
}