using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{
    public Player player {  get; set; }

    public override bool Initialize()
    {
  
        return base.Initialize();
    }
}
