using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class NPCPlayerFinder : MonoBehaviour
{
    #region Variables
    private NPCInteraction _interaction;
    public Transform _playerTransform {get; private set;}
    public PlayerInput _playerInput {get; private set;}
    public ObjectFlip _objectFlip {get; private set;}
    protected Canvas _press;

    #endregion

    private void Awake()
    {
        _objectFlip = new ObjectFlip(transform.parent);
        _interaction = GetComponent<NPCInteraction>();
        _press = GetComponentInChildren<Canvas>(true);
        if (_press != null) _press.gameObject.SetActive(false);
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
            //UIManager.Instance.OpenPopupUI(PopupType.Interact);
            if (_press != null) _press.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            var playerInputController = other.GetComponent<PlayerInputController>();
            playerInputController.OnInteraction -= StartConversation;
            //UIManager.Instance.ClosePopupUI(PopupType.Interact);
            if (_press != null) _press.gameObject.SetActive(false);
        } 
    }

    private void StartConversation()
    {
        float xDir = transform.position.x - _playerTransform.position.x;
        _objectFlip.Flip(xDir);
        StartCoroutine(_interaction.Interact(_playerInput));
    }
}
