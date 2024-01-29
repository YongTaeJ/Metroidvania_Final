using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newPlayerData", menuName ="Data/Player Data/Base Date")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 10f;

    [Header("jump State")]
    public float jumpVelocity = 15f;

    [Header("Check Variables")]
    public float groundCheckDistance = 0.1f;
    public LayerMask whatIsGround;
}
