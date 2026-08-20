using UnityEngine;
using UnityEngine.EventSystems;

public sealed class FileDragHandler : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private FileItem fileItem;
    [SerializeField] private RectTransform itemRect;
    [SerializeField] private Canvas canvas;

    public FileItem FileItem => fileItem;

    private RectTransform parentRect;
    private Vector2 pointerOffset;

    private void Awake()
    {
        itemRect ??= transform as RectTransform;
        parentRect = itemRect != null ? itemRect.parent as RectTransform : null;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        fileItem?.Select();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (itemRect == null || parentRect == null)
            return;

        fileItem?.Select();
        itemRect.SetAsLastSibling();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            itemRect, eventData.position, GetEventCamera(eventData), out pointerOffset);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (itemRect == null || parentRect == null)
            return;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentRect, eventData.position, GetEventCamera(eventData), out var pointerPosition))
            itemRect.localPosition = pointerPosition - pointerOffset;
    }

    public void OnEndDrag(PointerEventData eventData) { }

    private Camera GetEventCamera(PointerEventData eventData)
    {
        return canvas != null && canvas.renderMode != RenderMode.ScreenSpaceOverlay
            ? eventData.pressEventCamera
            : null;
    }
}
