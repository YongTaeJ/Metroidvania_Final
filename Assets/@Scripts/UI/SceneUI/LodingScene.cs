using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LodingScene : MonoBehaviour
{
    [SerializeField] private Slider progressBar; // 진행률을 표시할 UI 슬라이더
    public string sceneToLoad = "MainScene";

    private void Start()
    {
        LoadScene(sceneToLoad);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        float minimumLoadingTime = 2.0f; // 최소 로딩 시간을 2초로 설정
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        float startTime = Time.time;

        // 실제 로딩 진행과 동시에 최소 로딩 시간을 충족시키기 위한 루프
        while (!operation.isDone || Time.time - startTime < minimumLoadingTime)
        {
            float totalProgress = Mathf.Clamp01(operation.progress / 0.9f);
            // 최소 로딩 시간 동안은 진행률을 시간에 비례하여 계산
            if (Time.time - startTime < minimumLoadingTime)
            {
                totalProgress = Mathf.Lerp(0f, 1f, (Time.time - startTime) / minimumLoadingTime);
            }

            progressBar.value = totalProgress;

            // 최소 로딩 시간이 지났고, 실제 로딩도 완료된 경우
            if (Time.time - startTime >= minimumLoadingTime && operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
