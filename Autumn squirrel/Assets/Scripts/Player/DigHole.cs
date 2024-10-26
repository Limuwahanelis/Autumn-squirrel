using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigHole : MonoBehaviour
{
    public bool IsHoleBuilt => _isHoleBuilt;
    [SerializeField] Transform _holePos;
    [SerializeField] GameObject _hole;
    [SerializeField] PlayerChecks _playerChecks;
    private bool _isHoleBuilt;
    private bool _isCheckingHole = false;
    private bool _canPreviewHole = true;
    private void Update()
    {
        if (!_playerChecks.IsOnGround || _playerChecks.IsInFrontOfATree)
        {
            _canPreviewHole = false;
        }
        else _canPreviewHole = true;
        if(_isCheckingHole)
        {
            if(!_canPreviewHole) EndHolePreview();
        }
    }
    public void PreviewHole()
    {
        if (!_canPreviewHole) return;
        if (_isHoleBuilt) return;
        _hole.SetActive(true);
        _hole.transform.position = _holePos.position;
        _isCheckingHole = true;
    }
    public void EndHolePreview()
    {
        _isCheckingHole = false;
        if (_isHoleBuilt) return;
        _hole.SetActive(false);
    }
    public void BuildHole()
    {
        if (!_isCheckingHole) return;
        _isHoleBuilt = true;
        Color color =_hole.GetComponent<SpriteRenderer>().color;
        _hole.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 1);
    }
}
