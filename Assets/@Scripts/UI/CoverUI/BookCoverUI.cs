using UnityEngine;

public abstract class BookCoverUI : MonoBehaviour
{
    #region variables
    protected BookCoverEventReceiver _eventReceiver;
    #endregion

    private void Awake()
    {
        _eventReceiver = transform.Find("BookCover").GetComponent<BookCoverEventReceiver>();
        _eventReceiver.OnCloseUI += CloseUI;
        _eventReceiver.OnOpenUI += OpenUI;
        _eventReceiver.OnFlipUI += FlipUI;
    }

    protected abstract void CloseUI();
    protected abstract void OpenUI();
    protected abstract void FlipUI();
}
