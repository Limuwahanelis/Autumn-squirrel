using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerChecks _playerChecks;
    [SerializeField] float _speed;
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] Transform _mainBody;
    public void Move(Vector2 direction)
    {
        _rb.velocity = new Vector2(_speed*direction.x, 0f);
    }
}
