using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class TaskItemUI : MonoBehaviour
{
    [SerializeField] private TMP_Text taskNameText;
    [SerializeField] private Image checkImage;

    public void SetTask(TaskState state)
    {
        if (state == null)
            return;

        if (taskNameText != null)
            taskNameText.text = state.Data != null ? state.Data.TaskName : string.Empty;

        if (checkImage != null)
            checkImage.gameObject.SetActive(state.IsCompleted);
    }
}
