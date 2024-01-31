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
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        _nameText = transform.Find("NameText").GetComponent<TMP_Text>();
        _chatText = transform.Find("ChatText").GetComponent<TMP_Text>();
    }
    #endregion

    #region private
    private IEnumerator WaitforInput((string, string)[] chatDatas)
    {
        _nameText.text = chatDatas[0].Item1;
        TypeSentence(chatDatas[0].Item2);
        int currentIndex = 1;
        int length = chatDatas.Length;

        while(true)
        {
            if(IsKeyInput())
            {
                if(currentIndex < length)
                {
                    _nameText.text = chatDatas[currentIndex].Item1;
                    TypeSentence(chatDatas[currentIndex].Item2);
                    currentIndex++;
                }
                else
                {
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
            if(IsKeyInput())
            {
                _chatText.text = sentence;
                yield break;
            }
            _chatText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

    private bool IsKeyInput()
    {
        return Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return);
    }
    #endregion

    #region public
    public void StartChatting((string, string)[] chatDatas)
    {
        _chatCoroutine = WaitforInput(chatDatas);
        StartCoroutine(_chatCoroutine);
    }
    #endregion
}
