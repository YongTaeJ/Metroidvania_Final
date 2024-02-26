using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Camera mainCamera;
    Vector3 cameraPos;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }

    public void Shake(float shakeRange, float duration)
    {
        cameraPos = mainCamera.transform.position;
        StartCoroutine(StartShake(shakeRange, duration));
    }

    private IEnumerator StartShake(float shakeRange, float duration)
    {
        float startTime = Time.time;
        while (Time.time - startTime < duration)
        {
            float cameraPosX = Random.Range(-shakeRange, shakeRange);
            float cameraPosY = Random.Range(-shakeRange, shakeRange);
            Vector3 cameraOffset = new Vector3(cameraPosX, cameraPosY, 0);
            mainCamera.transform.position = cameraPos + cameraOffset;
            yield return null;
        }
        mainCamera.transform.position = cameraPos;
    }
}
