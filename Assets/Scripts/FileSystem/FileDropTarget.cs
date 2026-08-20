using UnityEngine;
using UnityEngine.EventSystems;

public sealed class FileDropTarget : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject dropHighlight;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
            SetHighlight(true);
    }

    public void OnPointerExit(PointerEventData eventData) => SetHighlight(false);

    public void OnDrop(PointerEventData eventData)
    {
        // This step intentionally keeps the FileItem at its current UI position.
        SetHighlight(false);
    }

    private void Awake() => SetHighlight(false);

    private void SetHighlight(bool isVisible)
    {
        if (dropHighlight != null)
            dropHighlight.SetActive(isVisible);
    }
}
