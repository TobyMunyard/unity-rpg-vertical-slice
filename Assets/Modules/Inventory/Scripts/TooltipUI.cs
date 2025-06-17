using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class TooltipUI : MonoBehaviour
{
    public static TooltipUI Instance { get; private set; }

    [SerializeField]
    private GameObject tooltipPanel;
    [SerializeField]
    private TMPro.TextMeshProUGUI itemNameText;
    [SerializeField] 
    private TMPro.TextMeshProUGUI descriptionText;

    private void Awake()
    {
        Instance = this;
        HideTooltip();
    }

    public void ShowTooltip(InventoryItem item, Vector3 position)
    {
        itemNameText.text = item.displayName;
        descriptionText.text = item.description;
        tooltipPanel.SetActive(true);
        tooltipPanel.transform.position = position;
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

}
