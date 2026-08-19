using UnityEngine;

public sealed class WindowManager : MonoBehaviour
{
    public static WindowManager Instance { get; private set; }

    [SerializeField] private RectTransform windowLayer;

    public RectTransform WindowLayer => windowLayer;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void BringToFront(WindowController window)
    {
        if (window != null)
            window.transform.SetAsLastSibling();
    }

    public void Register(WindowController window)
    {
        if (window == null)
            return;

        if (windowLayer != null && window.transform.parent != windowLayer)
            window.transform.SetParent(windowLayer, false);

        BringToFront(window);
    }
}
