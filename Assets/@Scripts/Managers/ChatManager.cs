using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.InputSystem.Utilities;

public class ChatManager : Singleton<ChatManager>
{
    #region variables
    private Dictionary<int, List<(string name, string chat)>> _chatDataList;
    private ChatBoxUI _chatBoxUI;
    #endregion

    public override bool Initialize()
    {
        if(base.Initialize())
        {
            GetDataFromResource();
            InitComponents();
        }
        return true;
    }

    private void GetDataFromResource()
    {
        TextAsset chatData_Json = Resources.Load<TextAsset>("Json/ChatData");
        ChatData[] _chatDatas = JsonConvert.DeserializeObject<ChatDataContainer>(chatData_Json.ToString()).ChatDatas;

        _chatDataList = new Dictionary<int, List<(string, string)>>();

        foreach(var data in _chatDatas)
        {
            if(!_chatDataList.ContainsKey(data.ChatID))
            {
                _chatDataList.Add(data.ChatID, new List<(string, string)>());
            }
            _chatDataList[data.ChatID].Add((data.Name, data.Chat));
        }
    }

    private void InitComponents()
    {
        _chatBoxUI = UIManager.Instance.GetUI(PopupType.ChatBox).GetComponent<ChatBoxUI>();
    }

    public List<(string, string)> GetChatData(int ID)
    {
        return _chatDataList[ID];
    }

    public IEnumerator StartChat(int ID)
    {
        var chatDatas = GetChatData(ID);
        yield return StartCoroutine(_chatBoxUI.StartChat(chatDatas));
    }
}

public class ChatDataContainer
{
    public ChatData[] ChatDatas;
}

public class ChatData
{
    public int ChatID;
    public string Name;
    public string Chat;
}