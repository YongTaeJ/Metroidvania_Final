using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SignDataManager : Singleton<SignDataManager>
{
    public Dictionary<string, string> SignMessages = new Dictionary<string, string>();

    private SignTexts _signTexts = new SignTexts();

    protected override void Awake()
    {
        base.Awake();

        SignMessages.Add("Sign00", _signTexts.Sign00);
        SignMessages.Add("Sign01", _signTexts.Sign01);
        SignMessages.Add("Sign02", _signTexts.Sign02);
        SignMessages.Add("Sign03", _signTexts.Sign03);
        SignMessages.Add("Sign04", _signTexts.Sign04);
        SignMessages.Add("Sign05", _signTexts.Sign05);
        SignMessages.Add("Sign06", _signTexts.Sign06);
    }

    public string GetMessage(string SignId)
    {
        if (SignMessages.TryGetValue(SignId, out string message))
        {
            return message;
        }
        else
        {
            return "no message";
        }
    }
}
