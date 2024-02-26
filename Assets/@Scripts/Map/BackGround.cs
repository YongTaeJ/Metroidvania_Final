using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public float Speed;
    public int StartIndex;
    public int EndIndex;
    public Transform[] sprites;

    private float viewWidth;
    private void Awake()
    {
        viewWidth = 30f;
    }
    private void Update()
    {
        Vector3 curPos = transform.position;
        Vector3 nextPos = Vector3.left * Speed * Time.deltaTime;
        transform.position = curPos + nextPos;

        if (sprites[EndIndex].position.x < viewWidth * (-1f))
        {
            Vector3 backSpritePos = sprites[StartIndex].localPosition;
            Vector3 frontSpritePos = sprites[EndIndex].localPosition;
            sprites[EndIndex].transform.localPosition = backSpritePos + Vector3.right * 4.8f;

            int startIndexSave = StartIndex;
            StartIndex = EndIndex;
            EndIndex = startIndexSave - 1 == -1 ? sprites.Length - 1 : startIndexSave - 1;
        }
    }
}
