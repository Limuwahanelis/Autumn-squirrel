using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDiggingAHoleState : PlayerState
{
    public static Type StateType { get => typeof(PlayerDiggingAHoleState); }
    private float _animLength;
    private float _time = 0;
    public PlayerDiggingAHoleState(GetState function) : base(function)
    {
    }

    public override void SetUpState(PlayerContext context)
    {
        base.SetUpState(context);
        _context.animationManager.PlayAnimation("Dig hole");
        _animLength = _context.animationManager.GetAnimationLength("Dig hole");
        _time = 0;
    }

    public override void Update()
    {
        PerformInputCommand();
        _time += Time.deltaTime;
        if(_time > _animLength) 
        {
            _context.digHole.BuildHole();
            _context.digHole.EndHolePreview();
            ChangeState(PlayerIdleState.StateType);
        }
    }

    public override void InterruptState()
    {
     
    }
}