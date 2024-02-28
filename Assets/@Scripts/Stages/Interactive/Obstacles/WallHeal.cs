using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHeal : WallBase
{
    [SerializeField]
    private float _wallHP = 4;
    [SerializeField]
    private float _wallHeal = 0.5f;
    protected override void WallReact(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            SFX.Instance.PlayOneShot(ResourceManager.Instance.GetAudioClip("WallReact"));
            _wallHP--;
            GameManager.Instance.player.GainHP(_wallHeal);


            if (_wallHP <= 0)
            {
                StartCoroutine(CoRecoverWall());
            }
        }
    }

    IEnumerator CoRecoverWall()
    {
        GameObject wall = transform.Find("Wall").gameObject;
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        wall.gameObject.SetActive(false);
        boxCollider.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(1f);
        _wallHP = 4;
        wall.gameObject.SetActive(true);
        boxCollider.GetComponent<BoxCollider2D>().enabled = true;
    }
}
