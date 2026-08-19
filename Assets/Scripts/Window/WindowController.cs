using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public sealed class WindowController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    [SerializeField] private RectTransform windowRect;
    [SerializeField] private RectTransform dragArea;
    [SerializeField] private Button closeButton;
    [SerializeField] private TMP_Text titleText;

    private Canvas rootCanvas;
    private Vector2 dragOffset;

    private void Awake()
    {
        windowRect ??= transform as RectTransform;
        rootCanvas = GetComponentInParent<Canvas>();
        closeButton?.onClick.AddListener(Close);
    }

    private void OnEnable()
    {
        WindowManager.Instance?.Register(this);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        WindowManager.Instance?.BringToFront(this);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        WindowManager.Instance?.BringToFront(this);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            windowRect, eventData.position, eventData.pressEventCamera, out dragOffset);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (rootCanvas == null || windowRect == null)
            return;

        var parent = windowRect.parent as RectTransform;
        if (parent == null)
            return;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parent, eventData.position, eventData.pressEventCamera, out var pointerPosition))
            windowRect.localPosition = pointerPosition - dragOffset;
    }

    public void OnEndDrag(PointerEventData eventData) { }

    public void SetTitle(string title)
    {
        if (titleText != null)
            titleText.text = title;
    }

    public void Close() => Destroy(gameObject);
}
