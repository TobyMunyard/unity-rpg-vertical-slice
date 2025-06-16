using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles third-person character movement using Unity's Input System and CharacterController.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("Walking speed of the player.")]
    public float walkSpeed = 5f;

    [Tooltip("Sprinting speed of the player.")]
    public float sprintSpeed = 8f;

    [Tooltip("How quickly the player rotates toward movement direction.")]
    public float rotationSpeed = 10f;

    [Header("References")]
    [Tooltip("Reference to the main camera transform (used for movement direction).")]
    public Transform cameraTransform;

    private CharacterController controller;
    private PlayerInput playerInput;
    private Vector2 inputVector;
    private float currentSpeed;

    /// <summary>
    /// Called when the script is being loaded.
    /// Caches CharacterController and PlayerInput components for use in the script.
    /// </summary>
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
    }

    /// <summary>
    /// Updates the player's movement each frame based on player input.
    /// </summary>
    private void Update()
    {
        // Reads input from player, gets movement direction and whether the player is sprinting
        inputVector = playerInput.actions["Move"].ReadValue<Vector2>();
        bool sprinting = playerInput.actions["Sprint"].IsPressed();

        // Sets speed based on whether the player is sprinting or not
        // TODO: Add a check later to ensure the player has stamina?
        currentSpeed = sprinting ? sprintSpeed : walkSpeed;

        // Calculates direction of movement to correctly rotate character
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        if (moveDirection.magnitude > 0.1f)
        {
            // Rotate to face move direction based on camera position
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float smoothAngle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, Time.deltaTime * rotationSpeed);
            transform.rotation = Quaternion.Euler(0, smoothAngle, 0);

            // Move character in correct direction
            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            controller.Move(moveDir.normalized * currentSpeed * Time.deltaTime);
        }
    }
}
