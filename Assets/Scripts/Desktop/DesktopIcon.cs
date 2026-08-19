using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public sealed class DesktopIcon : MonoBehaviour, IPointerClickHandler
{
    private const float DoubleClickThreshold = 0.4f;

    [SerializeField] private string windowTitle = "New Window";
    [SerializeField] private DesktopLauncher launcher;
    [SerializeField] private TMP_Text iconNameText;
    [SerializeField] private GameObject selectionHighlight;

    private float lastClickTime = float.NegativeInfinity;

    private void Awake()
    {
        if (iconNameText != null)
            iconNameText.text = windowTitle;

        SetSelected(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SetSelected(true);

        if (Time.unscaledTime - lastClickTime <= DoubleClickThreshold)
        {
            launcher?.OpenWindow(windowTitle);
            lastClickTime = float.NegativeInfinity;
            return;
        }

        lastClickTime = Time.unscaledTime;
    }

    private void SetSelected(bool isSelected)
    {
        if (selectionHighlight != null)
            selectionHighlight.SetActive(isSelected);
    }
}
