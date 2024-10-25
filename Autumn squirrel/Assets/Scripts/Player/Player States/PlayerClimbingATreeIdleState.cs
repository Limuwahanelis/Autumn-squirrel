using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbingATreeIdleState : PlayerState
{
    public static Type StateType { get => typeof(PlayerClimbingATreeIdleState); }
    public PlayerClimbingATreeIdleState(GetState function) : base(function)
    {
    }

    public override void SetUpState(PlayerContext context)
    {
        base.SetUpState(context);
    }

    public override void Update()
    {
        PerformInputCommand();
    }
    public override void Move(Vector2 direction)
    {
        base.Move(direction);
        ChangeState(PlayerClimbingATreeState.StateType);
    }
    public override void InterruptState()
    {
     
    }
}