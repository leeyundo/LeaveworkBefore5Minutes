using UnityEngine;
using UnityEngine.EventSystems;

public sealed class DesktopDropTarget : MonoBehaviour, IDropHandler, IDesktopFileDropReceiver
{
    [SerializeField] private RectTransform desktopRoot;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null &&
            eventData.pointerDrag.TryGetComponent<FileDragHandler>(out var dragHandler))
            HandleDrop(dragHandler.FileItem, eventData);
    }

    public void HandleDrop(FileItem item, PointerEventData eventData)
    {
        if (item == null || desktopRoot == null)
            return;

        item.transform.SetParent(desktopRoot, false);
        item.SetLocation(FileLocation.Desktop);

        if (item.transform is RectTransform itemRect &&
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                desktopRoot, eventData.position, eventData.pressEventCamera, out var dropPosition))
            itemRect.anchoredPosition = dropPosition;
    }
}
