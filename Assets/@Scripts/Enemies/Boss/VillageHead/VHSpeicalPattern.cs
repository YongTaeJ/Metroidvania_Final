
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VHSpeicalPattern : MonoBehaviour
{
    private int _variation;
    private (Sprite, float, float)[] _rockDatas;
    private GameObject _rockPrefab;
    private float _boundary;
    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        _boundary = 11f;
        _variation = 5;
        _rockPrefab = Resources.Load<GameObject>("Enemies/Rocks/Rock");

        Sprite[] rockSprites = Resources.LoadAll<Sprite>("Enemies/Rocks/RockPack");

        _rockDatas = new(Sprite, float, float)[_variation];
        _rockDatas[0] = (rockSprites[0], 2f, 1.6f);
        _rockDatas[1] = (rockSprites[1], 1.6f, 1.4f);
        _rockDatas[2] = (rockSprites[2], 1.4f, 1.1f);
        _rockDatas[3] = (rockSprites[3], 1.4f, 1.3f);
        _rockDatas[4] = (rockSprites[4], 1.2f, 1.1f);
    }

    public void CreateRock()
    {
        Rock rock = Instantiate(_rockPrefab).GetComponent<Rock>();
        rock.transform.position += Vector3.right * Random.Range(-_boundary, _boundary);

        // TODO => 적당하게 position 조정
        int random = Random.Range(0, _variation);
        rock.Initialize(_rockDatas[random].Item1, _rockDatas[random].Item2, _rockDatas[random].Item3);
    }
}
