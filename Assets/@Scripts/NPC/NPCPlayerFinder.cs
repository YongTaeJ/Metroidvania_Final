using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPlayerFinder : MonoBehaviour
{
    // 본인의 해당 대화문을 넘겨줌

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            var controller = other.GetComponent<PlayerInputController>();
            controller.OnInteraction -= aaaa;
            controller.OnInteraction += aaaa;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            var controller = other.GetComponent<PlayerInputController>();
            controller.OnInteraction -= aaaa;
        } 
    }

    private void aaaa()
    {

    }
}
