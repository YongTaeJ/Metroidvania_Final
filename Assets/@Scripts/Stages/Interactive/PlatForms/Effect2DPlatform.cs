using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect2DPlatform : MonoBehaviour
{
    public string GroundLayer = "Ground";
    public string PlatformLayer = "Platform";
    public float CollisionDistance = 0.1f;
    private int playerLayer;
    private bool isDropDown = false;
    private Collider2D platformCollider;
    private bool _isInitialized = false;
    private void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        platformCollider = GetComponent<Collider2D>();
        OnEnable();
    }

    private void OnEnable()
    {
        if(!_isInitialized)
        {
            _isInitialized = true;
            return;
        }

        GameManager.Instance.player._controller.OnDropDownJump -= HandleDropDownJump;
        GameManager.Instance.player._controller.OnDropDownJump += HandleDropDownJump;
    }

    private void OnDisable()
    {
        if(!GameManager.IsNull())
        {
            GameManager.Instance.player._controller.OnDropDownJump -= HandleDropDownJump;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == playerLayer && !isDropDown)
        {
            Collider2D playerCollider = collision.collider;
            float playerBottom = playerCollider.bounds.min.y;

            foreach (ContactPoint2D point in collision.contacts)
            {
                if (point.point.y <= playerBottom + CollisionDistance)
                {
                    gameObject.layer = LayerMask.NameToLayer(GroundLayer);
                    return;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == playerLayer)
        {
            gameObject.layer = LayerMask.NameToLayer(PlatformLayer);
        }
    }

    private void HandleDropDownJump()
    {
        isDropDown = true;
        platformCollider.enabled = false;
        gameObject.layer = LayerMask.NameToLayer(PlatformLayer);
        StartCoroutine(ResetPlatformLayer());
    }

    private IEnumerator ResetPlatformLayer()
    {
        yield return new WaitForSeconds(0.3f);
        isDropDown = false;
        platformCollider.enabled = true;
    }
}
