using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChatBoxUI : MonoBehaviour
{
    #region variables
    private TMP_Text _nameText;
    private TMP_Text _chatText;
    private Transform _choiceArea;
    private GameObject _chatArea;
    private IEnumerator _typingCoroutine;
    private int _buttonIndex;
    private GameObject[] _choiceButtons;
    private bool _keyValue;
    private AudioClip _nextChatSound;

    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        _chatArea = transform.Find("ChatArea").gameObject;
        _nameText = transform.Find("ChatArea/NameText").GetComponent<TMP_Text>();
        _chatText = transform.Find("ChatArea/ChatText").GetComponent<TMP_Text>();
        _choiceArea = transform.Find("ChoiceArea");
        _buttonIndex = 0;
        _nextChatSound = ResourceManager.Instance.GetAudioClip("NextChatSound");
        GameObject choiceButton = Resources.Load<GameObject>("UI/ChoiceButton");

        _choiceButtons = new GameObject[4];
        for(int i=0; i < 4; i++)
        {
            _choiceButtons[i] = Instantiate(choiceButton, _choiceArea);
            _choiceButtons[i].SetActive(false);
        }
    }

    private void OnDisable()
    {
        if(_choiceButtons == null) return;
        CloseButtons();
    }

    private void Update()
    {
        _keyValue = IsKeyInput();
    }
    #endregion

    #region private
    private IEnumerator TypeSentence(string sentence)
    {
        _chatText.text = "";
        foreach ( char letter in sentence)
        {
            _chatText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

    private bool IsKeyInput()
    {
        return
        Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)
        || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X) ||Input.GetKeyDown(KeyCode.C);
    }

    private void SetChatArea(bool isActive)
    {
        gameObject.SetActive(isActive);
        _chatArea.SetActive(isActive);
        _choiceArea.gameObject.SetActive(!isActive);
    }

    private void SetChoiceArea(bool isActive)
    {
        gameObject.SetActive(isActive);
        _chatArea.SetActive(!isActive);
        _choiceArea.gameObject.SetActive(isActive);
    }

    private void PlayNextChatSound()
    {
        SFX.Instance.PlayOneShot(_nextChatSound);
    }
    #endregion

    #region public
    public IEnumerator StartChat(List<(string name, string chat)> chatDatas)
    {
        SetChatArea(true);

        int currentIndex = 0;
        int length = chatDatas.Count;

        _nameText.text = chatDatas[currentIndex].name;
        _typingCoroutine = TypeSentence(chatDatas[currentIndex].chat);
        yield return StartCoroutine(_typingCoroutine);
        currentIndex++;

        while(currentIndex < length)
        {
            if(_keyValue)
            {
                PlayNextChatSound();
                _nameText.text = chatDatas[currentIndex].name;
                _typingCoroutine = TypeSentence(chatDatas[currentIndex].chat);
                yield return StartCoroutine(_typingCoroutine);
                currentIndex++;
            }
            yield return null;
        }

        while(!_keyValue) yield return null;

        SetChatArea(false);
    }

    public void MakeButton(string content, UnityAction unityAction)
    {
        SetChoiceArea(true);
        
        GameObject buttonObj = _choiceButtons[_buttonIndex++];
        buttonObj.SetActive(true);
        Button button = buttonObj.GetComponent<Button>();
        TMP_Text contextText = buttonObj.transform.Find("ContentText").GetComponent<TMP_Text>();

        contextText.text = content;
        button.onClick.AddListener(unityAction);
    }

    public void CloseButtons()
    {
        _buttonIndex = 0;
        foreach(var obj in _choiceButtons)
        {
            obj.SetActive(false);
            obj.GetComponent<Button>().onClick.RemoveAllListeners();
        }
    }
    #endregion
}
