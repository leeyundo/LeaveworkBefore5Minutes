using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class FileItem : MonoBehaviour
{
    private static FileItem selectedItem;

    [SerializeField] private string fileName = "New File";
    [SerializeField] private TMP_Text fileNameText;
    [SerializeField] private Image iconImage;
    [SerializeField] private GameObject selectionHighlight;

    public string FileName => fileName;

    private void Awake()
    {
        RefreshName();
        SetSelected(selectedItem == this);
    }

    public void SetFileName(string value)
    {
        fileName = value;
        RefreshName();
    }

    public void SetIcon(Sprite icon)
    {
        if (iconImage != null)
            iconImage.sprite = icon;
    }

    public void Select()
    {
        if (selectedItem != null && selectedItem != this)
            selectedItem.SetSelected(false);

        selectedItem = this;
        SetSelected(true);
    }

    private void OnDisable()
    {
        if (selectedItem == this)
            selectedItem = null;
    }

    private void RefreshName()
    {
        if (fileNameText != null)
            fileNameText.text = fileName;
    }

    private void SetSelected(bool isSelected)
    {
        if (selectionHighlight != null)
            selectionHighlight.SetActive(isSelected);
    }
}
