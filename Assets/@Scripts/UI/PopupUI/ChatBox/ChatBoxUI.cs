using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatBoxUI : MonoBehaviour
{
    #region variables
    private TMP_Text _nameText;
    private TMP_Text _chatText;
    private IEnumerator _chatCoroutine;
    private IEnumerator _typingCoroutine;
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        _nameText = transform.Find("NameText").GetComponent<TMP_Text>();
        _chatText = transform.Find("ConversationArea/ChatText").GetComponent<TMP_Text>();
    }
    #endregion

    #region private
    private IEnumerator WaitforInput(List<(string, string)> chatDatas)
    {
        int currentIndex = 0;
        int length = chatDatas.Count;

        _nameText.text = chatDatas[currentIndex].Item1;
        _typingCoroutine = TypeSentence(chatDatas[currentIndex].Item2);
        StartCoroutine(_typingCoroutine);
        currentIndex++;

        while(true)
        {
            if(IsKeyInput())
            {
                yield return null;
                if(currentIndex < length)
                {
                    _nameText.text = chatDatas[currentIndex].Item1;
                    if(_typingCoroutine != null)
                    {
                        StopCoroutine(_typingCoroutine);
                    }
                    _typingCoroutine = TypeSentence(chatDatas[currentIndex].Item2);
                    yield return StartCoroutine(_typingCoroutine);
                    currentIndex++;
                }
                else
                {
                    StopCoroutine(_typingCoroutine);
                    yield break;
                }
                yield return new WaitForSeconds(0.5f);
            }
            yield return null;
        }
    }

    private IEnumerator TypeSentence(string sentence)
    {
        _chatText.text = "";
        foreach ( char letter in sentence)
        {
            // TOOD => (후순위) 나중에 스킵 기능 추가
            _chatText.text += letter;
            yield return new WaitForSeconds(0.1f);
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
    public IEnumerator StartChatting(List<(string, string)> chatDatas)
    {
        gameObject.SetActive(true);
        _chatCoroutine = WaitforInput(chatDatas);
        yield return StartCoroutine(_chatCoroutine);
        gameObject.SetActive(false);
    }
    #endregion
}
