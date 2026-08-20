using UnityEngine;
using UnityEngine.EventSystems;

public enum DropTargetType
{
    Documents,
    USB,
    RecycleBin
}

public sealed class FileDropTarget : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
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
            HandleDrop(dragHandler.FileItem);

        SetHighlight(false);
    }

    public void HandleDrop(FileItem item)
    {
        if (item == null)
            return;

        // This step records the target only; it intentionally does not move, copy, or delete files.
        LastDroppedItem = item;
    }

    private void Awake() => SetHighlight(false);

    private void SetHighlight(bool isVisible)
    {
        if (dropHighlight != null)
            dropHighlight.SetActive(isVisible);
    }
}
