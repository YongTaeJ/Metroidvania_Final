using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LodingScene : MonoBehaviour
{
    [SerializeField] private Slider progressBar; // 진행률을 표시할 UI 슬라이더
    [SerializeField] private TMP_Text _storyText;
    [SerializeField] private TMP_Text _clickText;
    public string sceneToLoad = "MainScene";
    private bool _isChatEnd;

    private void Start()
    {
        _clickText.enabled = false;
        _isChatEnd = false;
        LoadScene(sceneToLoad);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
        StartCoroutine(StartChat());
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

            // 최소 로딩 시간이 지났고, 실제 로딩도 완료 + 게임 처음 시작시에는 텍스트 대기!
            if (Time.time - startTime >= minimumLoadingTime && operation.progress >= 0.9f && _isChatEnd)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    private IEnumerator StartChat()
    {
        // TODO => 세이브 있는지 없는지 확인(없으면 그냥 true 만들고 break)
        // playerprefs로 확인하면 편하게 할 수 있을 것 같습니다.
        if(false)
        {
            _isChatEnd = true;
            yield break;
        }

        yield return StartCoroutine(Chat(
            "당신은 모험을 하던 도중, 한 시골 마을에 사건이 터졌다는 것을 알게 되었습니다."
        ));

        yield return StartCoroutine(Chat(
            "도와줘도 딱히 큰 보수는 없는 것 같지만, 당신은 심심해서 가보기로 마음 먹었습니다."
        ));

        yield return StartCoroutine(Chat(
            "마을에 가서 상황을 확인하고, 주민들을 도와주도록 합시다!"
        ));

        yield return StartCoroutine(Chat(
            "주의 사항 : 수동 저장 기능은 없으며, 포탈을 이용할 때 자동으로 저장됩니다."
        ));

        _isChatEnd = true;
    }

    private IEnumerator Chat(string sentence)
    {
        _clickText.enabled = false;

        _storyText.text = "";
        foreach ( char letter in sentence)
        {
            _storyText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }

        _clickText.enabled = true;
        while(!Input.GetMouseButtonDown(0)) yield return null;
    }
}
