using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.InputSystem.Utilities;

public class ChatManager : Singleton<ChatManager>
{
    #region variables
    private Dictionary<int, List<(string name, string chat)>> _chatDataList;
    #endregion

    public override bool Initialize()
    {
        if(base.Initialize())
        {
            GetDataFromResource();

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

    public List<(string, string)> GetChatData(int ID)
    {
        return _chatDataList[ID];
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