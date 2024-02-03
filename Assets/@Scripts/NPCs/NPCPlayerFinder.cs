using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class NPCPlayerFinder : MonoBehaviour
{
    #region Variables
    [SerializeField] private int ChatID;
    private Transform _playerTransform;
    private PlayerInput _playerInput;
    private ObjectFlip _objectFlip;
    #endregion

    public void Initialize(ObjectFlip objectFlip)
    {
        _objectFlip = objectFlip;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            _playerTransform = other.transform;
            _playerInput = other.GetComponent<PlayerInput>();
            var playerInputController = other.GetComponent<PlayerInputController>();
            playerInputController.OnInteraction -= StartConversation;
            playerInputController.OnInteraction += StartConversation;
            UIManager.Instance.OpenPopupUI(PopupType.Interact);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            var playerInputController = other.GetComponent<PlayerInputController>();
            playerInputController.OnInteraction -= StartConversation;
            UIManager.Instance.ClosePopupUI(PopupType.Interact);
        } 
    }

    private void StartConversation()
    {
        StopAllCoroutines();
        StartCoroutine(Conversation());
    }

    private IEnumerator Conversation()
    {
        float direction =
        _playerTransform.position.x - transform.position.x > 0 ?
        1 : -1;
        _objectFlip.Flip(direction);

        _playerInput.enabled = false;

        yield return StartCoroutine(ChatManager.Instance.StartChatting(ChatID));

        _playerInput.enabled = true;
    }
}
