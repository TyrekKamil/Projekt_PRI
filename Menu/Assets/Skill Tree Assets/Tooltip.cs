using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tooltip : MonoBehaviour
{
    private static Tooltip instance;
    [SerializeField] private Camera uiCamera;
    private Text tooltipText;
    private RectTransform backgroundTransform;
    private void Awake()
    {
        instance = this;
        backgroundTransform = transform.Find("background").GetComponent<RectTransform>();
        tooltipText = transform.Find("text").GetComponent<Text>();

        //ShowTooltip("random text for the tooltip");
    }

    private void Update()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out localPoint);
        Vector2 threshold = new Vector2(80, 0);
        transform.localPosition = localPoint + threshold;
    }
    private void ShowTooltip(string tooltipString) {
        gameObject.SetActive(true);

        tooltipText.text = tooltipString;
        float textPaddingSize = 4f;
        Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + (textPaddingSize * 2f), tooltipText.preferredHeight + (textPaddingSize * 2f));
        backgroundTransform.sizeDelta = backgroundSize;
    }

    private void HideTooltip() {
        gameObject.SetActive(false);
    }
    public static void ShowTooltip_Static(string tooltipString) {
        instance.ShowTooltip(tooltipString);
    }
    public static void HideTooltip_Static() {
        instance.HideTooltip();
    }
}
