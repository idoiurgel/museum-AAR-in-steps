using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementController : MonoBehaviour
{
    public float moveSpeed = 5f;           // Movement speed
    public float mouseSensitivity = 100f;  // Mouse sensitivity for looking around
    public Transform playerCamera;         // Reference to the player's camera
    private float xRotation = 0f;          // Variable to store the vertical camera rotation

    private CharacterController controller;

    void Start()
    {
        // Get the CharacterController component attached to this GameObject
        controller = GetComponent<CharacterController>();

        // Lock the cursor to the center of the screen and hide it
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Get the mouse input for looking around
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate the player horizontally based on the mouse X input (turning left/right)
        transform.Rotate(Vector3.up * mouseX);

        // Rotate the camera vertically based on the mouse Y input (looking up/down)
        xRotation -= mouseY;                            // Invert the vertical axis for natural FPS control
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // Prevent over-rotation to avoid flipping the camera
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Get player input for movement (W/S for forward/backward, A/D for left/right)
        float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow Keys
        float moveZ = Input.GetAxis("Vertical");   // W/S or Up/Down Arrow Keys

        // Move the player in the direction relative to the player's forward direction
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * moveSpeed * Time.deltaTime);
    }
}
