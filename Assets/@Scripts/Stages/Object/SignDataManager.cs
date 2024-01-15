using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignDataManager : MonoBehaviour
{
    public static SignDataManager Instance;

    public Dictionary<string, string> SignMessages = new Dictionary<string, string>();

    [SerializeField]
    private SignTexts _signTexts;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SignMessages.Add("Sign1", _signTexts.FirstSign);
        SignMessages.Add("Sign2", _signTexts.SecondSign);
        SignMessages.Add("Sign3", _signTexts.ThirdSign);
        SignMessages.Add("Sign4", _signTexts.FourthSign);
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
