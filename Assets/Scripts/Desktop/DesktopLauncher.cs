using UnityEngine;

public sealed class DesktopLauncher : MonoBehaviour
{
    [SerializeField] private WindowController windowPrefab;
    [SerializeField] private RectTransform windowLayer;
    [SerializeField] private Vector2 spawnOffset = new(24f, -18f);

    public void OpenWindow(string title)
    {
        if (windowPrefab == null || windowLayer == null)
        {
            Debug.LogWarning("DesktopLauncher requires Window Prefab and Window Layer references.", this);
            return;
        }

        WindowController window = Instantiate(windowPrefab, windowLayer);
        window.SetTitle(title);

        if (window.transform is RectTransform windowRect)
            windowRect.anchoredPosition = spawnOffset;
    }
}
