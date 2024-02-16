using System;
using UnityEngine;

public class BookCoverEventReceiver : MonoBehaviour
{
    public event Action OnCloseUI;
    public event Action OnOpenUI;
    public event Action OnFlipUI;

    private void Animation_Close()
    {
        OnCloseUI?.Invoke();
    }

    private void Animation_Open()
    {
        OnCloseUI?.Invoke();
    }
    private void Animation_Flip()
    {
        OnCloseUI?.Invoke();
    }
}
