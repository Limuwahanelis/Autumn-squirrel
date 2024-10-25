using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChecks : MonoBehaviour
{
    [SerializeField] Transform _groundRayCastOrigin;
    [SerializeField] LayerMask _ground;
    [SerializeField] Transform _mainBody;
    private RaycastHit2D _hit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _hit = Physics2D.Raycast(_groundRayCastOrigin.position, -_mainBody.up, 25f, _ground);
        if(_hit)
        {
            _mainBody.up = _hit.normal;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(_groundRayCastOrigin.position,-_mainBody.up*25);
        if (_hit) Gizmos.DrawRay(_hit.point, _hit.normal * 2);
    }
}
