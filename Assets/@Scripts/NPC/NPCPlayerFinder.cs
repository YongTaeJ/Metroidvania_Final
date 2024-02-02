using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class NPCPlayerFinder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            var controller = other.GetComponent<PlayerInputController>();
            controller.OnInteraction -= StartConversation;
            controller.OnInteraction += StartConversation;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            var controller = other.GetComponent<PlayerInputController>();
            controller.OnInteraction -= StartConversation;
        } 
    }

    // TODO => 상황에 맞는 대화문을 출력할 수 있도록
    private void StartConversation()
    {
        StartCoroutine(Conversation());
    }

    private IEnumerator Conversation()
    {
        yield return null;
    }
}
