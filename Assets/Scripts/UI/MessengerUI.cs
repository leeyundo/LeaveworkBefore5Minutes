using TMPro;
using UnityEngine;

public sealed class MessengerUI : MonoBehaviour
{
    [SerializeField] private MessageManager messageManager;
    [SerializeField] private TMP_Text senderText;
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private TMP_Text acceptButtonText;
    [SerializeField] private TMP_Text declineButtonText;
    [SerializeField] private GameObject root;

    private void OnEnable()
    {
        if (messageManager == null)
            return;

        messageManager.OnMessageOpened += Open;
        messageManager.OnMessageClosed += Close;
    }

    private void Start()
    {
        if (messageManager != null && messageManager.CurrentMessage != null)
            Open(messageManager.CurrentMessage);
        else
            Close();
    }

    private void OnDisable()
    {
        if (messageManager == null)
            return;

        messageManager.OnMessageOpened -= Open;
        messageManager.OnMessageClosed -= Close;
    }

    private void Open(MessageData data)
    {
        if (senderText != null)
            senderText.text = data.SenderName;
        if (messageText != null)
            messageText.text = data.Message;
        if (acceptButtonText != null)
            acceptButtonText.text = data.AcceptText;
        if (declineButtonText != null)
            declineButtonText.text = data.DeclineText;

        if (root != null)
            root.SetActive(true);
    }

    private void Close()
    {
        if (root != null)
            root.SetActive(false);
    }
}
