using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] PlayerMovement _playerMovement;
    Vector2 _direction;
    private void Update()
    {
        _playerMovement.Move(_direction);
    }

    public void OnMove(InputValue inputValue)
    {
        _direction = inputValue.Get<Vector2>();
        
    }
}
