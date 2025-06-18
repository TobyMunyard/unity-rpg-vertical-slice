using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles player interaction with objects in the world, for example picking up items.
/// </summary>
public class PlayerInteraction : MonoBehaviour
{
    // Can be adjusted to change cone size
    public float rayDistance = 2f;
    public float coneAngle = 45f;
    public int rayCount = 9;

    [SerializeField]
    private PlayerInventoryController inventoryController;

    /// <summary>
    /// Checks each frame if the interaction key ("E") is pressed. If it is a cone raycast 
    /// is sent out and any items in the cone are picked up and added to the player's inventory, 
    /// provided there is space.
    /// </summary>
    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            float angleStep = (coneAngle * 2f) / (rayCount - 1);

            for (int i = 0; i < rayCount; i++)
            {
                float angle = -coneAngle + (i * angleStep); // from -45° to +45°
                Vector3 direction = Quaternion.Euler(0f, angle, 0f) * transform.forward;

                Ray ray = new Ray(transform.position, direction);

                if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
                {
                    Debug.Log($"Ray {i} hit: {hit.collider.name}");
                    if (hit.collider.CompareTag("WorldItem"))
                    {
                        WorldItem worldItem = hit.collider.GetComponentInParent<WorldItem>();
                        if (worldItem != null)
                        {
                            inventoryController.AddItem(worldItem.item, worldItem.quantity);
                            Destroy(worldItem.gameObject);
                        }
                    }
                }

                Debug.DrawRay(transform.position, direction * rayDistance, Color.cyan, 1f);
            }
        }
    }
}
