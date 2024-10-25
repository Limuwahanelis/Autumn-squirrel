using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerChecks : MonoBehaviour
{
    public bool Aa;
    public RaycastHit2D IsInFrontOfATree =>_treeHit;
    public RaycastHit2D IsInFrontOfGround => _groundFromTreeHit;
    [SerializeField] Transform _groundRayCastOrigin;
    [SerializeField] Transform _treeRayCastOrigin;
    [SerializeField] LayerMask _ground;
    [SerializeField] LayerMask _tree;
    [SerializeField] Transform _mainBody;
    [SerializeField] float _treeRayLength;
    private RaycastHit2D _hit;
    private RaycastHit2D _treeHit;
    private RaycastHit2D _groundFromTreeHit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _hit = Physics2D.Raycast(_groundRayCastOrigin.position, -_mainBody.up, 25f, _ground);
        //if(_hit)
        //{
        //    _mainBody.up = _hit.normal;
        //}
        _treeHit = Physics2D.Raycast(_treeRayCastOrigin.position, _mainBody.right*_mainBody.localScale.x, _treeRayLength, _tree);
        _groundFromTreeHit= Physics2D.Raycast(_treeRayCastOrigin.position, _mainBody.right * _mainBody.localScale.x, _treeRayLength, _ground);
        Aa = _groundFromTreeHit;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(_groundRayCastOrigin.position,-_mainBody.up*25);
        Gizmos.color = new Color(155/255f, 103/255f, 60/255f,1);
        //Gizmos.color = Color.blue;
        Gizmos.DrawRay(_treeRayCastOrigin.position, _mainBody.right * _treeRayLength * _mainBody.localScale.x);
        //if (_hit) Gizmos.DrawRay(_hit.point, _hit.normal * 2);
    }
}
