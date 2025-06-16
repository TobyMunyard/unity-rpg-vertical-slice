using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{

    public float rayDistance = 2f;
    public float coneAngle = 45f;
    public int rayCount = 9;

    [SerializeField]
    private PlayerInventoryController inventoryController;

    // Update is called once per frame
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
                    if(hit.collider.CompareTag("WorldItem"))
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
