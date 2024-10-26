using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerChecks : MonoBehaviour
{
    public bool Aa;
    public RaycastHit2D IsOnToOfATree => _treeTopHit;
    public RaycastHit2D IsInFrontOfATree =>_treeHit;
    public RaycastHit2D IsInFrontOfGround => _groundFromTreeHit;
    public RaycastHit2D IsOnGround => _hit;
    [SerializeField] Transform _groundRayCastOrigin;
    [SerializeField] Transform _treeRayCastOrigin;
    [SerializeField] LayerMask _ground;
    [SerializeField] LayerMask _tree;
    [SerializeField] Transform _mainBody;
    [SerializeField] float _treeRayLength;
    [SerializeField] float _treeTopRayLength;
    [SerializeField] float _groundRayLength;
    private RaycastHit2D _hit;
    private RaycastHit2D _treeHit;
    private RaycastHit2D _treeTopHit;
    private RaycastHit2D _groundFromTreeHit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _hit = Physics2D.Raycast(_groundRayCastOrigin.position, -_mainBody.up, _groundRayLength, _ground);
        _treeHit = Physics2D.Raycast(_treeRayCastOrigin.position, _mainBody.right*_mainBody.localScale.x, _treeRayLength, _tree);
        _groundFromTreeHit= Physics2D.Raycast(_treeRayCastOrigin.position, _mainBody.right * _mainBody.localScale.x, _treeRayLength, _ground);


    }
    public bool HasReachedtreeTop()
    {
        if (!_treeHit)
        {
            Vector2 org = _treeRayCastOrigin.position + _mainBody.right * _mainBody.localScale.x - _mainBody.up * _treeTopRayLength;
            _treeTopHit = Physics2D.Raycast(org, -_mainBody.right, 1f, _tree);
            if (_treeTopHit) 
            {
                return true;
            }
        }
        return false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(_groundRayCastOrigin.position,-_mainBody.up* _groundRayLength);
        Gizmos.color = new Color(155/255f, 103/255f, 60/255f,1);
        //Gizmos.color = Color.blue;
        Gizmos.DrawRay(_treeRayCastOrigin.position, _mainBody.right * _treeRayLength * _mainBody.localScale.x);
        Gizmos.color = Color.red;
        Vector2 org = _treeRayCastOrigin.position + _mainBody.right * _mainBody.localScale.x - _mainBody.up * _treeTopRayLength;
        Gizmos.DrawRay(org,-_mainBody.right);
        //if (_hit) Gizmos.DrawRay(_hit.point, _hit.normal * 2);
    }
}
