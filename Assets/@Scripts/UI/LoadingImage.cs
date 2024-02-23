using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingImage : MonoBehaviour
{
    public TMP_Text loadingText; // Unity 인스펙터에서 할당

    private void Start()
    {
        if (loadingText != null)
        {
            StartCoroutine(AnimateDots());
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
}
