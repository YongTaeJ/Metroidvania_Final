using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHeal : WallBase
{
    [SerializeField]
    private float _wallHP = 5;
    [SerializeField]
    private float _wallHeal = 2f;
    protected override void WallReact(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            SFX.Instance.PlayOneShot(ResourceManager.Instance.GetAudioClip("WallReact"));
            _wallHP--;


            if (_wallHP <= 0)
            {
                GameManager.Instance.player.GainHP(_wallHeal);
                SFX.Instance.PlayOneShot(ResourceManager.Instance.GetAudioClip("WallHeal"), 0.2f);
                StartCoroutine(CoRecoverWall());
            }
        }
    }

    IEnumerator CoRecoverWall()
    {
        GameObject wall = transform.Find("WallHeal").gameObject;
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        wall.gameObject.SetActive(false);
        boxCollider.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(10f);
        _wallHP = 5;
        wall.gameObject.SetActive(true);
        boxCollider.GetComponent<BoxCollider2D>().enabled = true;
    }
}
