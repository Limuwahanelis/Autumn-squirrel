using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    public static Type StateType { get => typeof(PlayerMoveState); }
    private float _moveAnimeLength;
    private Vector2 _direction;
    private Vector2 _newDirection;
    private float _time;
    public PlayerMoveState(GetState function) : base(function)
    {
    }

    public override void SetUpState(PlayerContext context)
    {
        base.SetUpState(context);
        _context.animationManager.PlayAnimation("Move");
        _moveAnimeLength = _context.animationManager.GetAnimationLength("Move");
        _direction = _context.firstMove;
        _time = 0;
    }

    public override void Update()
    {
        PerformInputCommand();
        _time += Time.deltaTime;
        if(_time > _moveAnimeLength) 
        {
            if (_direction == Vector2.zero) { ChangeState(PlayerIdleState.StateType); return; }
            else
            {
                _time = 0;
                //_direction = _newDirection;
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
    public override void Move(Vector2 direction)
    {
        base.Move(direction);
        if (_direction.x == 0) return;
        _direction = direction;
    }
    public override void InterruptState()
    {
        _context.playerMovement.Move(Vector2.zero);
    }
}