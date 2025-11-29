using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    public float sensitivity = 300f;

    ActionMap actions;
    Vector2 lookInput;
    float xRotation = 0f;

    private void Awake()
    {
        actions = new ActionMap();
        actions.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        actions.Player.Look.canceled += ctx => lookInput = Vector2.zero;
    }

    private void OnEnable() => actions.Player.Enable();
    private void OnDisable() => actions.Player.Disable();

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = lookInput.x * sensitivity * Time.deltaTime;
        float mouseY = lookInput.y * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.parent.Rotate(Vector3.up * mouseX);
    }

}
