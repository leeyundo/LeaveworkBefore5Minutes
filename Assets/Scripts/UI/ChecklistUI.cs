using UnityEngine;

public sealed class ChecklistUI : MonoBehaviour
{
    [SerializeField] private TaskManager taskManager;
    [SerializeField] private Transform contentRoot;
    [SerializeField] private GameObject taskItemPrefab;

    private void OnEnable()
    {
        if (taskManager != null)
            taskManager.OnTaskChanged += Refresh;
    }

    private void Start()
    {
        Refresh();
    }

    private void OnDisable()
    {
        if (taskManager != null)
            taskManager.OnTaskChanged -= Refresh;
    }

    public void Refresh()
    {
        if (taskManager == null || contentRoot == null || taskItemPrefab == null)
            return;

        for (int index = contentRoot.childCount - 1; index >= 0; index--)
            Destroy(contentRoot.GetChild(index).gameObject);

        foreach (TaskState state in taskManager.GetAllTasks())
            GenerateItem(state);
    }

    private void GenerateItem(TaskState state)
    {
        GameObject item = Instantiate(taskItemPrefab, contentRoot);
        if (item.TryGetComponent<TaskItemUI>(out var taskItemUI))
            taskItemUI.SetTask(state);
    }
}
