using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerMovement : MonoBehaviour
{
    public enum playerDirection
    {
        LEFT = -1,
        SAME = 0,
        RIGHT = 1
    }
    public int FlipSide => _flipSide;
    public bool IsPlayerFalling { get => _rb.velocity.y < 0; }
    public Rigidbody2D PlayerRB => _rb;
    [SerializeField] float _normalGravityForce;
    [SerializeField] float _treeJumpOffset;
    [SerializeField] PlayerChecks _playerChecks;
    [SerializeField] float _speed;
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] Transform _mainBody;
    [SerializeField] PlayerController _player;
    [SerializeField] Transform _feet;
    [SerializeField] Ringhandle _jumpHandle;
    [SerializeField] float _jumpStrength;

    private int _flipSide = 1;
    private playerDirection _newPlayerDirection;
    private playerDirection _oldPlayerDirection;
    private float _previousDirection;
    public void Move(Vector2 direction)
    {
        // _rb.velocity = new Vector2(_speed*direction.x, 0f);

        if (direction.x != 0)
        {
            _oldPlayerDirection = _newPlayerDirection;
            _newPlayerDirection = (playerDirection)direction.x;
            _rb.MovePosition(_rb.position + new Vector2(_mainBody.right.x * _flipSide * _speed * Time.deltaTime, 0));
            //_rb.velocity = ;
            if (direction.x > 0)
            {
                _flipSide = 1;
                _player.MainBody.transform.localScale = new Vector3(_flipSide, _player.MainBody.transform.localScale.y, _player.MainBody.transform.localScale.z);
            }
            if (direction.x < 0)
            {
                _flipSide = -1;
                _player.MainBody.transform.localScale = new Vector3(_flipSide, _player.MainBody.transform.localScale.y, _player.MainBody.transform.localScale.z);
            }
            _previousDirection = direction.x;
        }
        else
        {
            if (_previousDirection != 0)
            {
                _rb.velocity = new Vector2(0, 0);
                _rb.MovePosition(_rb.position + new Vector2(_mainBody.right.x * _flipSide * _speed * Time.deltaTime, 0));
                //StopPlayerOnXAxis();
            }

        }

    }
    public void Climb(Vector2 direction)
    {
        // _rb.velocity = new Vector2(_speed*direction.x, 0f);

        if (direction.x != 0)
        {
            _oldPlayerDirection = _newPlayerDirection;
            _newPlayerDirection = (playerDirection)direction.x;
            _rb.MovePosition(_rb.position + new Vector2(0, _mainBody.right.y * _flipSide * _speed * Time.deltaTime));
            //_rb.velocity = ;
            if (direction.x > 0)
            {
                _flipSide = 1;
                _player.MainBody.transform.localScale = new Vector3(_flipSide, _player.MainBody.transform.localScale.y, _player.MainBody.transform.localScale.z);
            }
            if (direction.x < 0)
            {
                _flipSide = -1;
                _player.MainBody.transform.localScale = new Vector3(_flipSide, _player.MainBody.transform.localScale.y, _player.MainBody.transform.localScale.z);
            }
            _previousDirection = direction.x;
        }
        else
        {
            if (_previousDirection != 0)
            {
                _rb.velocity = new Vector2(0, 0);
                _rb.MovePosition(_rb.position + new Vector2(0, _mainBody.right.x * _flipSide * _speed * Time.deltaTime));
                //StopPlayerOnXAxis();
            }

        }
    }
    public bool TrySnapToATree()
    {
        if (_playerChecks.IsInFrontOfATree)
        {
            _mainBody.up = _playerChecks.IsInFrontOfATree.normal;
            _player.transform.position = _playerChecks.IsInFrontOfATree.point;
            _rb.gravityScale = 0;
            return true;
        }
        return false;
    }
    public bool TrySnapToGround()
    {
        if (_playerChecks.IsInFrontOfGround)
        {
            _mainBody.up = _playerChecks.IsInFrontOfGround.normal;
            _player.transform.position = _playerChecks.IsInFrontOfGround.point;
            _rb.gravityScale = _normalGravityForce;
            return true;
        }
        return false;
    }
    public bool TrySnapToTreeTop()
    {
        if (_playerChecks.IsOnToOfATree)
        {
            _mainBody.up = _playerChecks.IsOnToOfATree.normal;
            _player.transform.position = _playerChecks.IsOnToOfATree.point;
            _rb.gravityScale = _normalGravityForce;
            return true;
        }
        return false;
    }
    public void Jump()
    {
        _rb.velocity = new Vector3(0, 0, 0);
        _rb.AddForce(_jumpHandle.GetVector() * _jumpStrength, ForceMode2D.Impulse);
    }
    public void JumpFromATree()
    {
        _rb.velocity = new Vector3(0, 0, 0);
        //climbs up a tree
        if(_mainBody.up.x<0)
        {
            _mainBody.up = Vector3.up;
            _mainBody.localScale = new Vector3(-1, _mainBody.localScale.y, _mainBody.localScale.z);
           
        }
        else
        {
            _mainBody.up = Vector3.up;
            _mainBody.localScale = new Vector3(1, _mainBody.localScale.y, _mainBody.localScale.z);
        }
        _player.transform.position = new Vector3(_player.transform.position.x + _treeJumpOffset * _mainBody.localScale.x, _player.transform.position.y, _player.transform.position.z);
        _rb.gravityScale = _normalGravityForce;
        _rb.AddForce(Vector3.right*_mainBody.localScale.x*(_jumpStrength/2), ForceMode2D.Impulse);
    }
    public void StopPlayer(bool vertical = true, bool horizontal = true)
    {
        if (horizontal) _rb.velocity = new Vector2(0, _rb.velocity.y);
        if (vertical) _rb.velocity = new Vector2(_rb.velocity.x, 0);
    }
}
