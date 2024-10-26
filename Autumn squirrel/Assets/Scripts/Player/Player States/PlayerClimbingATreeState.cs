using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbingATreeState : PlayerState
{
    public static Type StateType { get => typeof(PlayerClimbingATreeState); }
    private Vector2 _direction;
    public PlayerClimbingATreeState(GetState function) : base(function)
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
        if (_context.checks.IsInFrontOfGround)
        {
            if (_context.playerMovement.TrySnapToGround())
            {
                ChangeState(PlayerMoveState.StateType);
                _context.firstMove = _direction;
                return;
            }
        }
        if(_context.checks.HasReachedtreeTop())
        {
            if(_context.playerMovement.TrySnapToTreeTop())
            {
                ChangeState(PlayerIdleState.StateType);
                return;
            }
        }
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        _context.playerMovement.Climb(_direction);
    }
    public override void Move(Vector2 direction)
    {
        base.Move(direction);
        _direction = direction;
        if (_direction == Vector2.zero) ChangeState(PlayerClimbingATreeIdleState.StateType);
    }
    public override void InterruptState()
    {
        _context.playerMovement.Move(Vector2.zero);
    }
}