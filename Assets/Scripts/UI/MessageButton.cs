using UnityEngine;

public sealed class MessageButton : MonoBehaviour
{
    [SerializeField] private bool isAccept;
    [SerializeField] private MessageManager manager;

    public void OnClick()
    {
        if (manager == null)
            return;

        if (isAccept)
            manager.Accept();
        else
            manager.Decline();
    }
}
