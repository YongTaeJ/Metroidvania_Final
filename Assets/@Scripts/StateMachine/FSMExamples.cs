using System.Collections.Generic;
using UnityEngine;

// 스파르타 슈팅클럽 우진영님 코드 예시입니다.

//#region StateExamples

//public class PlayerStateBaseExample : IState
//{
//    protected PlayerStateBaseExample _machine;
//    // protected PlayerController _controller;
//    // protected InputManager _input;
//    protected Transform _cameraTransform;

//    public PlayerStateBaseExample(PlayerStateBaseExample machine)
//    {
//        _machine = machine;
//        // _controller = machine.Controller;
//        // _input = machine.Input;
//        // _cameraTransform = machine.CameraTransform;
//    }

//    public virtual void OnStateEnter() { }

//    public virtual void OnStateExit() { }

//    public virtual void OnStateStay() { }
//}

//public class PlayerGroundState : PlayerStateBaseExample
//{
//    public PlayerGroundState(PlayerStateBaseExample machine) : base(machine) { }

//    public override void OnStateStay()
//    {
//        // _controller.Move();
//        // _controller.SetFastRun(_controller.IsFastRun);
//        // if (_input.ADSTrigger && !WeaponEquipManager.Instance.CurrentWeapon.IsReloading)
//        //     _controller.ChangeADS();
//        // if (!_input.FastRunPress && _input.FirePress)
//        //     WeaponEquipManager.Instance.CurrentWeapon.ShotStart();
//        // if (_input.ReloadTrigger)
//        //     WeaponEquipManager.Instance.CurrentWeapon.Reload();
//    }
//}

//public class PlayerStandState : PlayerGroundState
//{
//    public PlayerStandState(PlayerStateBaseExample machine) : base(machine) { }

//    public override void OnStateEnter()
//    {
//        // _controller.Sit(false);
//    }

//    public override void OnStateStay()
//    {
//        base.OnStateStay();
//        // if (_input.SitKeyDown)
//        //     _machine.StateTransition(_machine.StateDictionary[PlayerStateMachine.PlayerState.Sit]);
//    }
//}

//public class PlayerSitState : PlayerGroundState
//{
//    public PlayerSitState(PlayerStateBaseExample machine) : base(machine) { }

//    public override void OnStateEnter()
//    {
//        // _controller.Sit(true);
//    }

//    public override void OnStateStay()
//    {
//        base.OnStateStay();
//        // if (_input.SitKeyDown)
//        //     _machine.StateTransition(_machine.StateDictionary[PlayerStateMachine.PlayerState.Stand]);
//    }
//}

//#endregion

//#region StateMachineExample

//public class PlayerStateMachineExamples : StateMachine<PlayerStateBaseExample>
//{
//    // public PlayerController Controller { get; private set; }
//    // public InputManager Input { get; private set; }
//    public Transform CameraTransform { get; private set; }

//    public enum PlayerState
//    {
//        Stand,
//        Sit,
//    }

//    private Dictionary<PlayerState, PlayerStateBaseExample> _stateDict;

//    public Dictionary<PlayerState, PlayerStateBaseExample> StateDictionary => _stateDict;

//    private void Awake()
//    {
//        Initialize();
//    }

//    public void Initialize()
//    {
//        // Controller = GetComponent<PlayerController>();
//        // Input = InputManager.Instance;
//        CameraTransform = Camera.main.transform;
//        _stateDict = new()
//        {
//            // { PlayerState.Stand, new PlayerStandState(this) },
//            // { PlayerState.Sit, new PlayerSitState(this) },
//        };
//        StateTransition(_stateDict[PlayerState.Stand]);
//    }
//}

//#endregion