



### 구현 내역

#### 플레이어 움직임

<details>

플레이어 컨트롤

![PlayerControl](https://github.com/YongTaeJ/Metroidvania_Final/assets/144416099/6feb94dc-1b78-4862-b961-5a40b271a1b7)


플레이어 스킬 1번

![Skill01](https://github.com/YongTaeJ/Metroidvania_Final/assets/144416099/51ecf361-9d52-4414-84d4-ce2af3fdd1a5)


플레이어 스킬 2번

![Skill02](https://github.com/YongTaeJ/Metroidvania_Final/assets/144416099/1c7cd243-2472-4376-bb4c-8a0a0163365c)


플레이어 사망

![Death](https://github.com/YongTaeJ/Metroidvania_Final/assets/144416099/dd420f56-5d8f-4a48-803a-7db3695cadb7)

</details>

#### 몬스터 구현

<details>

##### 보스

보스 패턴 1번

![BossPattern01](https://github.com/YongTaeJ/Metroidvania_Final/assets/144416099/a91d97d3-34d7-42bc-a09e-eb33f53d5fbc)


보스 패턴 2번

![BossPattern02](https://github.com/YongTaeJ/Metroidvania_Final/assets/144416099/1b173cba-e5cc-4ab3-9b06-0b9594314735)


보스 패턴 3번

![BossPattern03](https://github.com/YongTaeJ/Metroidvania_Final/assets/144416099/358fd874-6c5e-4a45-9318-373140be0a8d)

</details>

#### 순간이동 구현

<details>
  
![Teleport](https://github.com/YongTaeJ/Metroidvania_Final/assets/144416099/5484f08c-d1a8-465c-b0b3-c4e03848b734)
  
</details>

### 코드리뷰 관련

#### 정용태
<details>
//////////////PatternFinder.cs

상태간의 Transition 대신 State를 총괄적으로 판단하여 전달해줄 클래스 작성

  궁금한 부분

1. 해당 코드가 몬스터와는 별개로 보스만을 위해 작성되었는데, 이것이 객체지향에 적합한 코딩인지 모르겠습니다.
2. 주어진 시간이 많다면 패턴이 많은 보스는 일반 몬스터와 다른 방법으로 작업하는게 좋은지, 아니면 이런 식으로 작성하는 것에 큰 문제가 없는지 궁금합니다.

/////////////UIManager.cs

Resources 폴더에 있는 특정 위치의 UI 프리펩들을 모두 Load하여, Canvas 내부에서 활용할 수 있도록 고안한 싱글톤 코드 작성

  궁금한 부분

UI의 빈도에 따라 구분하여 Fixed, Popup, Disposable으로 나누어 각자 고정UI, 활성/비활성화, 생성/파괴 형식으로 구현했습니다.

Fixed : 특정 이벤트에서 가시성을 위해 껐다 켜지고 대부분의 상황에는 켜져있는 UI입니다. (플레이어 체력, 스킬 목록 등)
Popup : KeyInput을 통해 언제나 돌입할 수 있거나, 내부에 컴포넌트가 많아 생성에 비용이 많이 들 것으로 예상하는 UI입니다(Status, Settings, Upgrade, Chat)
Disposable : 비주기적으로, 낮은 빈도로 등장하며 매우 단순한 정보를 전달하는 용도의 UI입니다.(ChestUI, InteractionUI)

이런 식으로 나누어서 작업을 진행했는데, 이런 방식에 문제점이 있을지 궁금합니다.
또한 게임 전체 UI의 깊이가 낮을 것이라 예상하여 단순하게 구조를 짰는데, UI 매니징에 관한 부분도 설계 단계에서 엄밀하게 고민하는 것이 효율이 좋은지 궁금합니다.
지금 진행 방식은 우선 내부 기능들을 구현하고 UI는 딸려오는 느낌으로 작업해서 크게 고민하지 않았기 때문에 궁금증이 생겼습니다.

//////////// BossRoom.cs, ChatManager.cs

  플레이어가 트리거 충돌을 일으키면, 보스전을 시작하기 위해 코루틴을 실행하는 코드입니다.
  ChatManager.cs 함수의 경우에는 코루틴을 이용해 npc 대화의 뼈대를 구현해 보았습니다.

  궁금한 부분

순차적인 시간 간격을 두고 처리하는 시스템을 모두 코루틴으로 작성하였는데, 이렇게 하니 너무 많은 코루틴 함수를 작성하게 되었습니다.
(보스룸 시작도 코루틴, 내부에서 대화가 끝나기를 기다리는 것도 코루틴, 코루틴을 호출하는 함수도 코루틴;;)
현재 체계가 작성하면서도 너무 코드가 쉽지 않다고(...) 생각되는데, 현재 코드의 개선점이나 코루틴이 아닌 다른 돌파구가 있을지 궁금합니다.
MVP 제작의 막바지에 도입한 시스템이라 코루틴의 개념도 정확히 알고 있는 것이 거의 없어서, 더 자세하게 짚고 넘어가야 한다는 생각이 들었습니다.

  긴 글 읽어주셔서 감사드리고, 또 긴 시간동안 열정적으로 도움 주셔서 정말 감사합니다!!

</details>

#### 조성민
<details>
/////////////WallBase.cs
플레이어의 이동을 제한하는 벽에 대한 기본 스크립트입니다.
하위로 접근 시 파괴되는 벽(가짜 벽 느낌), 공격 시 파괴되는 벽, 스킬로 공격 시 파괴되는 벽,
특정 조건(플레이어의 진행도)에 따라 파괴되는 벽으로 세분화했습니다.
궁금한 부분
상속을 사용해서 벽들을 모아봤는데, 이런 방식으로 작업하면 좋은지 궁금합니다.
현재는 벽들을 다 파괴되는 방식으로 작업했는데, 이들을 현재의 방법으로 진행해도 괜찮은지 궁금합니다.
       게임이 시작될 때마다 벽이 재생성되고 다시 파괴해야되는데 파괴된 상태를 기억해서 파괴되었다면 자동으로 파괴되게할지
       아니면 파괴된 상태라면 생성되지 않게 로직을 짜면 좋을지 궁금합니다.
/////////////MonsterSpawner.cs
몬스터를 생성하는 스포너의 스크립트입니다.
현재는 플레이어가 해당 콜라이더에 진입하면 생성되고, 나가면 파괴되는 방식으로 작동합니다.
현재는 오브젝트 풀을 사용하지 않지만 추후에 오브젝트 풀을 사용하게 변경할 예정입니다.
궁금한 부분
저희 게임이 2D 게임이고 스케일이 작은데 최적화적인 부분에서 오브젝트 풀을 사용하는 것에 대해 어떻게 생각하시나요?
       오브젝트 풀을 사용하고 안하고의 극적인 차이가 있을지 궁금합니다.
 2.  오브젝트 풀을 사용하는 경우에 어떤 방식으로 구현하면 좋을지 궁금합니다.
      현재의 방식으로 방에 입장하는 경우에 해당 방의 몬스터만 생성하는 것이 좋을지, 입장하는 방과 연결된 방들까지 생성하는 것이 좋을지 궁금합니다.
///////////// PortalTrigger.cs
순간이동 기능이 있는 포탈의 상태를 처리하는 스크립트입니다.
플레이어가 상호작용할 수 있게 만들고, 해당 포탈이 사용 가능한지 판단하고,
사용 가능하다면 사용하게끔, 사용 불가능하면 해당 포탈을 활성화 시키는 작업을 수행시킵니다.
궁금한 부분
위에 글로 적은 것만 봐도 기능이 너무 많이 달려있는데, 이들을 어떻게 나눠서 관리하면 좋을지 궁금합니다.
스크립트를 나눈다면 어떤 방식으로(어떤 조건으로) 기능들을 나누면 좋을지 궁금합니다.
</details>

#### 최현호
<details>
//// PlayerInputController.cs, TouchingDirection.cs
Input System의 입력을 받아 동작하는 메서드 들을 모아두는 스크립트입니다.
궁금한 부분
1. 동작들을 제어하기위해 행동에 대해 bool값을 주고
어떤행동이 true면 되고 안되고 하는 방식으로 구현되어 있는데 하다보니 제어하기 위해 코드가 너무 길어지는 부분이 있습니다.
추후 FSM으로 코드를 리펙토링 할 예정이지만 현재 코드의 개선점과 나쁜점을 명확하게 알보고 싶어서 여쭙습니다.
</details>