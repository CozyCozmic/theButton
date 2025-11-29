using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float gravity = -9.8f;

    CharacterController controller;
    ActionMap actions;

    Vector2 moveInput;
    Vector3 velocity;

    private void Awake()
    {
        actions = new ActionMap();
        actions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        actions.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    }

    private void OnEnable() => actions.Player.Enable();
    private void OnDisable() => actions.Player.Disable();

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0) 
        {
            velocity.y = -2f;
        }
    }
}
