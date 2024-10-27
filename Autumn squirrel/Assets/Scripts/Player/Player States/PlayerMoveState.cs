using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    public static Type StateType { get => typeof(PlayerMoveState); }
    private float _move1AnimeLength;
    private float _move2AnimeLength;
    private Vector2 _direction;
    private Vector2 _newDirection;
    private float _time;
    bool _isInFirstPart = true;
    public PlayerMoveState(GetState function) : base(function)
    {
    }

    public override void SetUpState(PlayerContext context)
    {
        base.SetUpState(context);
        _context.animationManager.PlayAnimation("Move single 1");
        _move1AnimeLength = _context.animationManager.GetAnimationLength("Move single 1");
        _move2AnimeLength = _context.animationManager.GetAnimationLength("Move single 2");
        _direction = _context.firstMove;
        _isInFirstPart = true;
        _time = 0;
    }

    public override void Update()
    {
        PerformInputCommand();
        _time += Time.deltaTime;
        if (_isInFirstPart)
        {
            if (_time > _move1AnimeLength)
            {
                if (_direction == Vector2.zero) { ChangeState(PlayerIdleState.StateType); return; }
                else
                {
                    _time = 0;
                    _context.animationManager.PlayAnimation("Move single 2");
                    _isInFirstPart= false;
                    //_direction = _newDirection;
                }
            }
        }
        else
        {
            if (_time > _move2AnimeLength)
            {
                if (_direction == Vector2.zero) { ChangeState(PlayerIdleState.StateType); return; }
                else
                {
                    _time = 0;
                    _context.animationManager.PlayAnimation("Move single 1");
                    _isInFirstPart = true;
                    //_direction = _newDirection;
                }
            }
        }
        if(_context.checks.IsInFrontOfATree && _direction.y>0)
        {
            if(_context.playerMovement.TrySnapToATree())
            {
                ChangeState(PlayerClimbingATreeIdleState.StateType);
            }
        }
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        _context.playerMovement.Move(_direction);
    }
    public override void DigHole()
    {
        base.DigHole();
        ChangeState(PlayerDiggingAHoleState.StateType);
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
    }
    public override void InterruptState()
    {
        _context.playerMovement.Move(Vector2.zero);
    }
}