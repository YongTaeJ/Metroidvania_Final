using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TownEnter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Renderer renderer = this.GetComponent<Renderer>();
            renderer.material.DOColor(Color.black, 1.8f);

            Invoke("ColorReset", 1.8f);
        }
    }

    private void ColorReset()
    {

        Renderer renderer = this.GetComponent<Renderer>();
        renderer.material.DOColor(Color.white, 0.6f);
    }
}
