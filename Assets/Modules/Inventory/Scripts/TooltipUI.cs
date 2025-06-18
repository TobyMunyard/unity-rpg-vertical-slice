using System.Collections;
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

    private CanvasGroup canvasGroup;
    private Coroutine fadeCoroutine;
    private bool isVisible;

    private void Awake()
    {
        Instance = this;
        canvasGroup = tooltipPanel.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        tooltipPanel.SetActive(false);
        isVisible = false;
    }

    private void Update()
    {
        if (isVisible)
        {
            Vector3 offset = new Vector3(250, -200, 0);
            tooltipPanel.transform.position = Input.mousePosition + offset;
        }
    }
    public void ShowTooltip(InventoryItem item)
    {
        itemNameText.text = item.displayName;
        descriptionText.text = item.description;

        tooltipPanel.SetActive(true);
        isVisible = true;

        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        canvasGroup.alpha = 0;
        fadeCoroutine = StartCoroutine(FadeInCoroutine());
    }

    public void HideTooltip()
    {
        isVisible = false;

        if (!gameObject.activeInHierarchy || !tooltipPanel.activeInHierarchy)
        {
            // Don't run Coroutine if the instance is inactive
            canvasGroup.alpha = 0f;
            tooltipPanel.SetActive(false);
            return;
        }

        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeOutCoroutine());
    }


    private IEnumerator FadeInCoroutine()
    {
        float duration = 0.15f;
        float time = 0f;

        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1f;
    }

    private IEnumerator FadeOutCoroutine()
    {
        float duration = 0.15f;
        float time = 0f;

        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0f;
        tooltipPanel.SetActive(false);
    }


}
