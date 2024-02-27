using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingImage : MonoBehaviour
{
    public TMP_Text loadingText; // Unity 인스펙터에서 할당

    //private void Start()
    //{
    //    if (loadingText != null)
    //    {
    //        StartCoroutine(AnimateDots());
    //    }
    //}

    private void OnEnable()
    {
        if (loadingText != null)
        {
            loadingText.text = RandomHelpText();
        }
    }

    IEnumerator AnimateDots()
    {
        while (true) // 무한 루프로 반복
        {
            loadingText.text = "굴러가는중.";
            yield return new WaitForSeconds(0.1f); // 0.5초 기다림
            loadingText.text = "굴러가는중..";
            yield return new WaitForSeconds(0.1f); // 0.5초 기다림
            loadingText.text = "굴러가는중...";
            yield return new WaitForSeconds(0.1f); // 0.5초 기다림
            loadingText.text = "굴러가는중....";
            yield return new WaitForSeconds(0.1f); // 0.5초 기다림
        }
    }

    private string RandomHelpText()
    {
        string[] helptext = {
            "A키를 누르면\n\r오러 블레이드 공격이 가능합니다.",
            "공중에서 아래 화살표와 A키를 누르면\n\r낙하 공격이 가능합니다",
            "인벤토리에서 보유 중인\n\r재료들을 볼 수 있습니다",
            "인벤토리에서 보유 중인\n\r스킬과 장비를 볼 수 있습니다",
            "대성당은 샹들리에를 따라가면\n\r꼭대기 층에 도달할 수 있습니다"
            };

        int randomIndex = Random.Range(0, helptext.Length);
        return helptext[randomIndex];
    }
}
