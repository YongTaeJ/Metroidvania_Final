using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatManager : Singleton<ChatManager>
{
    private ChatData[] _chatDatas;

    public override bool Initialize()
    {
        // TextAsset chatData_Json = Resources.Load<TextAsset>("Json/ChatData");
        // _chatDatas = JsonConvert.DeserializeObject<DropTableArray>(itemTable_Json.ToString()).DropTables;
        return base.Initialize();
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