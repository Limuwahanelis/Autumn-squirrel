using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] PlayerController _player;
    [SerializeField] InputActionAsset _controls;
    [SerializeField] bool _useCommands;
    [SerializeField] PlayerInputStack _inputStack;
    [SerializeField] DigHole _digHole;
    [SerializeField] HoleManager _holeManager;
    private Vector2 _direction;
    private bool _showHole;
    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_player.IsAlive)
        {

            if (!PauseSettings.IsGamePaused)
            {
                _player.CurrentPlayerState.Move(_direction);

            }

            if(_showHole)
            {
                _digHole.PreviewHole();
            }
        }
    }
    private void OnMove(InputValue value)
    {
        _direction = value.Get<Vector2>();

    }
    void OnJump(InputValue value)
    {
        if (PauseSettings.IsGamePaused) return;
        if (_useCommands) _inputStack.CurrentCommand = new JumpInputCommand(_player.CurrentPlayerState);
        else _player.CurrentPlayerState.Jump();

    }
    void OnVertical(InputValue value)
    {
        _direction = value.Get<Vector2>();
        Logger.Log(_direction);
    }
    void OnPreviewHole(InputValue value)
    {
        if (_digHole.IsHoleBuilt) return;
        _showHole = value.Get<float>()>0?true:false;
        if(!_showHole) _digHole.EndHolePreview();
    }
    void OnBuildHole(InputValue value)
    {
        if (_digHole.IsHoleBuilt)
        {
            _holeManager.StoreAcorns();
            return;
        }
        _player.CurrentPlayerState.DigHole();

    }
    void OnJump()
    {
        _player.CurrentPlayerState.Jump();
    }
}
