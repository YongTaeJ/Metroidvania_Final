using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class ChatManager : Singleton<ChatManager>
{
    private ChatBoxUI _chatBoxUI;
    private List<List<(string, string)>> _chatDataList;


    public override bool Initialize()
    {
        _chatBoxUI = UIManager.Instance.GetUI(PopupType.ChatBox).GetComponent<ChatBoxUI>();
        GetDataFromResource();

        return base.Initialize();
    }

    private void GetDataFromResource()
    {
        TextAsset chatData_Json = Resources.Load<TextAsset>("Json/ChatData");
        ChatData[] _chatDatas = JsonConvert.DeserializeObject<ChatDataContainer>(chatData_Json.ToString()).ChatDatas;

        _chatDataList = new List<List<(string, string)>>();

        // 데이터 규칙 : ID 하나당 하나의 대화를 이룰 것. 동일한 ID에서는 위에서 아래로 대화 진행.
        int currentIndex = -1;
        foreach(var data in _chatDatas)
        {
            if(_chatDataList.Count - 1 != data.ChatID)
            {
                _chatDataList.Add(new List<(string, string)>());
                currentIndex++;
            }
            _chatDataList[currentIndex].Add((data.Name, data.Chat));
        }
    }

    public IEnumerator StartChatting(int ID)
    {
        yield return StartCoroutine(_chatBoxUI.StartChatting(_chatDataList[ID]));
        Debug.Log("chatend1");
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