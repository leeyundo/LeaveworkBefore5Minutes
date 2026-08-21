using UnityEngine;

[CreateAssetMenu(fileName = "Message", menuName = "LeaveWork/Message")]
public sealed class MessageData : ScriptableObject
{
    [SerializeField] private string senderName;
    [TextArea]
    [SerializeField] private string message;
    [SerializeField] private string acceptText;
    [SerializeField] private string declineText;
    [SerializeField] private TaskData relatedTask;

    public string SenderName => senderName;
    public string Message => message;
    public string AcceptText => acceptText;
    public string DeclineText => declineText;
    public TaskData RelatedTask => relatedTask;
}
