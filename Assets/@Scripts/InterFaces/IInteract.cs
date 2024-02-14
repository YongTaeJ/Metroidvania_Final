using System.Collections;
using UnityEngine.InputSystem;

/// <summary>
/// 플레이어의 주도권을 뺏고 절차적인 실행이 필요할 때 사용하는 인터페이스입니다. ex) Conversation, Event, etc.
/// </summary>
public interface IInteract
{
    public IEnumerator Interact(PlayerInput input);
}
