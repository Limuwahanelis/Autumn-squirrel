using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdlingOnATreeTopState : PlayerState
{
    public static Type StateType { get => typeof(PlayerIdlingOnATreeTopState); }
    public PlayerIdlingOnATreeTopState(GetState function) : base(function)
    {
    }

    public override void SetUpState(PlayerContext context)
    {
        base.SetUpState(context);
        _context.animationManager.PlayAnimation("Climb idle");
    }

    public override void Update()
    {
        PerformInputCommand();
    }
    public override void Move(Vector2 direction)
    {
        base.Move(direction);
        if (direction == Vector2.zero) return;
        ChangeState(PlayerWalkingOnATreeTopState.StateType);
    }
    public override void Jump()
    {
        base.Jump();
        ChangeState(PlayerJumpingState.StateType);
    }
    public override void InterruptState()
    {

    }
}