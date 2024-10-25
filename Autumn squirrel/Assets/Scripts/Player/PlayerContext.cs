using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContext
{
    public Action<PlayerState> ChangePlayerState;
    public Func<float, Action, Coroutine> WaitAndPerformFunction;
    public Func<Action, Coroutine> WaitFrameAndPerformFunction;
    public MonoBehaviour coroutineHolder;
    public AnimationManager animationManager;
    public PlayerMovement playerMovement;
    public PlayerChecks checks;
    public Vector2 firstMove;
   // public PlayerCollisions collisions;
    public bool canPerformAirCombo;
}
