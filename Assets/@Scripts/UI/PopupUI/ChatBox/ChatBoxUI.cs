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
    private IEnumerator _typingCoroutine;
    private int _buttonIndex;
    private GameObject[] _choiceButtons;
    private bool _keyValue;

    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        _nameText = transform.Find("ChatArea/NameText").GetComponent<TMP_Text>();
        _chatText = transform.Find("ChatArea/ChatText").GetComponent<TMP_Text>();
        _choiceArea = transform.Find("ChoiceArea");
        _buttonIndex = 0;
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

    #region ChatArea
    private IEnumerator TypeSentence(string sentence)
    {
        _chatText.text = "";
        foreach ( char letter in sentence)
        {
            // TODO => 입력시 스킵 추가
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
    #endregion

    #region public
    public IEnumerator StartChat(List<(string name, string chat)> chatDatas)
    {
        gameObject.SetActive(true);

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
                _nameText.text = chatDatas[currentIndex].name;
                _typingCoroutine = TypeSentence(chatDatas[currentIndex].chat);
                yield return StartCoroutine(_typingCoroutine);
                currentIndex++;
            }
            yield return null;
        }

        while(!_keyValue) yield return null;

        gameObject.SetActive(false);
    }

    public void MakeButton(string content, UnityAction unityAction)
    {
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

    // TODO => 경로 추적용 임시 코드(그냥 UI매니저로 호출할지 고민중)
    public void ActiveUI(bool flag)
    {
        gameObject.SetActive(flag);
    }
    #endregion
}
