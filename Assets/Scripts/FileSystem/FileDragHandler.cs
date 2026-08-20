using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public sealed class FileDragHandler : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private FileItem fileItem;
    [SerializeField] private RectTransform itemRect;

    public FileItem FileItem => fileItem;

    private RectTransform parentRect;
    private RectTransform originalParent;
    private Vector2 pointerOffset;
    private Vector2 originalAnchoredPosition;
    private int originalSiblingIndex;

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
        originalParent = parentRect;
        originalAnchoredPosition = itemRect.anchoredPosition;
        originalSiblingIndex = itemRect.GetSiblingIndex();

        Canvas rootCanvas = itemRect.GetComponentInParent<Canvas>()?.rootCanvas;
        if (rootCanvas != null && rootCanvas.transform is RectTransform canvasRoot)
        {
            itemRect.SetParent(canvasRoot, true);
            parentRect = canvasRoot;
        }

        itemRect.SetAsLastSibling();
        SetRaycastTargets(false);
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

    public void OnEndDrag(PointerEventData eventData)
    {
        try
        {
            if (itemRect == null || fileItem == null)
                return;

            if (!TryHandleDrop(eventData))
                RestoreOriginalPosition();

            parentRect = itemRect.parent as RectTransform;
        }
        finally
        {
            SetRaycastTargets(true);
        }
    }

    private bool TryHandleDrop(PointerEventData eventData)
    {
        if (EventSystem.current == null)
            return false;

        var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);

        IDesktopFileDropReceiver desktopFallback = null;

        foreach (RaycastResult result in raycastResults)
        {
            if (IsOwnGraphic(result.gameObject))
                continue;

            foreach (MonoBehaviour component in result.gameObject.GetComponentsInParent<MonoBehaviour>(true))
            {
                if (component is IDesktopFileDropReceiver desktopTarget)
                {
                    desktopFallback ??= desktopTarget;
                    continue;
                }

                if (component is IFileDropReceiver fileTarget)
                {
                    fileTarget.HandleDrop(fileItem, eventData);
                    return true;
                }
            }
        }

        if (desktopFallback != null)
        {
            desktopFallback.HandleDrop(fileItem, eventData);
            return true;
        }

        return false;
    }

    private bool IsOwnGraphic(GameObject target)
    {
        return itemRect != null &&
               (target.transform == itemRect || target.transform.IsChildOf(itemRect));
    }

    private void SetRaycastTargets(bool enabled)
    {
        if (itemRect == null)
            return;

        foreach (Graphic graphic in itemRect.GetComponentsInChildren<Graphic>(true))
            graphic.raycastTarget = enabled;
    }

    private void RestoreOriginalPosition()
    {
        if (originalParent == null)
            return;

        itemRect.SetParent(originalParent, false);
        itemRect.SetSiblingIndex(originalSiblingIndex);
        itemRect.anchoredPosition = originalAnchoredPosition;
    }

    private Camera GetEventCamera(PointerEventData eventData)
    {
        Canvas rootCanvas = itemRect != null
            ? itemRect.GetComponentInParent<Canvas>()?.rootCanvas
            : null;

        return rootCanvas != null && rootCanvas.renderMode != RenderMode.ScreenSpaceOverlay
            ? eventData.pressEventCamera
            : null;
    }
}
