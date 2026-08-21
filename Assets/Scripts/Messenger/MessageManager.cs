using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class MessageManager : MonoBehaviour
{
    [SerializeField] private List<MessageData> messageList = new();

    public MessageData CurrentMessage { get; private set; }

    public event Action<MessageData> OnMessageOpened;
    public event Action OnMessageClosed;
    public event Action<MessageData> OnMessageAccepted;
    public event Action<MessageData> OnMessageDeclined;

    public void ShowMessage(MessageData data)
    {
        if (data == null)
            return;

        CurrentMessage = data;
        OnMessageOpened?.Invoke(CurrentMessage);
    }

    public void Accept()
    {
        if (CurrentMessage == null)
            return;

        MessageData message = CurrentMessage;
        OnMessageAccepted?.Invoke(message);
        CloseCurrentMessage();
    }

    public void Decline()
    {
        if (CurrentMessage == null)
            return;

        MessageData message = CurrentMessage;
        OnMessageDeclined?.Invoke(message);
        CloseCurrentMessage();
    }

    private void CloseCurrentMessage()
    {
        CurrentMessage = null;
        OnMessageClosed?.Invoke();
    }
}
