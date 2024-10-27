using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkingOnATreeTopState : PlayerState
{
    public static Type StateType { get => typeof(PlayerWalkingOnATreeTopState); }
    private Vector2 _direction;
    public PlayerWalkingOnATreeTopState(GetState function) : base(function)
    {
    }

    public override void SetUpState(PlayerContext context)
    {
        base.SetUpState(context);
        _context.animationManager.PlayAnimation("Climb");
        _direction = _context.firstMove;
    }

    public override void Update()
    {
        PerformInputCommand();
        if (!_context.checks.IsOnGround) ChangeState(PlayerInAirState.StateType);
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        _context.playerMovement.Move(_direction);
    }
    public override void Jump()
    {
        base.Jump();
        ChangeState(PlayerJumpingState.StateType);
    }
    public override void Move(Vector2 direction)
    {
        base.Move(direction);
        _direction = direction;
        if (_direction.x == 0) ChangeState(PlayerIdlingOnATreeTopState.StateType);
    }
    public override void InterruptState()
    {
        _context.playerMovement.Move(Vector2.zero);
    }
}