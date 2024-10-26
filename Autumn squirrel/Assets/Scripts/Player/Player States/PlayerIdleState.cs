using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public static Type StateType { get => typeof(PlayerIdleState); }
    private int _cyclesToRotateHead = 2;
    private float _idleAnimeLength;
    private float _rotateHeadAnimTime;
    private float _time = 0;
    private bool _isRotatingHead;
    private Coroutine _cor;
    public PlayerIdleState(GetState function) : base(function)
    {
    }

    public override void SetUpState(PlayerContext context)
    {
        base.SetUpState(context);
        _context.animationManager.PlayAnimation("Idle");
        _idleAnimeLength = _context.animationManager.GetAnimationLength("Idle");
        _rotateHeadAnimTime = _context.animationManager.GetAnimationLength("Rotate head");
        
        _time = 0;
    }

    public override void Update()
    {
        PerformInputCommand();

        if (_time < _idleAnimeLength * _cyclesToRotateHead)
        {
            _time += Time.deltaTime;
        }
        else if(!_isRotatingHead)
        {
            _context.animationManager.PlayAnimation("Rotate head");
            Logger.Log("Start cir");
            _cor = _context.WaitAndPerformFunction(_rotateHeadAnimTime, () => { _context.animationManager.PlayAnimation("Idle"); ResetTimer(); });
            _isRotatingHead = true;
        }

    }
    public override void Move(Vector2 direction)
    {
        if (direction.x==0) return;
        base.Move(direction);
        _context.firstMove = direction;
        ChangeState(PlayerMoveState.StateType);
    }
    private void ResetTimer()
    {
        _time= 0;
        _isRotatingHead= false;
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
    public override void InterruptState()
    {
        if (_cor!=null) _context.coroutineHolder.StopCoroutine(_cor);

    }
}