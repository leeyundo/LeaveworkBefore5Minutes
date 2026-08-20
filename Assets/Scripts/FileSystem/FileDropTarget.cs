using UnityEngine;
using UnityEngine.EventSystems;

public enum DropTargetType
{
    Documents,
    USB,
    RecycleBin
}

public interface IFileDropReceiver
{
    void HandleDrop(FileItem item, PointerEventData eventData);
}

public interface IDesktopFileDropReceiver : IFileDropReceiver { }

public sealed class FileDropTarget : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler, IFileDropReceiver
{
    [SerializeField] private DropTargetType targetType;
    [SerializeField] private GameObject dropHighlight;

    public DropTargetType TargetType => targetType;
    public FileItem LastDroppedItem { get; private set; }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
            SetHighlight(true);
    }

    public void OnPointerExit(PointerEventData eventData) => SetHighlight(false);

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null &&
            eventData.pointerDrag.TryGetComponent<FileDragHandler>(out var dragHandler))
            HandleDrop(dragHandler.FileItem, eventData);

        SetHighlight(false);
    }

    public void HandleDrop(FileItem item, PointerEventData eventData)
    {
        if (item == null)
            return;

        item.transform.SetParent(transform, false);
        if (item.transform is RectTransform itemRect)
            itemRect.anchoredPosition = Vector2.zero;

        item.SetLocation(ToFileLocation(targetType));
        LastDroppedItem = item;
    }

    private static FileLocation ToFileLocation(DropTargetType type)
    {
        return type switch
        {
            DropTargetType.Documents => FileLocation.Documents,
            DropTargetType.USB => FileLocation.USB,
            DropTargetType.RecycleBin => FileLocation.RecycleBin,
            _ => FileLocation.Desktop
        };
    }

    private void Awake() => SetHighlight(false);

    private void SetHighlight(bool isVisible)
    {
        if (dropHighlight != null)
            dropHighlight.SetActive(isVisible);
    }
}
