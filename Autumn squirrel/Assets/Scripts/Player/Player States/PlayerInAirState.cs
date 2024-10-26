using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    public static Type StateType { get => typeof(PlayerInAirState); }
    private bool _isFalling;
    private Vector2 _direction;
    public PlayerInAirState(GetState function) : base(function)
    {
    }

    public override void SetUpState(PlayerContext context)
    {
        base.SetUpState(context);
    }

    public override void Update()
    {
        PerformInputCommand();
        if (_context.playerMovement.IsPlayerFalling)
        {
            if (!_isFalling)
            {
                _context.animationManager.PlayAnimation("Fall");
                _isFalling = true;
            }
        }
        if (_context.checks.IsOnGround && math.abs(_context.playerMovement.PlayerRB.velocity.y) < 0.0004)
        {
            // _context.playerMovement.SetRBMaterial(PlayerMovement.PhysicMaterialType.NORMAL);
            //if (_stateTypeToChangeFromInputCommand != null)
            //{
            //    ChangeState(_stateTypeToChangeFromInputCommand);
            //    _stateTypeToChangeFromInputCommand = null;
            //}
            //else 
            ChangeState(PlayerIdleState.StateType);
            return;
        }

        if (_context.checks.IsInFrontOfATree && _direction.y > 0)
        {
            if (_context.playerMovement.TrySnapToATree())
            {
                _context.playerMovement.StopPlayer();
                ChangeState(PlayerClimbingATreeIdleState.StateType);
                return;
            }
        }
    }
    public override void Move(Vector2 direction)
    {
        base.Move(direction);
        _direction= direction;
    }
    public override void InterruptState()
    {
        _isFalling = false;
    }
}