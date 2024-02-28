using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 기존 ChatBox를 사용할 수 없을때 쓰는 UI입니다. 버튼이 없는거 빼면 사용법은 동일!
/// </summary>
public class EventChatBoxUI : MonoBehaviour
{
    #region variables
    private TMP_Text _nameText;
    private TMP_Text _chatText;
    private IEnumerator _typingCoroutine;
    private bool _keyValue;
    private bool _isSkipped;

    #endregion

    #region MonoBehaviour

    private void Update()
    {
        _keyValue = IsKeyInput();
    }
    #endregion

    #region private
    private IEnumerator TypeSentence(string sentence)
    {
        _isSkipped = false;

        _chatText.text = "";
        foreach (char letter in sentence)
        {
            _chatText.text += letter;
            yield return ChatTimer();
            if(_isSkipped)
            {
                _chatText.text = sentence;
                yield break;
            } 
        }
    }

    private IEnumerator ChatTimer()
    {
        float time = 0f;

        yield return null;

        while(time < 0.05f)
        {
            time += Time.deltaTime;
            if(IsKeyInput())
            {
                _isSkipped = true;
                yield return null;
                break;
            }
            yield return null;
        }
    }

    private bool IsKeyInput()
    {
        return
        Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)
        || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.C)
        || Input.GetKeyDown(KeyCode.F);
    }

    private void PlayNextChatSound()
    {
        SFX.Instance.PlayOneShot("NextChatSound");
    }

    private IEnumerator WaitForKeyInput()
    {
        while(!_keyValue)
        {
            yield return null;
        }
    }

    #endregion

    #region public
    public IEnumerator StartChat(List<(string name, string chat)> chatDatas)
    {
        int currentIndex = 0;
        int length = chatDatas.Count;
        
        while(currentIndex < length)
        {
            PlayNextChatSound();
            if(chatDatas[currentIndex].name == null)
            {
                _nameText.text = "";
            }
            else
            {
                _nameText.text = chatDatas[currentIndex].name;
            }
            _typingCoroutine = TypeSentence(chatDatas[currentIndex].chat);
            yield return StartCoroutine(_typingCoroutine);
            currentIndex++;
            yield return WaitForKeyInput();
        }
        yield return WaitForKeyInput();
    }

    public void EndChat()
    {
        Destroy(gameObject);
    }

    public void Initialize()
    {
        _nameText = transform.Find("ChatArea/NameText").GetComponent<TMP_Text>();
        _chatText = transform.Find("ChatArea/ChatText").GetComponent<TMP_Text>();
    }
    #endregion
}
